namespace Vendas.API.Endpoints.Pedidos {
    public record AdicionarItemRequest(Guid ClientId, Guid EnderecoId);
}
