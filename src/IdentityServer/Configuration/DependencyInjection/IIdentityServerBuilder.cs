﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.Extensions.DependencyInjection;

namespace Dbosoft.IdentityServer.Configuration.DependencyInjection
{
    /// <summary>
    /// IdentityServer builder Interface
    /// </summary>
    public interface IIdentityServerBuilder
    {
        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        IServiceCollection Services { get; }
    }
}