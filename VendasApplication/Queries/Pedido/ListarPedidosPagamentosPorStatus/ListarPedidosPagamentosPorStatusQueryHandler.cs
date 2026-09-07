using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queries.Pedido.DTO;

namespace Vendas.Application.Queries.Pedido.ListarPedidosPagamentosPorStatus {
    public sealed class ListarPedidosPagamentosPorStatusQueryHandler {
        private readonly IPedidoQueryRepository _queryRepo;

        public ListarPedidosPagamentosPorStatusQueryHandler(IPedidoQueryRepository queryRepo)
            => _queryRepo = queryRepo;

        public async Task<IReadOnlyList<PagamentoPorStatusDto>> HandleAsync(
            ListarPedidosPagamentosPorStatusQuery query,
            CancellationToken ct = default)
            => await _queryRepo.ListarPagamentosPorStatusAsync(query.Status, ct);
    }
}
