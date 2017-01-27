// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrionPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the OrionPrincipal type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Principal
{
    using System;
    using System.Security.Principal;

    using OrionWindows.Entities.Authentication;
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
        /// Gets or sets the user orion profile.
        /// </summary>
        public UserProfile User { get; set; }

        /// <summary>
        /// Gets or sets the user key.
        /// </summary>
        public Key UserKey { get; set; }

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
        /// This method is not implemented for Orion federation. NotImplementedException will be thrown
        /// </exception>
        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
