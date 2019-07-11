using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using ASP_Net_MVC_MultiLanguage_Example.Models;

namespace ASP_Net_MVC_MultiLanguage_Example
{
    public class LanguageMang
    {
        public static List<Language> AvailableLanguage = new List<Language> {
            new Language {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Language {
                LanguageFullName = "Español", LanguageCultureName = "es"
            },
            new Language {
                LanguageFullName = "Italiano", LanguageCultureName = "it"
            },
        };
        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguage.Where(a => a.LanguageCultureName.Equals(lang)).FirstOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguage[0].LanguageCultureName;
        }
        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
    }
}