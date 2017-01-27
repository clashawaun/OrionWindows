// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalKeyAuthenticator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The external key authenticator.
//   This authenticator is for use with Web API's that are leveraging the Orion filter for authentication in the OrionWindowsWebApi library.
//   Its placed in the portable library because it does not have any platform specific dependencies
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Authenticator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;

    /// <summary>
    /// The external key authenticator.
    /// This authenticator is for use with Web API's that are leveraging the Orion filter for authentication in the OrionWindowsWebApi library.
    /// Its placed in the portable library because it does not have any platform specific dependencies
    /// </summary>
    public class ExternalKeyAuthenticator : IAuthenticator
    {
        /// <summary>
        /// The scheme.
        /// </summary>
        private const string Scheme = "OrionKey";

        /// <summary>
        /// Gets or sets the user temp key.
        /// </summary>
        public Key UserTempKey { get; set; }

        /// <summary>
        /// The generate authorization header.
        /// </summary>
        /// <param name="content">
        /// The content of the request
        /// </param>
        /// <returns>
        /// The <see cref="AuthenticationHeaderValue"/>.
        /// </returns>
        public async Task<AuthenticationHeaderValue> GenerateAuthorizationHeader(HttpContent content)
        {
             return new AuthenticationHeaderValue(Scheme, this.UserTempKey.ApiKey);
        }
    }
}
