using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Infra.Extensions {
    public static class ApplicationExtensions {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
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
