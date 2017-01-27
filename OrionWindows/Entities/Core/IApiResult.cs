// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApiResult.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the IApiResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Core
{
    using System.Net;

    /// <summary>
    /// The API Result interface.
    /// </summary>
    public interface IApiResult
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        string Result { get; set; }

        /// <summary>
        /// Determine if http status 200 OK returned
        /// </summary>
        /// <param name="throwException">
        /// Should exception be thrown if 200 OK not returned
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsOk(bool throwException);
    }
}
