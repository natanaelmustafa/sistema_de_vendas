using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEmSeparacao {
    public sealed class MarcarPedidoComoEmSeparacaoCommand {
        public Guid PedidoId { get; }

        public MarcarPedidoComoEmSeparacaoCommand(Guid pedidoId) {
            PedidoId = pedidoId;
        }
    }
}
