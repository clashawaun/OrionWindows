// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IOrionPrincipal.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The OrionPrincipal interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsMvc.Principal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.User;

    /// <summary>
    /// The OrionPrincipal interface.
    /// </summary>
    public interface IOrionPrincipal : IPrincipal
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        UserProfile User { get; set; }

        /// <summary>
        /// Gets or sets the user key.
        /// </summary>
        Key UserKey { get; set; }
    }
}
