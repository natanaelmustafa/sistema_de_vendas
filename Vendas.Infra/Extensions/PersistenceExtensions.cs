using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Infra.Persistence.Context;
using Vendas.Infra.Repositories;

namespace Vendas.Infra.Extensions {
    public static class PersistenceExtensions {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection") ?? "Data Source=vendas.db";

            services.AddDbContext<VendasDbContext>(options =>
                options.UseSqlite(connectionString));

            // Substitui o FakePedidoRepository pelo repositório real.
            services.AddScoped<IPedidoRepository, PedidoRepository>();

            return services;
        }
    }
}
