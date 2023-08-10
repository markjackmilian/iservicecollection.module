using Microsoft.Extensions.DependencyInjection;

namespace iservicecollection.module.core;

public interface IModule
{
    void Load(IServiceCollection services);
}