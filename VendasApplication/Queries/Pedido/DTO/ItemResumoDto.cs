using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.DTO {
    public sealed class ItemResumoDto {
        public Guid ProdutoId { get; init; }
        public string NomeProduto { get; init; } = string.Empty;
        public decimal PrecoUnitario { get; init; }
        public int Quantidade { get; init; }
        public decimal ValorTotal { get; init; }
    }
}
