using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.DTO {
    public sealed class PedidoResumoDto {
        public Guid Id { get; init; }
        public string NumeroPedido { get; init; } = string.Empty;
        public Guid ClienteId { get; init; }
        public decimal ValorTotal { get; init; }
        public string StatusPedido { get; init; } = string.Empty;
        public DateTime DataCriacao { get; init; }
        public int TotalItens { get; init; }
        public int TotalPagamentos { get; init; }
    }
}
