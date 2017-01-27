// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The Authenticator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// The Authenticator interface.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Generate the authorization header for a http request.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <returns>
        /// The <see cref="AuthenticationHeaderValue"/>.
        /// </returns>
        Task<AuthenticationHeaderValue> GenerateAuthorizationHeader(HttpContent content);
    }
}
