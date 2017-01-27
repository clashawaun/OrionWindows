// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrgStandardAuthenticator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The authenticator for the OrgStandardAuthenticator Orion scheme.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsDesktop.Authenticator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;

    /// <summary>
    /// The authenticator for the OrgStandardAuthenticator Orion scheme.
    /// </summary>
    public class OrgStandardAuthenticator : IAuthenticator
    {
        /// <summary>
        /// The scheme name.
        /// </summary>
        private const string Scheme = "OrgStandard";

        /// <summary>
        /// Initializes a new instance of the <see cref="OrgStandardAuthenticator"/> class.
        /// </summary>
        public OrgStandardAuthenticator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrgStandardAuthenticator"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// The application public key.
        /// </param>
        /// <param name="secertKey">
        /// The application secret key.
        /// </param>
        public OrgStandardAuthenticator(Key publicKey, Key secertKey)
        {
            this.PublicKey = publicKey;
            this.SecertKey = secertKey;
        }

        /// <summary>
        /// Gets or sets the application secret key.
        /// </summary>
        public Key SecertKey { get; set; }

        /// <summary>
        /// Gets or sets the application public key.
        /// </summary>
        public Key PublicKey { get; set; }

        /// <summary>
        /// Generate the authorization header that confirms to the OrgStandardAuthenticator Orion scheme
        /// </summary>
        /// <param name="content">
        /// The request body content.
        /// </param>
        /// <returns>
        /// The <see cref="AuthenticationHeaderValue"/>.
        /// </returns>
        public async Task<AuthenticationHeaderValue> GenerateAuthorizationHeader(HttpContent content)
        {
            var currentTime = DateTime.UtcNow.ToUniversalTime();
            var sig = $"{this.PublicKey.ApiKey}{currentTime.ToString(CultureInfo.InvariantCulture)}";
            var contentHash = await AuthenticatorHelper.CalculateHash(content);
            if (contentHash != null)
            {
                sig += Convert.ToBase64String(contentHash);
            }

            var signedHash = AuthenticatorHelper.GenerateHash(sig, this.SecertKey.ApiKey);
            var headerDict = new Dictionary<string, string>()
                                 {
                                     { "public_key", this.PublicKey.ApiKey },
                                     { "timestamp",  currentTime.ToUniversalTime().ToString("R") },
                                     { "hash", signedHash }
                                 };

            return new AuthenticationHeaderValue(Scheme, AuthenticatorHelper.ConvertDictToParams(headerDict));
        }
    }
}
