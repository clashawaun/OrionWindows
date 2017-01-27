// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationController.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the Authentication controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindows.Core.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.Authentication.Actions;
    using OrionWindows.Entities.Core;
    using OrionWindows.Entities.User.SystemAccount;
    using OrionWindows.Logging;
    using OrionWindows.Utils;

    /// <summary>
    /// The authentication controller.
    /// </summary>
    public class AuthenticationController : IController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public AuthenticationController(IOrion context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets or sets the orion context.
        /// </summary>
        public IOrion Context { get; set; }

        #region Login

        /// <summary>
        /// Attempts to login to an Orion API endpoint with the given email and password
        /// </summary>
        /// <param name="email">
        /// The email address of the user.
        /// </param>
        /// <param name="password">
        /// The password of the user.
        /// </param>
        /// <returns>
        /// A temporary user key <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> LoginAsync(string email, string password)
        {
            var apiResult = await this.Context.Communicator.Post(new LoginRequest { Email = email, Password = password }, "Authentication/User/Login");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Attempts to login to an Orion API endpoint with the given email and password
        /// </summary>
        /// <param name="email">
        /// The email address of the user.
        /// </param>
        /// <param name="password">
        /// The password of the user.
        /// </param>
        /// <returns>
        /// A temporary user key <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> Login(string email, string password)
        {
            var task = Task.Run(() => this.LoginAsync(email, password));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region Logout

        /// <summary>
        /// Attempt a logout operation of a user on an Orion API endpoint.
        /// </summary>
        /// <param name="keyToLogout">
        /// The key to logout.
        /// </param>
        /// <param name="logoutEverywhere">
        /// Should this user be logged out in all instances.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        public async Task<OrionResult<bool>> LogoutAsync(Key keyToLogout, bool logoutEverywhere = false)
        {
            var apiResult = await this.Context.Communicator.Post(keyToLogout, $"Authentication/User/Logout/{logoutEverywhere}");

            try
            {
                return new OrionResult<bool>(apiResult.IsOk(false));
            }
            catch (Exception)
            {
                return new OrionResult<bool>(false);
            }
        }

        /// <summary>
        /// Attempt a logout operation of a user on an Orion API endpoint.
        /// </summary>
        /// <param name="keyToLogout">
        /// The key to logout.
        /// </param>
        /// <param name="logoutEverywhere">
        /// Should this user be logged out in all instances.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        public OrionResult<bool> Logout(Key keyToLogout, bool logoutEverywhere = false)
        {
            var task = Task.Run(() => this.LogoutAsync(keyToLogout, logoutEverywhere));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region TwoFactorActions

        /// <summary>
        /// Perform two factor SMS authentication.
        /// </summary>
        /// <param name="action">
        /// The TwoFactorAuthentication action.
        /// </param>
        /// <param name="phoneDigits">
        /// The last 4 digits of users phone number.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.TwoFactorAuthentication is not used
        /// </exception>
        public async Task<OrionResult<bool>> PerformTwoFactorAuthenticationAsync(AuthenticationAction action, string phoneDigits)
        {
            if (action.ActionType != AuthenticationActionType.TwoFactorAuthentication)
            {
                throw new Exception("Must be a two factor authentication action");
            }

            return await this.DoAuthenticationAction(action, new TwoFactorRequirement() { PhoneVerificationDigits = phoneDigits }, "perform");
        }

        /// <summary>
        /// Perform two factor SMS authentication.
        /// </summary>
        /// <param name="action">
        /// The TwoFactorAuthentication action.
        /// </param>
        /// <param name="phoneDigits">
        /// The last 4 digits of users phone number.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.TwoFactorAuthentication is not used
        /// </exception>
        public OrionResult<bool> PerformTwoFactorAuthentication(AuthenticationAction action, string phoneDigits)
        {
            var task = Task.Run(() => this.PerformTwoFactorAuthenticationAsync(action, phoneDigits));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Validate two factor SMS authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.TwoFactorAuthentication is not used
        /// </exception>
        public async Task<OrionResult<bool>> ValidateTwoFactorAuthenticationAsync(AuthenticationAction action, string verificationCode)
        {
            if (action.ActionType != AuthenticationActionType.TwoFactorAuthentication)
            {
                throw new Exception("Must be a two factor authentication action");
            }

            return await this.DoAuthenticationAction(action, new TwoFactorVerification() { VerificationCode = verificationCode }, "validate");
        }

        /// <summary>
        /// Validate two factor SMS authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.TwoFactorAuthentication is not used
        /// </exception>
        public OrionResult<bool> ValidateTwoFactorAuthentication(AuthenticationAction action, string verificationCode)
        {
            var task = Task.Run(() => this.ValidateTwoFactorAuthenticationAsync(action, verificationCode));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region ConfirmPhoneActions

        /// <summary>
        /// Perform confirm phone authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="phoneDigits">
        /// The last 4 digits of the users phone number.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmPhone is not used
        /// </exception>
        public async Task<OrionResult<bool>> PerformConfirmPhoneAuthenticationAsync(AuthenticationAction action, string phoneDigits)
        {
            if (action.ActionType != AuthenticationActionType.ConfirmPhone)
            {
                throw new Exception("Must be a confirm phone authentication action");
            }

            return await this.DoAuthenticationAction(action, new ConfirmPhoneRequirement() { PhoneVerificationDigits = phoneDigits }, "perform");
        }

        /// <summary>
        /// Perform confirm phone authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="phoneDigits">
        /// The last 4 digits of the users phone number.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmPhone is not used
        /// </exception>
        public OrionResult<bool> PerformConfirmPhoneAuthentication(AuthenticationAction action, string phoneDigits)
        {
            var task = Task.Run(() => this.PerformConfirmPhoneAuthenticationAsync(action, phoneDigits));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Validate confirm phone authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmPhone is not used
        /// </exception>
        public async Task<OrionResult<bool>> ValidateConfirmPhoneAuthenticationAsync(AuthenticationAction action, string verificationCode)
        {
            if (action.ActionType != AuthenticationActionType.ConfirmPhone)
            {
                throw new Exception("Must be a confirm phone authentication action");
            }

            return await this.DoAuthenticationAction(action, new ConfirmPhoneVerification() { VerificationCode = verificationCode }, "validate");
        }

        /// <summary>
        /// Validate confirm phone authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmPhone is not used
        /// </exception>
        public OrionResult<bool> ValidateConfirmPhoneAuthentication(AuthenticationAction action, string verificationCode)
        {
            var task = Task.Run(() => this.ValidateConfirmPhoneAuthenticationAsync(action, verificationCode));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region PasswordResetActions

        /// <summary>
        /// Perform password reset authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="email">
        /// The email address associated to account you want to reset the password for.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.PasswordReset is not used
        /// </exception>
        public async Task<OrionResult<bool>> PerformPasswordResetAuthenticationAsync(AuthenticationAction action, string email)
        {
            if (action.ActionType != AuthenticationActionType.PasswordReset)
            {
                throw new Exception("Must be a password reset authentication action");
            }

            return await this.DoAuthenticationAction(action, new PasswordResetRequirement() { Email = email }, "perform");
        }

        /// <summary>
        /// Perform password reset authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="email">
        /// The email address associated to account you want to reset the password for.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.PasswordReset is not used
        /// </exception>
        public OrionResult<bool> PerformPasswordResetAuthentication(AuthenticationAction action, string email)
        {
            var task = Task.Run(() => this.PerformPasswordResetAuthenticationAsync(action, email));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Validate password reset authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <param name="password">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.PasswordReset is not used
        /// </exception>
        public async Task<OrionResult<bool>> ValidatePasswordResetAuthenticationAsync(AuthenticationAction action, string verificationCode, string password)
        {
            if (action.ActionType != AuthenticationActionType.PasswordReset)
            {
                throw new Exception("Must be a password reset authentication action");
            }

            return await this.DoAuthenticationAction(action, new PasswordResetVerification() { VerificationCode = verificationCode, Password = password }, "validate");
        }

        /// <summary>
        /// Validate password reset authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <param name="password">
        /// The new password.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.PasswordReset is not used
        /// </exception>
        public OrionResult<bool> ValidatePasswordResetAuthentication(AuthenticationAction action, string verificationCode, string password)
        {
            var task = Task.Run(() => this.ValidatePasswordResetAuthenticationAsync(action, verificationCode, password));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Generates an optional password reset action for the account attached to the given email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{AuthenticationAction}"/>.
        /// </returns>
        public async Task<OrionResult<AuthenticationAction>> GeneratePasswordResetActionAsync(string emailAddress)
        {
            var result = await this.Context.Communicator.Get($"Authentication/User/{emailAddress}/PasswordReset");

            try
            {
                result.IsOk(true);
                return new OrionResult<AuthenticationAction>(this.Context.Config.Parser.ParseData<AuthenticationAction>(result.Result));
            }
            catch (Exception)
            {
                return new OrionResult<AuthenticationAction>(null);
            }
        }

        /// <summary>
        /// Generates an optional password reset action for the account attached to the given email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{AuthenticationAction}"/>.
        /// </returns>
        public OrionResult<AuthenticationAction> GeneratePasswordResetAction(string emailAddress)
        {
            var task = Task.Run(() => this.GeneratePasswordResetActionAsync(emailAddress));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region ConfirmEmailActions

        /// <summary>
        /// Perform confirm email authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmEmail is not used
        /// </exception>
        public async Task<OrionResult<bool>> PerformConfirmEmailAuthenticationAsync(AuthenticationAction action, string emailAddress)
        {
            if (action.ActionType != AuthenticationActionType.ConfirmEmail)
            {
                throw new Exception("Must be a confirm email authentication action");
            }

            return await this.DoAuthenticationAction(action, new ConfirmEmailRequirement() { Email = emailAddress }, "perform");
        }

        /// <summary>
        /// Perform confirm email authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmEmail is not used
        /// </exception>
        public OrionResult<bool> PerformConfirmEmailAuthentication(AuthenticationAction action, string emailAddress)
        {
            var task = Task.Run(() => this.PerformConfirmEmailAuthenticationAsync(action, emailAddress));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Validate confirm email authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmEmail is not used
        /// </exception>
        public async Task<OrionResult<bool>> ValidateConfirmEmailAuthenticationAsync(AuthenticationAction action, string verificationCode)
        {
            if (action.ActionType != AuthenticationActionType.ConfirmEmail)
            {
                throw new Exception("Must be a confirm email authentication action");
            }

            return await this.DoAuthenticationAction(action, new ConfirmEmailVerification() { VerificationCode = verificationCode }, "validate");
        }

        /// <summary>
        /// Validate confirm email authentication.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="verificationCode">
        /// The verification code.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws exception if AuthenticationActionType.ConfirmEmail is not used
        /// </exception>
        public OrionResult<bool> ValidateConfirmEmailAuthentication(AuthenticationAction action, string verificationCode)
        {
            var task = Task.Run(() => this.ValidateConfirmEmailAuthenticationAsync(action, verificationCode));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region Registration

        /// <summary>
        /// Register a new Orion user
        /// </summary>
        /// <param name="newUser">
        /// The new users details.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> RegisterUserAsync(UserAccount newUser)
        {
            var apiResult = await this.Context.Communicator.Post(newUser, "Authentication/User/Register");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Register a new Orion user
        /// </summary>
        /// <param name="newUser">
        /// The new users details.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> RegisterUser(UserAccount newUser)
        {
            var task = Task.Run(() => this.RegisterUserAsync(newUser));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Complete User Registration Step
        /// </summary>
        /// <param name="registrationKey">
        /// The registration key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType is not UserRegistrationKey
        /// </exception>
        public async Task<OrionResult<Key>> CompleteUserRegistrationAsync(Key registrationKey)
        {
            if (registrationKey.Type != KeyType.UserRegistrationKey)
            {
                throw new Exception("Only UserRegistrationKey keys can be used to complete registration");
            }

            var apiResult = await this.Context.Communicator.Get($"Authentication/User/Register/Complete/{Uri.EscapeDataString(registrationKey.ApiKey)}");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Complete User Registration Step
        /// </summary>
        /// <param name="registrationKey">
        /// The registration key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType is not UserRegistrationKey
        /// </exception>
        public OrionResult<Key> CompleteUserRegistration(Key registrationKey)
        {
            var task = Task.Run(() => this.CompleteUserRegistrationAsync(registrationKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Generate a new roaming user.
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        /// <remarks>
        /// Roaming users expire after 24 hours. Use the Roaming Ascension action 
        /// to convert a roaming account into an Orion account.
        /// </remarks>
        public async Task<OrionResult<Key>> GenerateRoamingUserAsync()
        {
            var result = await this.Context.Communicator.Get($"Authentication/User/Roaming/Register");

            try
            {
                result.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(result.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Generate a new roaming user.
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        /// <remarks>
        /// Roaming users expire after 24 hours. Use the Roaming Ascension action 
        /// to convert a roaming account into an Orion account.
        /// </remarks>
        public OrionResult<Key> GenerateRoamingUser()
        {
            var task = Task.Run(this.GenerateRoamingUserAsync);
            task.Wait();
            return task.Result;
        }

        #endregion

        #region KeyManagement

        /// <summary>
        /// Pull the latest version of the given key from an Orion API endpoint.
        /// </summary>
        /// <param name="keyToRefresh">
        /// The key to refresh.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> RefreshKeyAsync(Key keyToRefresh)
        {
            var apiResult = await this.Context.Communicator.Get($"Keys/User/{keyToRefresh.ApiKey}");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Pull the latest version of the given key from an Orion API endpoint.
        /// </summary>
        /// <param name="keyToRefresh">
        /// The key to refresh.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> RefreshKey(Key keyToRefresh)
        {
            var task = Task.Run(() => this.RefreshKeyAsync(keyToRefresh));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Generate a system account verification key for the account associated with the attached
        /// authenticator.
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method.
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> GenerateSystemAccountVerificationKeyAsync()
        {
            var apiResult = await this.Context.Communicator.Get("Authentication/Account/VerifyIdentity");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Generate a system account verification key for the account associated with the attached
        /// authenticator.
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method.
        /// </summary>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> GenerateSystemAccountVerificationKey()
        {
            var task = Task.Run(this.GenerateSystemAccountVerificationKeyAsync);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Convert a system account verification key to a system account context.
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method
        /// </summary>
        /// <param name="verificationKey">
        /// The verification key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{SystemAccountContext}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.ApplicationTempKey is not used.
        /// </exception>
        public async Task<OrionResult<SystemAccountContext>> ConvertVerificationKeyToContextAsync(Key verificationKey)
        {
            if (verificationKey.Type != KeyType.ApplicationTempKey)
            {
                throw new Exception("Must be an ApplicationTempKey");
            }

            var apiResult = await this.Context.Communicator.Get($"Authentication/Account/VerifyIdentity/{verificationKey.ApiKey}");

            try
            {
                apiResult.IsOk(true);
                return new OrionResult<SystemAccountContext>(this.Context.Config.Parser.ParseData<SystemAccountContext>(apiResult.Result));
            }
            catch (Exception)
            {
                return new OrionResult<SystemAccountContext>(null);
            }
        }

        /// <summary>
        /// Convert a system account verification key to a system account context.
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method
        /// </summary>
        /// <param name="verificationKey">
        /// The verification key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{SystemAccountContext}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.ApplicationTempKey is not used.
        /// </exception>
        public OrionResult<SystemAccountContext> ConvertVerificationKeyToContext(Key verificationKey)
        {
            var task = Task.Run(() => this.ConvertVerificationKeyToContextAsync(verificationKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Convert a temporary user key to a permanent user key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method
        /// </summary>
        /// <param name="userTempKey">
        /// The user temp key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> ConvertUserTempKeyToUserPermAsync(Key userTempKey)
        {
            var result = await this.Context.Communicator.Get($"Keys/User/{userTempKey.ApiKey}/Convert/To/UserPerm");

            try
            {
                result.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(result.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Convert a temporary user key to a permanent user key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method.
        /// </summary>
        /// <param name="userTempKey">
        /// The user temp key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> ConvertUserTempKeyToUserPerm(Key userTempKey)
        {
            var task = Task.Run(() => this.ConvertUserTempKeyToUserPermAsync(userTempKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Convert a permanent user key to a temporary user key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method.
        /// </summary>
        /// <param name="userPermKey">
        /// The user perm key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public async Task<OrionResult<Key>> ConvertUserPermKeyToUserTempAsync(Key userPermKey)
        {
            var result = await this.Context.Communicator.Get($"Keys/User/{userPermKey.ApiKey}/Convert/To/UserTemp");

            try
            {
                result.IsOk(true);
                return new OrionResult<Key>(this.Context.Config.Parser.ParseData<Key>(result.Result));
            }
            catch (Exception)
            {
                return new OrionResult<Key>(null);
            }
        }

        /// <summary>
        /// Convert a permanent user key to a temporary user key (Your application will need matching permissions)
        /// NOTE: An OrgStandardAuthenticator must be used when calling this method.
        /// </summary>
        /// <param name="userPermKey">
        /// The user perm key.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{Key}"/>.
        /// </returns>
        public OrionResult<Key> ConvertUserPermKeyToUserTemp(Key userPermKey)
        {
            var task = Task.Run(() => this.ConvertUserPermKeyToUserTempAsync(userPermKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Check if the application signing the request has permission to access data associated with the given key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.UserTempKey is not used
        /// </exception>
        public async Task<OrionResult<bool>> DoesApplicationHavePermissionAsync(Key key, string applicationId)
        {
            if (key.Type != KeyType.UserTempKey)
            {
                throw new Exception("Only user temp keys can be used");
            }

            var result = await this.Context.Communicator.Get($"Keys/User/{key.ApiKey}/Permission/{applicationId}");

            try
            {
                return new OrionResult<bool>(result.IsOk(false));
            }
            catch (Exception)
            {
                return new OrionResult<bool>(false);
            }
        }

        /// <summary>
        /// Check if the application signing the request has permission to access data associated with the given key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        /// <returns>
        /// The <see cref="OrionResult{T}"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// Throws an exception if KeyType.UserTempKey is not used
        /// </exception>
        public OrionResult<bool> DoesApplicationHavePermission(Key key, string applicationId)
        {
            var task = Task.Run(() => this.DoesApplicationHavePermissionAsync(key, applicationId));
            task.Wait();
            return task.Result;
        }

        #endregion

        #region AuthenticationActions

        /// <summary>
        /// Performs provided authentication action request with the API.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="payload">
        /// The payload.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<OrionResult<bool>> DoAuthenticationAction(AuthenticationAction action, object payload, string type)
        {
            var apiResult = await this.Context.Communicator.Post(payload, $"Authentication/Action/{action.Id}/{type}");
            return apiResult.StatusCode == HttpStatusCode.OK ? new OrionResult<bool>(true) : new OrionResult<bool>(false);
        }

        #endregion
    }
}
