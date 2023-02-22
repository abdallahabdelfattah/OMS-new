using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Utilities
{
    public class JsonNetResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType)
                ? ContentType
                : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            MaxJsonLength = int.MaxValue;

            // If you need special handling, you can call another form of SerializeObject below
            //commented by osama
            //var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            response.Write(serializedObject);
        }
    }
}