using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

using R5T.Lombardy;
using R5T.Magyar;

using R5T.D0062;
using R5T.D0068;
using R5T.T0064;


namespace R5T.D0064
{
    /// <summary>
    /// Only if the environment is named <see cref="T0019.EnvironmentNames.Development"/> is Razor runtime compilation added.
    /// </summary>
    [ServiceImplementationMarker]
    public class MvcRazorRuntimeCompilationOptionsConfigurer : IMvcRazorRuntimeCompilationOptionsConfigurer, IServiceImplementation
    {
        private IApplicationProjectDirectoryPathProvider ApplicationProjectDirectoryPathProvider { get; }
        private IEnvironmentNameProvider EnvironmentNameProvider { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }


        public MvcRazorRuntimeCompilationOptionsConfigurer(
            IApplicationProjectDirectoryPathProvider applicationProjectDirectoryPathProvider,
            IEnvironmentNameProvider environmentNameProvider,
            IStringlyTypedPathOperator stringlyTypedPathOperator)
        {
            this.ApplicationProjectDirectoryPathProvider = applicationProjectDirectoryPathProvider;
            this.EnvironmentNameProvider = environmentNameProvider;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
        }

        public async Task<IMvcBuilder> ConfigureMvcRazorRuntimeCompilationOptions(IMvcBuilder builder, Action<MvcRazorRuntimeCompilationOptions> configureAction,
            IEnumerable<string> applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths)
        {
            var isDevelopment = await this.EnvironmentNameProvider.IsDevelopment();
            if(isDevelopment)
            {
                // Put this await outside the AddRazorRuntimeCompilation() call to avoid an async lambda.
                var applicationProjectDirectoryPath = await this.ApplicationProjectDirectoryPathProvider.GetApplicationProjectDirectoryPath();

                builder.AddRazorRuntimeCompilation(mvcRazorRuntimeCompilationOptions =>
                {
                    // Run the provided configure action.
                    configureAction(mvcRazorRuntimeCompilationOptions);

                    // Now add all Razor Class Library project directory paths
                    applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths.ForEach(relativeRazorClassLibraryProjectDirectoryPath =>
                    {
                        var razorClassLibraryProjectDirectoryPath = this.StringlyTypedPathOperator.Combine(applicationProjectDirectoryPath, relativeRazorClassLibraryProjectDirectoryPath);

                        mvcRazorRuntimeCompilationOptions.AddRazorClassLibraryRuntimeCompilation(razorClassLibraryProjectDirectoryPath);
                    });
                });
            }

            return builder;
        }
    }
}
