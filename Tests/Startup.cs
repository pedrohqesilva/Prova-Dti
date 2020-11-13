using Infrastructure.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            InjectorContainer.Register(services, null);
        }
    }
}