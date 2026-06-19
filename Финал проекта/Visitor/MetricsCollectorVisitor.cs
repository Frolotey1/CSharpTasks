using System;

namespace Patterns.Visitor;

public record MetricsReport(
    int TotalNodes,
    int RenderedNodes,
    int ProxyCount,
    int MaterializedProxyCount,
    int DecoratorCount,
    TimeSpan TotalRenderTime
);

public class MetricsCollectorVisitor : IUIComponentVisitor
{
    private int _totalNodes;
    private int _renderedNodes;
    private int _proxyCount;
    private int _materializedProxyCount;
    private int _decoratorCount;
    private TimeSpan _totalRenderTime;

    public MetricsReport GetReport() => new MetricsReport(
        _totalNodes, _renderedNodes, _proxyCount, _materializedProxyCount, _decoratorCount, _totalRenderTime
    );

    public void Visit(ButtonComponent button)
    {
        _totalNodes++;
        _renderedNodes++;
    }

    public void Visit(PanelComponent panel)
    {
        _totalNodes++;
        _renderedNodes++;
    }

    public void Visit(LabelComponent label)
    {
        _totalNodes++;
        _renderedNodes++;
    }

    public void Visit(SliderComponent slider)
    {
        _totalNodes++;
        _renderedNodes++;
    }

    public void Visit(VirtualComponentProxy proxy)
    {
        _totalNodes++;
        _proxyCount++;
        if (proxy.IsMaterialized) _materializedProxyCount++;
    }

    public void Visit(ProtectionComponentProxy proxy)
    {
        _totalNodes++;
        _proxyCount++;
    }
}
