using System.Threading.Tasks;
using Dbosoft.IdentityServer.Validation.Contexts;

namespace Dbosoft.IdentityServer.Validation.Default
{
    /// <summary>
    /// No-op client configuration validator (for backwards-compatibility).
    /// </summary>
    /// <seealso cref="IClientConfigurationValidator" />
    public class NopClientConfigurationValidator : IClientConfigurationValidator
    {
        /// <summary>
        /// Determines whether the configuration of a client is valid.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Task ValidateAsync(ClientConfigurationValidationContext context)
        {
            context.IsValid = true;
            return Task.CompletedTask;
        }
    }
}