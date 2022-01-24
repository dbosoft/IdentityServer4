﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using dbosoft.IdentityServer.Storage.Models;
using dbosoft.IdentityServer.Validation.Models;

namespace dbosoft.IdentityServer.Services
{
    /// <summary>
    /// Service to retrieve and update consent.
    /// </summary>
    public interface IConsentService
    {
        /// <summary>
        /// Checks if consent is required.
        /// </summary>
        /// <param name="subject">The user.</param>
        /// <param name="client">The client.</param>
        /// <param name="parsedScopes">The parsed scopes.</param>
        /// <returns>
        /// Boolean if consent is required.
        /// </returns>
        Task<bool> RequiresConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<ParsedScopeValue> parsedScopes);

        /// <summary>
        /// Updates the consent.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="client">The client.</param>
        /// <param name="parsedScopes">The parsed scopes.</param>
        /// <returns></returns>
        Task UpdateConsentAsync(ClaimsPrincipal subject, Client client, IEnumerable<ParsedScopeValue> parsedScopes);
    }
}