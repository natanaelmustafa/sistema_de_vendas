using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queries.Pedido.DTO;

namespace Vendas.Application.Queries.Pedido.ListarPedidosResumoPorCliente {
    public sealed class ListarPedidosResumoPorClienteQueryHandler {
        private readonly IPedidoQueryRepository _queryRepo;

        public ListarPedidosResumoPorClienteQueryHandler(IPedidoQueryRepository queryRepo) =>
            _queryRepo = queryRepo;

        public async Task<IReadOnlyList<PedidoResumoDto>> HandleAsync(
            ListarPedidosResumoPorClienteQuery query,
            CancellationToken ct = default)
            => await _queryRepo.ListarResumoPorClienteAsync(query.ClienteId, ct);
    }
}
