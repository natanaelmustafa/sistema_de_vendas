using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.ObterPedidoCompletoPorId {
    public sealed class ObterPedidoCompletoPorIdQuery {
        public Guid PedidoId { get; }

        public ObterPedidoCompletoPorIdQuery(Guid pedidoId) {
            if (pedidoId == Guid.Empty)
                throw new ArgumentException("PedidoId inválido.", nameof(pedidoId));

            PedidoId = pedidoId;
        }
    }
}
