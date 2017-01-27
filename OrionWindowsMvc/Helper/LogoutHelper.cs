// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogoutHelper.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The logout helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Helper
{
    using System;
    using System.Web;

    /// <summary>
    /// The logout helper.
    /// </summary>
    public static class LogoutHelper
    {
        /// <summary>
        /// Generate html for logout
        /// </summary>
        /// <param name="title">
        /// Title of the link.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateLogoutHref(string title)
        {
            return $"<a href=\"/?orion_logout=true\">{title}</a>";
        }

        /// <summary>
        /// Generate html for logout.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString GenerateLogoutHtmlHref(string title)
        {
            return new HtmlString($"<a href=\"/?orion_logout=true\">{title}</a>");
        }

        /// <summary>
        /// Generate html for logout.
        /// </summary>
        /// <param name="title">
        /// The title.
        /// </param>
        /// <param name="redirectUrl">
        /// The redirect url.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString GenerateLogoutHtmlHref(string title, string redirectUrl)
        {
            return new HtmlString($"<a href=\"/?orion_logout=true&return_from={Uri.EscapeDataString(redirectUrl)}\">{title}</a>");
        } 
    }
}
