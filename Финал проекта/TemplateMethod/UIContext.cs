namespace Patterns;

public record UIContext(
    IRenderingContext RenderingContext,
    IApplicationTelemetry Telemetry,
    IContainerComponent Root
);
