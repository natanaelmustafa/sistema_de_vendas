using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queries.Pedido.DTO;

namespace Vendas.Application.Queries.Pedido.ListarPedidosResumo {
    public sealed class ListarPedidosResumoQueryHandler {
        private readonly IPedidoQueryRepository _queryRepo;

        public ListarPedidosResumoQueryHandler(IPedidoQueryRepository queryRepo)
            => _queryRepo = queryRepo;

        public async Task<IReadOnlyList<PedidoResumoDto>> HandleAsync(
            ListarPedidosResumoQuery query,
            CancellationToken ct = default)
            => await _queryRepo.ListarResumoAsync(ct);
    }
}
