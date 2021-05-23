using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;
using R5T.Lombardy;

using R5T.D0062;
using R5T.D0068;


namespace R5T.D0064
{
    public static class IServiceCollectioneExtensions
    {
        /// <summary>
        /// Adds the <see cref="MvcRazorRuntimeCompilationOptionsConfigurer"/> implementation of <see cref="IMvcRazorRuntimeCompilationOptionsConfigurer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceCollection AddMvcRazorRuntimeCompilationOptionsConfigurer(this IServiceCollection services,
            IServiceAction<IApplicationProjectDirectoryPathProvider> applicationProjectDirectoryPathProviderAction,
            IServiceAction<IEnvironmentNameProvider> environmentNameProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            services
                .AddSingleton<IMvcRazorRuntimeCompilationOptionsConfigurer, MvcRazorRuntimeCompilationOptionsConfigurer>()
                .Run(applicationProjectDirectoryPathProviderAction)
                .Run(environmentNameProviderAction)
                .Run(stringlyTypedPathOperatorAction)
                ;

            return services;
        }

        /// <summary>
        /// Adds the <see cref="MvcRazorRuntimeCompilationOptionsConfigurer"/> implementation of <see cref="IMvcRazorRuntimeCompilationOptionsConfigurer"/> as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        public static IServiceAction<IMvcRazorRuntimeCompilationOptionsConfigurer> AddMvcRazorRuntimeCompilationOptionsConfigurerAction(this IServiceCollection services,
            IServiceAction<IApplicationProjectDirectoryPathProvider> applicationProjectDirectoryPathProviderAction,
            IServiceAction<IEnvironmentNameProvider> environmentNameProviderAction,
            IServiceAction<IStringlyTypedPathOperator> stringlyTypedPathOperatorAction)
        {
            var serviceAction = ServiceAction.New<IMvcRazorRuntimeCompilationOptionsConfigurer>(() => services.AddMvcRazorRuntimeCompilationOptionsConfigurer(
                applicationProjectDirectoryPathProviderAction,
                environmentNameProviderAction,
                stringlyTypedPathOperatorAction));

            return serviceAction;
        }
    }
}
