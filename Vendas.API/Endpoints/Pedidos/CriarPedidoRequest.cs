namespace Vendas.API.Endpoints.Pedidos {
    public record CriarPedidoRequest(Guid ClientId, Guid EnderecoId);
}
