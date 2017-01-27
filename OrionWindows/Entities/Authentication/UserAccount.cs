// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAccount.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The system account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Entities.Authentication
{
    /// <summary>
    /// The system account.
    /// </summary>
    public class UserAccount : IOrionEntity
    {
        /// <summary>
        /// Gets or sets the First Name.
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
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
