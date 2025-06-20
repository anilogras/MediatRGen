﻿using MediatRGen.Core.Base;
using MediatRGen.Core.Concrete;
using MediatRGen.Core.Concrete.MediatR;
using MediatRGen.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Extensions
{
    public static class DependencyInjectionConfigs
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IArgsService, ArgsService>();
            services.AddTransient<IClassLibraryService, ClassLibraryService>();
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IConfigService, ConfigService>();
            services.AddTransient<IConstructorService, ConstructorService>();
            services.AddTransient<ICoreServices, CoreServices>();
            services.AddTransient<IDirectoryServices, DirectoryServices>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IInheritanceService, InheritanceService>();
            services.AddTransient<INameSpaceService, NameSpaceService>();
            services.AddTransient<INugetService, NugetService>();
            services.AddTransient<IParameterService, ParameterService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<ISystemProcessService, SystemProcessService>();
            services.AddTransient<ISolutionService, SolutionService>();
            services.AddTransient<IUsingService, UsingService>();
            services.AddTransient<IWebApiService, WebApiService>();
            services.AddTransient<ISettings, Settings>();
            services.AddTransient<ICoreServices, CoreServices>();
            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<IMediatRService, MediatRService>();
            services.AddTransient<IMethodService, MethodService>();

            return services;
        }
    }
}
