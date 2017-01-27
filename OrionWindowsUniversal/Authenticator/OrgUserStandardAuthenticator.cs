// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrgUserStandardAuthenticator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the OrgUserStandardAuthenticator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsUniversal.Authenticator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;

    /// <summary>
    /// The authenticator for the OrgUserStandardAuthenticator Orion scheme.
    /// </summary>
    public class OrgUserStandardAuthenticator : IAuthenticator
    {
        /// <summary>
        /// The scheme name.
        /// </summary>
        private const string Scheme = "OrgUserStandard";

        /// <summary>
        /// Gets or sets the system account secret key.
        /// </summary>
        public Key SecertKey { get; set; }

        /// <summary>
        /// Gets or sets the system account public key.
        /// </summary>
        public Key PublicKey { get; set; }

        /// <summary>
        /// Gets or sets the external user key.
        /// </summary>
        public Key ExternalKey { get; set; }

        /// <summary>
        /// Generate authorization header for the OrgUserStandardAuthenticator Orion scheme.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<AuthenticationHeaderValue> GenerateAuthorizationHeader(HttpContent content)
        {
            var currentTime = DateTime.UtcNow.ToUniversalTime();
            var sig = $"{this.PublicKey.ApiKey}{currentTime.ToString(CultureInfo.InvariantCulture)}{ this.ExternalKey.ApiKey }";
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
                                     { "hash", signedHash },
                                     { "external_key", this.ExternalKey.ApiKey }
                                 };

            return new AuthenticationHeaderValue(Scheme, AuthenticatorHelper.ConvertDictToParams(headerDict));
        }
    }
}
