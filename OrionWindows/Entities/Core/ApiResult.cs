// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApiResult.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The api result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Core
{
    using System;
    using System.Net;

    /// <summary>
    /// The API result.
    /// </summary>
    public class ApiResult : IApiResult
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// Check if the request returned an OK response
        /// </summary>
        /// <param name="throwException">
        /// Should the method throw an exception if HTTP 200 not received
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// If throwException true, exception is thrown if HTTP 200 not returned
        /// </exception>
        public bool IsOk(bool throwException)
        {
            var result = (int)this.StatusCode >= 200 && (int)this.StatusCode < 300;

            if (!result)
            {
                if (throwException)
                {
                    throw new Exception("Response code was not OK");
                }
            }

            return result;
        }

    }
}
