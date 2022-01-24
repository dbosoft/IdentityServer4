﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using dbosoft.IdentityServer.ResponseHandling;
using dbosoft.IdentityServer.ResponseHandling.Models;
using dbosoft.IdentityServer.Validation.Models;

namespace IdentityServer.UnitTests.Common
{
    internal class StubAuthorizeResponseGenerator : IAuthorizeResponseGenerator
    {
        public AuthorizeResponse Response { get; set; } = new AuthorizeResponse();

        public Task<AuthorizeResponse> CreateResponseAsync(ValidatedAuthorizeRequest request)
        {
            return Task.FromResult(Response);
        }
    }
}