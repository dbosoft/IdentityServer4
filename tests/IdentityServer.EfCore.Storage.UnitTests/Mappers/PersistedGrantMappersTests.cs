﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Dbosoft.IdentityServer.EfCore.Storage.Mappers;
using Dbosoft.IdentityServer.Storage.Models;
using FluentAssertions;
using Xunit;

namespace Dbosoft.IdentityServer.EfCore.Storage.UnitTests.Mappers
{
    public class PersistedGrantMappersTests
    {
        [Fact]
        public void PersistedGrantAutomapperConfigurationIsValid()
        {
            PersistedGrantMappers.Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void CanMap()
        {
            var model = new PersistedGrant()
            {
                ConsumedTime = new System.DateTime(2020, 02, 03, 4, 5, 6)
            };
            
            var mappedEntity = model.ToEntity();
            mappedEntity.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));
            
            var mappedModel = mappedEntity.ToModel();
            mappedModel.ConsumedTime.Value.Should().Be(new System.DateTime(2020, 02, 03, 4, 5, 6));

            Assert.NotNull(mappedModel);
            Assert.NotNull(mappedEntity);
        }
    }
}