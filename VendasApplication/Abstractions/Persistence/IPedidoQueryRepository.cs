using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Application.Queries.Pedido.DTO;

namespace Vendas.Application.Abstractions.Persistence {
    public interface IPedidoQueryRepository {
        Task<IReadOnlyList<PedidoResumoDto>> ListarResumoAsync(CancellationToken ct = default);

        Task<IReadOnlyList<PedidoResumoDto>> ListarResumoPorClienteAsync(Guid clienteId,
            CancellationToken ct = default);

        Task<IReadOnlyList<PagamentoPorStatusDto>> ListarPagamentosPorStatusAsync(
            StatusPagamento status, CancellationToken ct = default);

        Task<PedidoCompletoDto?> ObterPedidoCompletoPorIdAsync(Guid pedidoId, CancellationToken ct =
            default);
    }
}
