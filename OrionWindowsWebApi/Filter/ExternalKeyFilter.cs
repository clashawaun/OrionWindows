// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalKeyFilter.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   Defines the ExternalKeyFilter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    using Newtonsoft.Json;

    using OrionWindows;
    using OrionWindows.Entities.Authentication;
    using OrionWindows.Entities.Core;
    using OrionWindows.Entities.User;

    using OrionWindowsDesktop;
    using OrionWindowsDesktop.Authenticator;
    using OrionWindowsDesktop.Logging;

    using OrionWindowsWebApi.Entity;
    using OrionWindowsWebApi.Principal;

    /// <summary>
    /// The external key filter for automatic Orion Authorization header processing.
    /// </summary>
    public class ExternalKeyFilter : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// The scheme.
        /// </summary>
        private const string Scheme = "OrionKey";

        /// <summary>
        /// Gets the organisation public key.
        /// </summary>
        private readonly string organisationPublicKey;

        /// <summary>
        /// System account public key.
        /// </summary>
        private readonly string publicKey;

        /// <summary>
        /// System account secret key.
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// Should roaming user keys be accepted.
        /// </summary>
        private readonly bool allowRoamingUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalKeyFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key</param>
        /// <param name="secretKey">
        /// A system account secret key
        /// </param>
        /// <param name="organisationPublicKey">
        /// Application public key</param>
        /// <param name="isOptional">
        /// Is authentication optional.
        /// </param>
        public ExternalKeyFilter(string publicKey, string secretKey, string organisationPublicKey, bool isOptional)
        {
            this.AllowMultiple = true;
            this.IsOptional = isOptional;
            this.organisationPublicKey = organisationPublicKey;
            this.publicKey = publicKey;
            this.secretKey = secretKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalKeyFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key</param>
        /// <param name="secretKey">
        /// A system account secret key
        /// </param>
        /// <param name="organisationPublicKey">
        /// Application public key</param>
        /// <param name="isOptional">
        /// Is authentication optional.
        /// </param>
        /// <param name="allowRoamingUser">
        /// Should roaming user keys be accepted</param>
        public ExternalKeyFilter(string publicKey, string secretKey, string organisationPublicKey, bool isOptional, bool allowRoamingUser)
        {
            this.AllowMultiple = true;
            this.IsOptional = isOptional;
            this.organisationPublicKey = organisationPublicKey;
            this.publicKey = publicKey;
            this.secretKey = secretKey;
            this.allowRoamingUser = allowRoamingUser;
        }

        /// <summary>
        /// Gets a value indicating whether allow multiple.
        /// </summary>
        public bool AllowMultiple { get; }

        /// <summary>
        /// Gets a value indicating whether is optional.
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// Called when a request hits a filtered web API action.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // Ensure that the auth header and scheme have been set
            if (context.Request.Headers.Authorization?.Parameter == null || context.Request.Headers.Authorization.Scheme == null)
            {
                this.HandleFailCase(context);
                return;
            }

            // Check that the correct scheme was used.
            if (!context.Request.Headers.Authorization.Scheme.Equals(Scheme))
            {
                this.HandleFailCase(context);
                return;
            }

            var orionContext = new Orion(new DeleteMeLogger())
            {
                Communicator =
                {
                    ApiAuthenticator = new OrgStandardAuthenticator()
                    {
                        PublicKey = new Key() { ApiKey = this.publicKey, Type = KeyType.ApplicationPublicKey },
                        SecertKey = new Key() { ApiKey = this.secretKey, Type = KeyType.ApplicationSecretKey }
                    }
                }
            };

            // Call the Orion API and request the user information bound to the key!
            var profile = await orionContext.CreateUserController().GetUserProfileAsync(new Key() { ApiKey = context.Request.Headers.Authorization.Parameter, Type = KeyType.UserTempKey }, this.organisationPublicKey, this.allowRoamingUser);
            if (profile.Result != null)
            {
                var principal = new OrionPrincipal(context.Request.Headers.Authorization.Parameter)
                {
                    User = profile.Result
                };
                context.Principal = principal;
            }
            else
            {
                this.HandleFailCase(context);
            }
        }

        /// <summary>
        /// The challenge a sync.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// N / A
        /// </exception>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// The handle fail case.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void HandleFailCase(HttpAuthenticationContext context)
        {
            if (this.IsOptional)
            {
                context.Principal = new OrionPrincipal("optional");
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Authentication failed", context.Request);
            }
        }
    }
}
