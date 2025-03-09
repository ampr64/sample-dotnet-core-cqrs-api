using Autofac;

namespace SampleProject.Infrastructure.Caching;

public class CachingModule(Dictionary<string, TimeSpan> expirationConfiguration) : Module
{
    private readonly Dictionary<string, TimeSpan> _expirationConfiguration = expirationConfiguration;

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<MemoryCacheStore>()
            .As<ICacheStore>()
            .WithParameter("expirationConfiguration", _expirationConfiguration)
            .SingleInstance();
    }
}