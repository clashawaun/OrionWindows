// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrionPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The orion principal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Principal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Core.Controller;
    using OrionWindows.Entities.User;

    /// <summary>
    /// The orion principal.
    /// </summary>
    public class OrionPrincipal : IOrionPrincipal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionPrincipal"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public OrionPrincipal(string key)
        {
            this.Identity = new GenericIdentity(key);
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        public IIdentity Identity { get; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public UserProfile User { get; set; }

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
        /// This is not used for Orion Authentication
        /// </exception>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
