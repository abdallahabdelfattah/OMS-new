using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Commons.Framework.Extensions
{
    public class IOExtensions
    {
        public static void DeleteDirectoryIfExist(string directoryName, bool recursive = true)
        {
            if (Directory.Exists(directoryName))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
                directoryInfo.Delete(recursive);
            }
        }
        public static string CreateDirectory(string directoryPath, string directoryName, bool overWrite = false)
        {
            var newDirectoryPath = Path.Combine(directoryPath, directoryName);

            if (Directory.Exists(newDirectoryPath))
            {
                if (overWrite)
                {
                    DeleteDirectoryIfExist(newDirectoryPath);
                }
            }
            else
            {
                Directory.CreateDirectory(newDirectoryPath);
            }

            return newDirectoryPath;
        }

        public static FileContentResult DownloadFile(string filePath, string fileName)
        {
            var fileUrl = Path.Combine(filePath, fileName);
            if (!File.Exists(fileUrl))
            {
                return null;
            }

            var fileBytes = File.ReadAllBytes(fileUrl);
          
            var response = new FileContentResult(fileBytes, "application/zip")
            {
                FileDownloadName = fileName,                
            };
            return response;
        }
    }
}
