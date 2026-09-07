using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Infra.Fakes;

namespace Vendas.Infra.Extensions {
    public static class IntegrationExtensions {
        public static IServiceCollection AddFakeIntegration(this IServiceCollection services) {
            services.AddSingleton<ICatalogoGateway, FakeCatalogoGateway>();
            services.AddSingleton<IClientesGateway, FakeClientesGateway>();

            services.AddSingleton<CatalogoAcl>();
            services.AddSingleton<ClientesAcl>();

            return services;
        }
    }
}
