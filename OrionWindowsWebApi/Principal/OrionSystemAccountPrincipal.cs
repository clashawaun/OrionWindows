// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrionSystemAccountPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The orion system account principal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Principal
{
    using System;
    using System.Security.Principal;

    using OrionWindows.Entities.User.SystemAccount;

    /// <summary>
    /// The orion system account principal.
    /// </summary>
    public class OrionSystemAccountPrincipal : IOrionSystemAccountPrincipal
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="OrionSystemAccountPrincipal"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public OrionSystemAccountPrincipal(string key)
        {
            this.Identity = new GenericIdentity(key);
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        public IIdentity Identity { get; }


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public SystemAccountContext Context { get; set; }

        /// <summary>
        /// The is in role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// This is not used in Orion Authentication flow.
        /// </exception>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
