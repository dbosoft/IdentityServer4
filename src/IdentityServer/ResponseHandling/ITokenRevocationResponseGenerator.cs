﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using dbosoft.IdentityServer.ResponseHandling.Models;
using dbosoft.IdentityServer.Validation.Models;

namespace dbosoft.IdentityServer.ResponseHandling
{
    /// <summary>
    /// Interface for the userinfo response generator
    /// </summary>
    public interface ITokenRevocationResponseGenerator
    {
        /// <summary>
        /// Creates the revocation endpoint response and processes the revocation request.
        /// </summary>
        /// <param name="validationResult">The userinfo request validation result.</param>
        /// <returns></returns>
        Task<TokenRevocationResponse> ProcessAsync(TokenRevocationRequestValidationResult validationResult);
    }
}