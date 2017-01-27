// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserProfile.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The user profile.
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

    /// <summary>
    /// The user profile.
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the perm id.
        /// </summary>
        public string PermId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [JsonProperty(PropertyName = "UserName")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        public ICollection<UserMeta> Meta { get; set; }

        /// <summary>
        /// Gets or sets the organisation id.
        /// </summary>
        public string OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public UserRole Role { get; set; }
    }
}
