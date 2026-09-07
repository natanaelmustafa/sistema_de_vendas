using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Queries.Pedido.ListarPedidosResumoPorCliente {
    public sealed class ListarPedidosResumoPorClienteQuery {
        public Guid ClienteId { get; }

        public ListarPedidosResumoPorClienteQuery(Guid clienteId) {
            if (clienteId == Guid.Empty)
                throw new ArgumentException("ClienteId inválido.", nameof(clienteId));

            ClienteId = clienteId;
        }
    }
}
