// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExternalSystemAccountFilter.cs" company="Shane Craven">
//   Copyright (c) Shane Craven. All rights reserved.
// </copyright>
// <summary>
//   The external system account filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OrionWindowsWebApi.Filter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    using OrionWindows.Core.Controller;
    using OrionWindows.Entities.Authentication;

    using OrionWindowsDesktop;
    using OrionWindowsDesktop.Authenticator;
    using OrionWindowsDesktop.Logging;

    using OrionWindowsWebApi.Entity;
    using OrionWindowsWebApi.Principal;

    /// <summary>
    /// The external system account filter.
    /// </summary>
    public class ExternalSystemAccountFilter : Attribute, IAuthenticationFilter
    {
        /// <summary>
        /// The scheme.
        /// </summary>
        private const string Scheme = "OrionKey";

        /// <summary>
        /// A system account public key.
        /// </summary>
        private readonly string publicKey;

        /// <summary>
        /// A system account secret key.
        /// </summary>
        private readonly string secretKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalSystemAccountFilter"/> class.
        /// </summary>
        /// <param name="publicKey">
        /// A system account public key</param>
        /// <param name="secretKey">
        /// A system account secret key</param>
        /// <param name="isOptional">
        /// Is authentication optional.
        /// </param>
        public ExternalSystemAccountFilter(string publicKey, string secretKey, bool isOptional)
        {
            this.AllowMultiple = true;
            this.IsOptional = isOptional;
            this.publicKey = publicKey;
            this.secretKey = secretKey;
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

            var systemAccountContext = await orionContext.CreateAuthenticationController().ConvertVerificationKeyToContextAsync(new Key() { ApiKey = context.Request.Headers.Authorization.Parameter, Type = KeyType.ApplicationTempKey });

            if (systemAccountContext.Result != null)
            {
                var principal = new OrionSystemAccountPrincipal(context.Request.Headers.Authorization.Parameter)
                {
                   Context = systemAccountContext.Result
                };
                context.Principal = principal;
            }
            else
            {
                this.HandleFailCase(context);
            }

        }

        /// <summary>
        /// The challenge async.
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
        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Handle failed system account authentication.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private void HandleFailCase(HttpAuthenticationContext context)
        {
            if (this.IsOptional)
            {
                context.Principal = new OrionSystemAccountPrincipal("optional");
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult("Authentication failed", context.Request);
            }
        }
    }
}
