// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Threading.Tasks;
using Dbosoft.IdentityServer.Configuration.DependencyInjection.Options;
using Dbosoft.IdentityServer.Models.Messages;
using Dbosoft.IdentityServer.ResponseHandling.Models;
using Dbosoft.IdentityServer.Services;
using Dbosoft.IdentityServer.Storage.Models;
using Dbosoft.IdentityServer.UnitTests.Common;
using Dbosoft.IdentityServer.Validation.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Xunit;
using static IdentityModel.OidcConstants;

namespace Dbosoft.IdentityServer.UnitTests.ResponseHandling.AuthorizeInteractionResponseGenerator
{
    public class CustomAuthorizeInteractionResponseGenerator : IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator
    {
        public CustomAuthorizeInteractionResponseGenerator(ISystemClock clock, ILogger<IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator> logger, IConsentService consent, IProfileService profile) : base(clock, logger, consent, profile)
        {
        }

        public InteractionResponse ProcessLoginResponse { get; set; }
        protected internal override Task<InteractionResponse> ProcessLoginAsync(ValidatedAuthorizeRequest request)
        {
            if (ProcessLoginResponse != null)
            {
                return Task.FromResult(ProcessLoginResponse);
            }

            return base.ProcessLoginAsync(request);
        }

        public InteractionResponse ProcessConsentResponse { get; set; }
        protected internal override Task<InteractionResponse> ProcessConsentAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            if (ProcessConsentResponse != null)
            {
                return Task.FromResult(ProcessConsentResponse);
            }
            return base.ProcessConsentAsync(request, consent);
        }
    }

    public class AuthorizeInteractionResponseGeneratorTests_Custom
    {
        private IdentityServerOptions _options = new IdentityServerOptions();
        private CustomAuthorizeInteractionResponseGenerator _subject;
        private MockConsentService _mockConsentService = new MockConsentService();
        private StubClock _clock = new StubClock();

        public AuthorizeInteractionResponseGeneratorTests_Custom()
        {
            _subject = new CustomAuthorizeInteractionResponseGenerator(
                _clock,
                TestLogger.Create<IdentityServer.ResponseHandling.Default.AuthorizeInteractionResponseGenerator>(),
                _mockConsentService,
                new MockProfileService());
        }


        [Fact]
        public async Task ProcessInteractionAsync_with_overridden_login_returns_redirect_should_return_redirect()
        {
            var request = new ValidatedAuthorizeRequest
            {
                ClientId = "foo",
                Subject = new IdentityServerUser("123")
                {
                    IdentityProvider = IdentityServerConstants.LocalIdentityProvider
                }.CreatePrincipal(),
                Client = new Client
                {
                },
            };

            _subject.ProcessLoginResponse = new InteractionResponse
            {
                RedirectUrl = "/custom"
            };

            var result = await _subject.ProcessInteractionAsync(request);

            result.IsRedirect.Should().BeTrue();
            result.RedirectUrl.Should().Be("/custom");
        }

        [Fact]
        public async Task ProcessInteractionAsync_with_prompt_none_and_login_returns_login_should_return_error()
        {
            var request = new ValidatedAuthorizeRequest
            {
                ClientId = "foo",
                Subject = new IdentityServerUser("123")
                {
                    IdentityProvider = IdentityServerConstants.LocalIdentityProvider
                }.CreatePrincipal(),
                Client = new Client
                {
                },
                PromptModes = new[] { PromptModes.None },
            };

            _subject.ProcessLoginResponse = new InteractionResponse
            {
                IsLogin = true
            };

            var result = await _subject.ProcessInteractionAsync(request);

            result.IsError.Should().BeTrue();
            result.Error.Should().Be("login_required");
        }

        [Fact]
        public async Task ProcessInteractionAsync_with_prompt_none_and_login_returns_redirect_should_return_error()
        {
            var request = new ValidatedAuthorizeRequest
            {
                ClientId = "foo",
                Subject = new IdentityServerUser("123")
                {
                    IdentityProvider = IdentityServerConstants.LocalIdentityProvider
                }.CreatePrincipal(),
                Client = new Client
                {
                },
                PromptModes = new[] { PromptModes.None },
            };

            _subject.ProcessLoginResponse = new InteractionResponse
            {
                RedirectUrl = "/custom"
            };

            var result = await _subject.ProcessInteractionAsync(request);

            result.IsError.Should().BeTrue();
            result.Error.Should().Be("interaction_required");
            result.RedirectUrl.Should().BeNull();
        }

        [Fact]
        public async Task ProcessInteractionAsync_with_prompt_none_and_consent_returns_consent_should_return_error()
        {
            var request = new ValidatedAuthorizeRequest
            {
                ClientId = "foo",
                Subject = new IdentityServerUser("123")
                {
                    IdentityProvider = IdentityServerConstants.LocalIdentityProvider
                }.CreatePrincipal(),
                Client = new Client
                {
                },
                PromptModes = new[] { PromptModes.None },
            };

            _subject.ProcessConsentResponse = new InteractionResponse
            {
                IsConsent = true
            };

            var result = await _subject.ProcessInteractionAsync(request);

            result.IsError.Should().BeTrue();
            result.Error.Should().Be("consent_required");
        }
    }
}
