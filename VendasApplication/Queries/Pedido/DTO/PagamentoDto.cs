using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.DTO {
    public sealed class PagamentoDto {
        public Guid PagamentoId { get; init; }
        public string MetodoPagamento { get; init; } = string.Empty;
        public string StatusPagamento { get; init; } = string.Empty;
        public decimal Valor { get; init; }
        public string? CodigoTransacao { get; init; }
        public DateTime? DataPagamento { get; init; }
    }
}
