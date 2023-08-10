
using System.Reflection;
using iservicecollection.module.core;
using Microsoft.Extensions.DependencyInjection;

namespace iservicecollection.module.tests;

public class ServiceCollectionShould
{
    [Fact]
    public void AddModuleUsingGeneric()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddModule<TestModule>();
        
        Assert.Contains(serviceCollection, a => a.ServiceType == typeof(ITest));
    }
    
    [Fact]
    public void AddModuleUsingType()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddModule(typeof(TestModule));
        
        Assert.Contains(serviceCollection, a => a.ServiceType == typeof(ITest));
    }
    
    [Fact]
    public void AddModuleUsingInstance()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddModule(new TestModule());
        
        Assert.Contains(serviceCollection, a => a.ServiceType == typeof(ITest));
    }
    
    [Fact]
    public void AddModuleUsingAssemblyScan()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddModulesFromAssemblies(typeof(ServiceCollectionShould).Assembly);
        
        Assert.Contains(serviceCollection, a => a.ServiceType == typeof(ITest));
    }
    
   
}


class TestModule : IModule
{
    public void Load(IServiceCollection services)
    {
        services.AddTransient<ITest, Test>();
    }
}

interface ITest
{
    string Name { get; }
}

class Test : ITest
{
    public string Name => "Test";
}