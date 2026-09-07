using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Abstractions.Persistence;
using Vendas.Application.Queries.Pedido.DTO;

namespace Vendas.Application.Queries.Pedido.ObterPedidoCompletoPorId {
    public sealed class ObterPedidoCompletoPorIdQueryHandler {
        private readonly IPedidoQueryRepository _queryRepo;

        public ObterPedidoCompletoPorIdQueryHandler(IPedidoQueryRepository queryRepo)
            => _queryRepo = queryRepo;

        public async Task<PedidoCompletoDto?> HandleAsync(ObterPedidoCompletoPorIdQuery query,
            CancellationToken ct = default)
            => await _queryRepo.ObterPedidoCompletoPorIdAsync(query.PedidoId, ct);
    }
}
