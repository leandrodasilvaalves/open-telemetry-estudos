# Getting started with OpenTelemetry and distributed tracing in .NET Core

## Span and activities

A span is the building block that forms a trace, it has a unique identifier and represents a piece of the workflow in the distributed system. In dotnet a Span is represented by an Activity. The OpenTelemetry client for dotnet is reusing the existing Activity and associated classes to represent the OpenTelemetry Span.

## Attributes
Attributes are key:value pairs that provide additional information to a trace.
In .NET those are called Tags. 


# References

- https://www.mytechramblings.com/posts/getting-started-with-opentelemetry-and-dotnet-core/
- https://github.com/karlospn/opentelemetry-tracing-demo