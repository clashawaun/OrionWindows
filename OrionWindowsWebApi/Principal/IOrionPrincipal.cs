// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrionPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The OrionPrincipal interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Principal
{
    using System.Security.Principal;

    using OrionWindows.Entities.User;

    /// <summary>
    /// The OrionPrincipal interface.
    /// </summary>
    public interface IOrionPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets or sets the orion user profile.
        /// </summary>
        UserProfile User { get; set; }
    }
}
