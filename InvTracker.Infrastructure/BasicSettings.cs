using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace InvTracker.Infrastructure
{
    public class BasicSettings
    {
        public static string GetAppSettingsValue(string keyName)
        {
            return WebConfigurationManager.AppSettings[keyName];
        }
    }
}
