// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommunicator.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The Communicator interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Communication
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.Core;

    /// <summary>
    /// The Communicator interface.
    /// </summary>
    public interface ICommunicator
    {
        /// <summary>
        /// Gets or sets the API authenticator.
        /// </summary>
        IAuthenticator ApiAuthenticator { get; set; }

        /// <summary>
        /// Method to send HTTP Post Request.
        /// </summary>
        /// <param name="payload">
        /// The payload.
        /// </param>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        Task<IApiResult> Post(object payload, string apiExtension, Dictionary<string, string> queryString = null);

        /// <summary>
        /// Method to send HTTP Get Request.
        /// </summary>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        Task<IApiResult> Get(string apiExtension, Dictionary<string, string> queryString = null);

        /// <summary>
        /// Method to send HTTP Delete Request.
        /// </summary>
        /// <param name="apiExtension">
        /// The API extension.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The <see cref="IApiResult"/>.
        /// </returns>
        Task<IApiResult> Delete(string apiExtension, Dictionary<string, string> queryString);
    }
}
