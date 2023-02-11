using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

using R5T.Magyar;
using R5T.Magyar.Extensions;

using R5T.D0064;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class IMvcBuilderExtensions
    {
        public static async Task<IMvcBuilder> AddRazorRuntimeCompilation(this Task<IMvcBuilder> gettingBuilder, IMvcRazorRuntimeCompilationOptionsConfigurer configurer,
            Action<MvcRazorRuntimeCompilationOptions> configureAction,
            IEnumerable<string> applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths)
        {
            var builder = await gettingBuilder;

            await configurer.ConfigureMvcRazorRuntimeCompilationOptions(builder,
                configureAction,
                applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths);

            return builder;
        }

        public static Task<IMvcBuilder> AddRazorRuntimeCompilation(this IMvcBuilder builder, IMvcRazorRuntimeCompilationOptionsConfigurer configurer,
            Action<MvcRazorRuntimeCompilationOptions> configureAction,
            IEnumerable<string> applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths)
        {
            return builder
                .AsTask()
                .AddRazorRuntimeCompilation(configurer,
                    configureAction,
                    applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths);
        }

        public static Task<IMvcBuilder> AddRazorRuntimeCompilation(this IMvcBuilder builder, IMvcRazorRuntimeCompilationOptionsConfigurer configurer,
            IEnumerable<string> applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths)
        {
            return builder.AddRazorRuntimeCompilation(configurer,
                ActionHelper.DoNothing,
                applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths);
        }
    }
}
