﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Threading.Tasks;
using Dbosoft.IdentityServer.Models;

namespace Dbosoft.IdentityServer.Stores
{
    /// <summary>
    /// Interface for the validation key store
    /// </summary>
    public interface IValidationKeysStore
    {
        /// <summary>
        /// Gets all validation keys.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync();
    }
}