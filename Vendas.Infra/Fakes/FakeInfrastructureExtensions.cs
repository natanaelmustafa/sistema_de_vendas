using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Infra.Fakes {
    public static class FakeInfrastructureExtensions {
        public static IServiceCollection AddFakeInfrastructure(this IServiceCollection services) {
            // — Repositórios — // Singleton: os dados persistem durante toda a execução da app
            services.AddSingleton<FakePedidoRepository>();
            services.AddSingleton<IPedidoRepository>(sp => sp.GetRequiredService<FakePedidoRepository>());

            // — Gateways (Supporting Contexts simulados) — // Singleton: dados de catálogo e clientes são imutáveis no Fake
            services.AddSingleton<ICatalogoGateway, FakeCatalogoGateway>();
            services.AddSingleton<IClientesGateway, FakeClientesGateway>();

            // — ACLs (Anti-Corruption Layers) — // Singleton: sem estado, apenas lógica de tradução
            services.AddSingleton<CatalogoAcl>();
            services.AddSingleton<ClientesAcl>();

            // — Command Handlers — // Scoped: ciclo de vida por requisição HTTP (padrão para handlers)
            services.AddScoped<CriarPedidoCommandHandler>();
            services.AddScoped<AdicionarItemAoPedidoCommandHandler>();
            services.AddScoped<IniciarPagamentoCommandHandler>();
            services.AddScoped<MarcarPedidoComoEnviadoCommandHandler>();
            services.AddScoped<MarcarPedidoComoEntregueCommandHandler>();
            services.AddScoped<CancelarPedidoCommandHandler>();
            services.AddScoped<MarcarPedidoComoEmSeparacaoCommandHandler>();

            return services;
        }
    }
}
