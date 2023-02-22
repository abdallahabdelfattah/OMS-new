using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Commons.Framework
{
    public class BasicCaptcha 
    {
        private string Letters { get; set; }

        private int CodeLength { get; set; }
        private string SessionName { get; set; }

        public BasicCaptcha(
            string letters = "0123456789",
            string sessionName = "CaptchaCode",
            int codeLength = 4)
        {
            Letters = letters;
            SessionName = sessionName;
            CodeLength = codeLength;
        }

        public string GenerateCaptchaCode()
        {
            var rand = new Random();
            var maxRand = Letters.Length - 1;

            var sb = new StringBuilder();
            for (var i = 0; i < CodeLength; i++)
            {
                var index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }



        public FileStreamResult GenerateCaptchaImage(int width = 100, int height = 36)
        {
            if (HttpContext.Current.Session == null)
            {
                throw new ArgumentNullException("Session",
                    "Session can not be null, please check if Session is enabled in ASP.NET Core via services.AddSession() and app.UseSession().");
            }
            var captchaCode = GenerateCaptchaCode();
            var result = CaptchaImageGenerator.GetImage(width, height, captchaCode);
            HttpContext.Current.Session.Add(SessionName, result.CaptchaCode);
            Stream s = new MemoryStream(result.CaptchaByteData);
            return new FileStreamResult(s, "image/png");
        }

        public bool IsValid(bool ignoreCase = true, bool dropSession = true)
        {
            var userInputCaptcha = HttpContext.Current.Request.Form["CaptchaCode"].ToString();
            var codeInSession = HttpContext.Current.Session[SessionName] as string;
            var isValid = string.Compare(userInputCaptcha, codeInSession, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
            if (dropSession)
            {
                HttpContext.Current.Session.Remove(SessionName);
            }
            return isValid == 0;
        }
    }
}