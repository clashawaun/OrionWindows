// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrionSystemAccountPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The OrionSystemAccountPrincipal interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Principal
{
    using System.Security.Principal;

    using OrionWindows.Entities.User.SystemAccount;

    /// <summary>
    /// The OrionSystemAccountPrincipal interface.
    /// </summary>
    public interface IOrionSystemAccountPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets or sets the orion system account context.
        /// </summary>
        SystemAccountContext Context { get; set; }
    }
}
