using System.Reflection;

namespace iservicecollection.module.core;

public class ModuleScanner
{
    private readonly Assembly _assembly;

    public ModuleScanner(Assembly assembly)
    {
        _assembly = assembly;
    }

    public IEnumerable<Type> GetModules()
    {
        return _assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.GetInterfaces().Contains(typeof(IModule)));
    }
}