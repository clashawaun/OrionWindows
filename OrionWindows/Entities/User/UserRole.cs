// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRole.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The user role.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// The user role.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserRole
    {
        /// <summary>
        /// The invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// The standard user.
        /// </summary>
        StandardUser,

        /// <summary>
        /// The system account.
        /// </summary>
        SystemAccount,

        /// <summary>
        /// The administrator.
        /// </summary>
        Administrator,

        /// <summary>
        /// The roaming user.
        /// </summary>
        Roaming
    }
}
