// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FederationManager.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The federation manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using OrionWindowsMvc.Entity;
    using OrionWindowsMvc.Principal;

    /// <summary>
    /// The federation manager.
    /// </summary>
    public class FederationManager
    {
        /// <summary>
        /// Checks if a federation user logged in
        /// WARNING: This method is intended to be used in razor views to change layout components.
        /// DO NOT use this to actually authenticate users, authentication should be carried out using the 
        /// OrionFederationFilter attribute
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="user">
        /// The user principal.
        /// </param>
        /// <param name="allowOptional">
        /// Is authentication optional.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsAuthenticated(HttpRequestBase request, IPrincipal user, bool allowOptional = false)
        {

            if (request.RequestContext.HttpContext.Items["FederationStatus"] == null)
            {
                return false;
            }

            var federationState = (FederationStatus)request.RequestContext.HttpContext.Items["FederationStatus"];

            if (!allowOptional)
            {
                return federationState.Authenticated;
            }

            return ((OrionPrincipal)user).Identity.Name.Equals("optional") || federationState.Authenticated;
        }

        /// <summary>
        /// Generate a link to the public Orion Federation Service
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GenerateFederationLink(string returnUrl, string applicationId)
        {
            return $"https://orion.shanecraven.com/Federation/Login?returnurl={Uri.EscapeDataString(returnUrl)}&appId={applicationId}";
        }
    }
}
