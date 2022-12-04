# OpenTelemetry

## Rastreamento distribuído do .NET
**OpenTelemetry** é uma biblioteca neutra para fornecedores que dá suporte a vários serviços.
**Application Insights** é um serviço completo fornecido pela Microsoft. 

O rastreamento distribuído é uma técnica de diagnóstico que ajuda os engenheiros a localizar falhas e problemas de desempenho em aplicativos, especialmente aqueles que podem ser distribuídos em vários computadores ou processos.

**O ActivityListener dá suporte à observação de qualquer Atividade, independentemente de o desenvolvedor ter algum conhecimento prévio sobre ela**. Isso torna o ActivityListener uma solução de uso geral simples e flexível. Por outro lado, o **uso do DiagnosticListener é um cenário mais complexo que exige que o código instrumentado aceite invocando DiagnosticSource.StartActivity** e a biblioteca de coleção deve ter conhecimento das informações exatas de nomenclatura que o código instrumentado usou ao iniciá-lo.

### Práticas recomendadas
- Somente os desenvolvedores de aplicativos precisam fazer referência a uma biblioteca opcional de terceiros para coletar a telemetria de rastreamento distribuído, como o OpenTelemetry neste exemplo. Os autores da biblioteca do .NET podem contar exclusivamente com as APIs no System.Diagnostics.DiagnosticSource, que fazem parte do runtime do .NET. Isso garante que as bibliotecas sejam executadas em diversos aplicativos .NET, independentemente das preferências do desenvolvedor do aplicativo sobre qual biblioteca ou fornecedor usar para coletar telemetria.

- Crie o ActivitySource uma vez, armazene-o em uma variável estática e use essa instância durante o tempo necessário. 
- O nome da fonte passado para o construtor deve ser exclusivo para evitar conflitos com outras fontes.

- Se um assembly estiver adicionando instrumentação para código em um segundo assembly independente, o nome deverá ser baseado no assembly que define o ActivitySource, não no assembly cujo código está sendo instrumentado.

- Os eventos são armazenados em uma lista na memória até que possam ser transmitidos, o que torna esse mecanismo adequado apenas para registrar um pequeno número de eventos. Para um volume grande ou desassociado de eventos, é melhor usar uma API de registro em log focada nessa tarefa, como iLogger. 

- Em um projeto maior e mais realista, o uso de uma atividade em cada método produziria rastreamentos extremamente detalhados e, portanto, não é recomendado.

- O exportador de console é útil para exemplos rápidos ou desenvolvimento local, mas em uma implantação de produção, é melhor rastreamentos a um repositório centralizado. O OpenTelemetry dá suporte a vários destinos usando diferentes exportadores. 

- A telemetria de rastreamento distribuído é capturada automaticamente após a configuração do SDK do Application Insights para aplicativos ASP.NET ou ASP.NET Core ou a habilitação da instrumentação sem código.

## Registro em log e rastreamento no .NET









## Ler:
 - [x] https://learn.microsoft.com/pt-br/dotnet/core/diagnostics/distributed-tracing
 - [ ] https://learn.microsoft.com/pt-br/azure/azure-monitor/app/distributed-tracing
 - [ ] https://learn.microsoft.com/pt-br/azure/azure-monitor/app/codeless-overview
 - [x] https://learn.microsoft.com/pt-br/dotnet/core/diagnostics/logging-tracing
 - [ ] https://learn.microsoft.com/pt-br/dotnet/core/diagnostics/metrics


 - [ ] https://www.mytechramblings.com/tags/opentelemetry/
 - [x] https://www.mytechramblings.com/posts/getting-started-with-opentelemetry-and-dotnet-core/ 
 - [ ] https://www.mytechramblings.com/posts/getting-started-with-opentelemetry-metrics-and-dotnet-part-1/
 - [ ] https://www.mytechramblings.com/posts/getting-started-with-opentelemetry-metrics-and-dotnet-part-2/

 - [ ] https://opentelemetry.io/docs/concepts/
  