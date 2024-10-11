﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);
            UpdateCultureCookie(context, culture);
        }

        /// <summary>
        /// Set the culture
        /// </summary>
        public string SetCulture(string language)
        {
            string culture = "";
            // Default to English if the input is null or empty
            language = language?.ToLower();
            if (string.IsNullOrEmpty(language))
            {
                return "en";
            }

            if (language == "french" || language == "français")
            {
                culture = "fr";
            }
            else if (language == "spanish" || language == "espagnol")
            {
                culture = "es";
            }
            else
            {
                culture = "en";
            }
            // TODO complete the code 
            // Default language is "en", french is "fr" and spanish is "es".
            return culture;
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
        }
    }
}