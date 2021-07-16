using Autofac;
using PipefittersSupplyCompany.Core.Interfaces;
using PipefittersSupplyCompany.Core.Services;

namespace PipefittersSupplyCompany.Core
{
    public class DefaultCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemSearchService>()
                .As<IToDoItemSearchService>().InstancePerLifetimeScope();
        }
    }
}
