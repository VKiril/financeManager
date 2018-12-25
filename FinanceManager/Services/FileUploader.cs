using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceManager.Services
{
    public class FileUploader
    {
        public static string GenerateUniqName()
        {
            return DateTime.Now.Ticks + Guid.NewGuid().ToString("N");
        }
    }
}