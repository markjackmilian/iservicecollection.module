﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace iservicecollection.module.core;

public static class Extensions
{
    /// <summary>
    /// Add a module to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="T"></typeparam>
    public static void AddModule<T>(this IServiceCollection services) where T : class, IModule
    {
        var module = Activator.CreateInstance<T>();
        module.Load(services);
    }
    
    /// <summary>
    /// Add a module to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleType"></param>
    public static void AddModule(this IServiceCollection services, Type? moduleType) 
    {
        if(moduleType == null) return;
        var module = Activator.CreateInstance(moduleType) as IModule;
        module?.Load(services);
    }


    /// <summary>
    /// Add a module to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="module"></param>
    public static void AddModule(this IServiceCollection services,IModule? module)
    {
        module?.Load(services);
    }


    /// <summary>
    /// Scan assemblies for modules and add them to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    public static void AddModulesFromAssemblies(this IServiceCollection services, params Assembly[]? assemblies)
    {
        if(assemblies == null) return;
        
        var types = assemblies.SelectMany(assembly =>
        {
            var scanner = new ModuleScanner(assembly);
            return scanner.GetModules();
        });

        foreach (var type in types)
        {
            services.AddModule(type);
        }
    }
}

