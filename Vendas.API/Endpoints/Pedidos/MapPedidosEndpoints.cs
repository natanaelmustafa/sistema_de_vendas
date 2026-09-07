namespace Vendas.API.Endpoints.Pedidos {
    public static class MapPedidosEndpoints {
        public static WebApplication MapPedidosEndpoints(this WebApplication app) {
            var group = app.MapGroup("/pedidos")
                .WithTags("Pedidos")
                .WithOpenApi();

            group.MapGet("/fake-ids", () => Results.Ok(new {
                clientes = new[]
                {
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000001"),
                        enderecos = new[]
                        {
                            new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                                  descricao = "Av. Paulista 1578, Bela Vista, São Paulo" },
                            new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                                  descricao = "Rua das Flores 300, Vila Olímpia, São Paulo" }
                        }
                    },
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000002"),
                        enderecos = new[]
                        {
                            new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                                  descricao = "Av. do Contorno 8000, Santo Agostinho, Belo Horizonte" }
                        }
                    }
                },
                produtos = new[]
                {
                    new { produtoId = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                          descricao = "Notebook Gamer RTX 4060 - R$ 8.500,00" },
                    new { produtoId = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                          descricao = "Mouse Sem Fio Logitech MX Master - R$ 450,00" },
                    new { produtoId = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                          descricao = "Teclado Mecânico Keychron K8 - R$ 680,00" },
                    new { produtoId = Guid.Parse("11111111-0000-0000-0000-000000000004"),
                          descricao = "Monitor Ultrawide 34 Polegadas - R$ 3.200,00" }
                }
            })).WithSummary("Exibe os IDs dos dados disponíveis nos Fakes para usar nos testes");

            // — GET /pedidos ——————————————————————————————————————————

            group.MapGet("/", async (
                [FromServices] ListarPedidosResumoQueryHandler handler,
                CancellationToken ct) =>
            {
                var resultado = await handler.HandleAsync(new ListarPedidosResumoQuery(), ct);

                return Results.Ok(resultado);
            })
            .WithSummary("Lista Resumida de todos os pedidos");

            // — GET /pedidos/{id} —____________________________________

            group.MapGet("/{id:guid}", async (Guid id,
                [FromServices] ObterPedidoCompletoPorIdQueryHandler handler,
                CancellationToken ct) =>
            {
                var resultado = await handler.HandleAsync(new ObterPedidoCompletoPorIdQuery(id), ct);

                return resultado is null ? Results.NotFound() : Results.Ok(resultado);
            })
            .WithSummary("Retorna os detalhes completos de um pedido");

            // — GET /pedidos/clientes/{clienteId} —————————————————————

            group.MapGet("/clientes/{clienteId:guid}", async (
                Guid clienteId,
                [FromServices] ListarPedidosResumoPorClienteQueryHandler handler,
                CancellationToken ct) =>
            {
                var resultado = await handler.HandleAsync(new ListarPedidosResumoPorClienteQuery(clienteId), ct);

                return Results.Ok(resultado);
            })
            .WithSummary("Lista pedidos resumidos de um cliente específico");

            // — GET /pedidos/pagamentos —________________________________________

            group.MapGet("/pagamentos", async (
                [FromQuery] StatusPagamento? status,
                [FromServices] ListarPedidosPagamentosPorStatusQueryHandler handler,
                CancellationToken ct) =>
            {
                if (status is null)
                    return Results.BadRequest(new {
                        erro = "Status inválido. Valores aceitos: Pendente (0), Aprovado (1), Recusado (2)"
                    });

                var resultado = await handler.HandleAsync(new ListarPedidosPagamentosPorStatusQuery(status.Value), ct);

                return Results.Ok(resultado);
            })
            .WithSummary("Lista pagamentos filtrados por status")
            .WithDescription(
                "Valores válidos para status:\n" +
                "  Pendente ou 1\n" +
                "  Aprovado ou 2\n" +
                "  Cancelado ou 3");

            // — POST /pedidos ——————————————————————————————————————————
            group.MapPost("/", async (
                CriarPedidoRequest req,
                CriarPedidoCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var command = new CriarPedidoCommand(req.ClienteId, req.EnderecoId);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Created($"/pedidos/{result.PedidoId}", result);
                } catch (InvalidOperationException ex) {
                    return Results.NotFound(new { erro = ex.Message });
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Cria um novo pedido");

            // — POST /pedidos/{id}/itens ——————————————————————————————
            group.MapPost("/{id:guid}/itens", async (
                Guid id,
                AdicionarItemRequest req,
                AdicionarItemAoPedidoCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var command = new AdicionarItemAoPedidoCommand(id, req.ProdutoId,
                        req.Quantidade);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                } catch (InvalidOperationException ex) {
                    return Results.NotFound(new { erro = ex.Message });
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Adicionar um item ao pedido");

            // — POST /pedidos/{id}/pagamento ——————————————————————————
            group.MapPost("/{id:guid}/pagamento", async (
                Guid id,
                IniciarPagamentoRequest req,
                IniciarPagamentoCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var metodo = (MetodoPagamento)req.MetodoPagamento;
                    var command = new IniciarPagamentoCommand(id, metodo);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Inicia o pagamento do pedido");

            // — POST /pedidos/{id}/pagamento/confirmar ——————————————————
            group.MapPost("/{id:guid}/pagamento/confirmacao", async (
                Guid id,
                ConfirmarPagamentoRequest req,
                IPedidoRepository repo,
                CancellationToken ct) => {
                    try {
                        var pedido = await repo.ObterPorIdAsync(id, ct);
                        if (pedido is null) return Results.NotFound();

                        var pagamento = pedido.Pagamentos
                            .FirstOrDefault(p => p.Id == req.PagamentoId);

                        if (pagamento is null)
                            return Results.NotFound(new { erro = "Pagamento não encontrado." });

                        // Simula o retorno do gateway externo:
                        // Em produção este código viria no payload do webhook
                        pagamento.GerarCodigoTransacaoLocal();

                        // Pagamento processa sua transição interna
                        pagamento.ConfirmarPagamento();

                        // Aggregate Root reage: StatusPedido -> PagamentoConfirmado
                        pedido.HandlePagamentoAprovado(pagamento.Id);

                        await repo.AtualizarAsync(pedido, ct);

                        return Results.Ok(new {
                            PedidoId = pedido.Id,
                            PagamentoId = pagamento.Id,
                            StatusPedido = pedido.StatusPedido.ToString(),
                            StatusPagamento = pagamento.StatusPagamento.ToString(),
                            CodigoTransacao = pagamento.CodigoTransacao
                        });
                    } catch (DomainException ex) {
                        return Results.UnprocessableEntity(new { erro = ex.Message });
                    }
                })
            .WithSummary("Confirma o pagamento do pedido (simula gateway)")
            .WithDescription(
                "SIMULAÇÃO - em produção este endpoint não existiria.\n" +
                "O gateway de pagamento enviaria um webhook para /webhooks/pagamento\n" +
                "com o código de transação gerado externamente.\n" +
                "Aqui o código é gerado localmente via GerarCodigoTransacaoLocal().");

            // — POST /pedidos/{id}/separacao ——————————————————————————
            group.MapPost("/{id:guid}/separacao", async (
                Guid id,
                MarcarPedidoComoEmSeparacaoCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var command = new MarcarPedidoComoEmSeparacaoCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Marca o pedido em separação (PagamentoConfirmado -> EmSeparacao)");

            // — POST /pedidos/{id}/enviado ————————————————————————————
            group.MapPost("/{id:guid}/enviado", async (
                Guid id,
                MarcarPedidoComoEnviadoCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var command = new MarcarPedidoComoEnviadoCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Marca o pedido como enviado (Em Separação -> Enviado)");

            // — POST /pedidos/{id}/entregue ———————————————————————————
            group.MapPost("/{id:guid}/entregue", async (
                Guid id,
                MarcarPedidoComoEntregueCommandHandler handler,
                CancellationToken ct) =>
            {
                try {
                    var command = new MarcarPedidoComoEntregueCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                } catch (DomainException ex) {
                    return Results.UnprocessableEntity(new { erro = ex.Message });
                }
            })
            .WithSummary("Marca o pedido como entregue");

            // — POST /pedidos/{id}/cancelar ———————————————————————————
            group.MapPost("/{id:guid}/cancelamento", async (
                Guid id,
                CancelarPedidoRequest? req,         // nullable - body inteiro é opcional
                CancelarPedidoCommandHandler handler,
                CancellationToken ct) => {
                    try {
                        var command = new CancelarPedidoCommand(id, req?.CodigoMotivo ?? "Outro");
                        var result = await handler.HandleAsync(command, ct);
                        return Results.Ok(result);
                    } catch (DomainException ex) {
                        return Results.UnprocessableEntity(new { erro = ex.Message });
                    }
                })
            .WithSummary("Cancela o pedido")
            .WithDescription(
                "Body opcional. Códigos válidos para codigoMotivo:\n" +
                "  ClienteDesistiu  - Cliente desistiu da compra\n" +
                "  ErroPagamento    - Erro no processamento do pagamento\n" +
                "  ItemSemEstoque   - Item esgotado no estoque\n" +
                "  EnderecoInvalido - Endereço de entrega inválido\n" +
                "  Outro            - Outro motivo não especificado\n" +
                "Se omitido, o motivo padrão 'Outro' é ativado.");

            return app;
        }
    }
}
