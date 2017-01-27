// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrionFederationFilter.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the OrionFederationFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;

    using OrionWindows.Entities.Authentication;

    using OrionWindowsDesktop;
    using OrionWindowsDesktop.Authenticator;
    using OrionWindowsDesktop.Logging;

    using OrionWindowsMvc.Entity;
    using OrionWindowsMvc.Principal;

    /// <summary>
    /// The orion federation filter for automatic single sign on in ASP.NET MVC.
    /// </summary>
    public class OrionFederationFilter : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// The _federation server endpoint.
        /// </summary>
        private readonly string federationServer;

        /// <summary>
        /// The application orion id.
        /// </summary>
        private readonly string applicationId;

        /// <summary>
        /// Is authentication optional.
        /// </summary>
        private readonly bool isOptional;

        /// <summary>
        /// A system account public key.
        /// </summary>
        private readonly string publicKey;

        /// <summary>
        /// A system account secret key.
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// The federation mode to enforce.
        /// </summary>
        private readonly FederationMode federationMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionFederationFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key.
        /// </param>
        /// <param name="secretKey">
        /// A system account secret key.
        /// </param>
        /// <param name="isOptional">
        /// The is optional.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="federationServer">
        /// The federation server endpoint.
        /// </param>
        public OrionFederationFilter(string publicKey, string secretKey, bool isOptional, string applicationId, string federationServer)
        {
            this.federationServer = federationServer;
            this.applicationId = applicationId;
            this.isOptional = isOptional;
            this.secretKey = secretKey;
            this.publicKey = publicKey;
            this.federationMode = FederationMode.AuthOnly;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionFederationFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key.
        /// </param>
        /// <param name="secretKey">
        /// A system account secret key.
        /// </param>
        /// <param name="isOptional">
        /// The is optional.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="federationServer">
        /// The federation server endpoint.
        /// </param>
        /// <param name="mode">
        /// The federation mode.
        /// </param>
        public OrionFederationFilter(string publicKey, string secretKey, bool isOptional, string applicationId, string federationServer, FederationMode mode)
        {
            this.federationServer = federationServer;
            this.applicationId = applicationId;
            this.isOptional = isOptional;
            this.secretKey = secretKey;
            this.publicKey = publicKey;
            this.federationServer = federationServer;
            this.federationMode = mode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionFederationFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key.
        /// </param>
        /// <param name="secretKey">
        /// A system account secret key.
        /// </param>
        /// <param name="isOptional">
        /// The is optional.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <param name="mode">
        /// The federation mode.
        /// </param>
        public OrionFederationFilter(string publicKey, string secretKey, bool isOptional, string applicationId, FederationMode mode)
        {
            this.federationServer = "https://orion.shanecraven.com/Federation";
            this.applicationId = applicationId;
            this.isOptional = isOptional;
            this.secretKey = secretKey;
            this.publicKey = publicKey;
            this.federationMode = mode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionFederationFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key.
        /// </param>
        /// <param name="secretKey">
        /// A system account secret key.
        /// </param>
        /// <param name="isOptional">
        /// The is optional.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        public OrionFederationFilter(string publicKey, string secretKey, bool isOptional, string applicationId)
        {
            this.federationServer = "https://orion.shanecraven.com/Federation";
            this.applicationId = applicationId;
            this.isOptional = isOptional;
            this.secretKey = secretKey;
            this.publicKey = publicKey;
            this.federationMode = FederationMode.AuthOnly;
        }

        /// <summary>
        /// Authenticate a request with Orion federation
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Request.QueryString["orion_logout"] != null)
            {
                if (filterContext.HttpContext.Request.QueryString["orion_logout"].Equals("true"))
                {
                    this.DeleteOrionCookie(filterContext.HttpContext.Request, filterContext.HttpContext.Response);

                    var returnFrom = string.Empty;
                    if (filterContext.HttpContext.Request.QueryString["return_from"] != null)
                    {
                        returnFrom = "?returnUrl=" + filterContext.HttpContext.Request.QueryString["return_from"];
                    }

                    filterContext.Result = new RedirectResult($"{this.federationServer}/Logout{Uri.EscapeDataString(returnFrom)}");
                    return;
                }
            }

            string orionAuthKey = null;
            var authKeyPresent = false;

            if (filterContext.HttpContext.Request.QueryString["orion_key"] != null)
            {
                authKeyPresent = true;
                orionAuthKey = filterContext.HttpContext.Request.QueryString["orion_key"];
            }
            else
            {
                orionAuthKey = this.ExtractOrionCookie(filterContext.HttpContext.Request);
            }

            if (orionAuthKey == null)
            {
                this.HandleAuthenticationFailed(filterContext, authKeyPresent);
                return;
            }

            var orion = new Orion(new DeleteMeLogger())
            {
                Communicator =
                {
                    ApiAuthenticator = new OrgStandardAuthenticator()
                    {
                        PublicKey = new Key() { ApiKey = this.publicKey, Type = KeyType.ApplicationPublicKey },
                        SecertKey = new Key() { ApiKey = this.secretKey, Type = KeyType.ApplicationSecretKey }
                    }
                }
            };

            var userProfileTask = Task.Run(() => orion.CreateUserController().GetUserProfileAsync(new Key() { ApiKey = orionAuthKey, Type = KeyType.UserTempKey }, this.applicationId, this.federationMode == FederationMode.RoamEnabled || this.federationMode == FederationMode.RoamOnly));
            userProfileTask.Wait(30000);

            var userProfile = userProfileTask.Result;
            if (userProfile.Result == null)
            {
                this.HandleAuthenticationFailed(filterContext, authKeyPresent);
                return;
            }

            if (this.ExtractOrionCookie(filterContext.HttpContext.Request) == null || authKeyPresent)
            {
                var cookie = new HttpCookie("OrionFederationKey") { Expires = DateTime.Now.AddDays(5), Value = orionAuthKey };
                filterContext.HttpContext.Response.Cookies.Add(cookie);
            }

            if (authKeyPresent)
            {
                this.RefreshKeyUrl(filterContext);
                return;
            }

            filterContext.HttpContext.Items["FederationStatus"] = new FederationStatus()
            {
                Authenticated = true,
                IsOptional = this.isOptional
            };

            filterContext.Principal = new OrionPrincipal(orionAuthKey) { User = userProfile.Result, UserKey = new Key() { ApiKey = orionAuthKey, Type = KeyType.UserTempKey } };
        }

        /// <summary>
        /// The on authentication challenge.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // Not applicable for Orion Federation
            return;
        }

        /// <summary>
        /// Extract an Orion Federation cookie from the request.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ExtractOrionCookie(HttpRequestBase message)
        {
            return message.Cookies["OrionFederationKey"]?.Value;
        }

        /// <summary>
        /// The delete orion cookie from the response.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        private void DeleteOrionCookie(HttpRequestBase request, HttpResponseBase response)
        {
            var orionCookie = request.Cookies["OrionFederationKey"];

            if (orionCookie == null)
            {
                return;
            }

            var expiresCookie = new HttpCookie("OrionFederationKey") { Expires = DateTime.Now.AddDays(-5) };
            response.Cookies.Add(expiresCookie);
        }

        /// <summary>
        /// Handles an authentication failure with the Orion service.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="authKeyPresent">
        /// Is an authentication key present.
        /// </param>
        private void HandleAuthenticationFailed(AuthenticationContext context, bool authKeyPresent)
        {
            if (this.isOptional)
            {
                if (authKeyPresent)
                {
                    this.RefreshKeyUrl(context);
                    return;
                }

                context.Principal = new OrionPrincipal("optional");
                context.HttpContext.Items["FederationStatus"] = new FederationStatus()
                {
                    Authenticated = false,
                    IsOptional = true
                };
            }
            else
            {
                context.Result = new RedirectResult($"{this.federationServer}/Login?returnurl={Uri.EscapeDataString(context.HttpContext.Request.Url?.AbsoluteUri)}&appid={this.applicationId}&federationMode={this.ConvertModeToString(this.federationMode)}");
            }
        }

        /// <summary>
        /// Refresh the URL to remove the Orion key.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void RefreshKeyUrl(AuthenticationContext context)
        {
            var newQueryString = string.Empty;

            var queryString = new NameValueCollection(context.HttpContext.Request.QueryString);
            queryString.Remove("orion_key");

            if (queryString.Count > 0)
            {
                newQueryString = string.Join("&", queryString.AllKeys.Select(x => x + "=" + HttpUtility.UrlEncode(queryString[x])));
            }

            var separator = queryString.Count > 0 ? "?" : string.Empty;

            context.Result = new RedirectResult($"{context.HttpContext.Request.Url.AbsolutePath}{separator}{newQueryString}");
        }

        /// <summary>
        /// Converts FederationMode enum to its string representation.
        /// </summary>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ConvertModeToString(FederationMode mode)
        {
            switch (mode)
            {
                case FederationMode.AuthOnly:
                    return "auth";
                case FederationMode.RoamEnabled:
                    return "roamEnabled";
                case FederationMode.RoamOnly:
                    return "roamOnly";
                default:
                    return "auth";
            }
        }
    }
}
