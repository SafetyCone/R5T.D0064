﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

using R5T.T0064;


namespace R5T.D0064
{
    [ServiceDefinitionMarker,]
    public interface IMvcRazorRuntimeCompilationOptionsConfigurer : IServiceDefinition
    {
        Task<IMvcBuilder> ConfigureMvcRazorRuntimeCompilationOptions(IMvcBuilder builder, Action<MvcRazorRuntimeCompilationOptions> configureAction,
            IEnumerable<string> applicationProjectDirectoryRelativeRazorClassLibraryProjectDirectoryPaths); // Named for project to avoid needing to remember how a content root directory relates to a project directory. Knowing the root makes it easier to figure out how to make relative paths to the Razor class library project directories.
    }
}
