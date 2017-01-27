// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the UserController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.Core;
    using OrionWindows.Entities.User;

    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : IController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UserController(IOrion context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        public IOrion Context { get; set; }

        /// <summary>
        /// Get the user profile attached to the given key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator is required to call this method.
        /// </summary>
        /// <param name="userKey">
        /// The user key.
        /// </param>
        /// <param name="applicationPublicKey">
        /// The application public key.
        /// </param>
        /// <param name="allowRoamingUser">
        /// Do you want to allow roaming user keys to return a profile
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.UserTempKey or KeyType.UserPermKey is not used
        /// </exception>
        public async Task<OrionResult<UserProfile>> GetUserProfileAsync(Key userKey, string applicationPublicKey, bool allowRoamingUser = false)
        {
            if (userKey.Type != KeyType.UserTempKey && userKey.Type != KeyType.UserPermKey)
            {
                throw new Exception("You must use a temporary key to get a profile from the API");
            }

            var apiResult = await this.Context.Communicator.Get($"User/Profile/{applicationPublicKey}/{userKey.ApiKey}{(allowRoamingUser ? "/Roam" : "")}");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<UserProfile>(this.Context.Config.Parser.ParseData<UserProfile>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<UserProfile>(null);
            }
        }

        /// <summary>
        /// Get the user profile attached to the given key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator is required to call this method.
        /// </summary>
        /// <param name="userKey">
        /// The user key.
        /// </param>
        /// <param name="applicationPublicKey">
        /// The application public key.
        /// </param>
        /// <param name="allowRoamingUser">
        /// Do you want to allow roaming user keys to return a profile
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.UserTempKey or KeyType.UserPermKey is not used
        /// </exception>
        public OrionResult<UserProfile> GetUserProfile(Key userKey, string applicationPublicKey, bool allowRoamingUser = false)
        {
            var task = Task.Run(() => this.GetUserProfileAsync(userKey, applicationPublicKey, allowRoamingUser));
            task.Wait();
            return task.Result;
        }
    }
}
