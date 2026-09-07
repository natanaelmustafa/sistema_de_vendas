using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.ListarPedidosPagamentosPorStatus {
    public sealed class ListarPedidosPagamentosPorStatusQuery {
        public StatusPagamento Status { get; }

        public ListarPedidosPagamentosPorStatusQuery(StatusPagamento status) {
            // Sem validação manual — o ASP.NET Core já rejeita
            // valores inválidos antes de chegar aqui
            Status = status;
        }
    }
}
