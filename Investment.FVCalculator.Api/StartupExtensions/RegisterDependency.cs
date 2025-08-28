using FluentValidation;
using Investment.FVCalculator.Api.Services;
using Investment.FVCalculator.Common.Models;
using Investment.FVCalculator.Common.Validators;

namespace Investment.FVCalculator.Api.StartupExtensions
{
    internal static class RegisterDependency
    {
        internal static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMultiAssetsFVServices, MultiAssetsFVServices>();
            services.AddScoped<IValidator<Asset>, AssetRequestValidators>();
        }
    }
}
