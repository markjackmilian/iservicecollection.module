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
    /// <param name="module"></param>
    public static void AddModule(this IServiceCollection services,IModule module)
    {
        module.Load(services);
    }
}

