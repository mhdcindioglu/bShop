using bShop.Client.Models;

namespace bShop.Client.Store.LoadingState;

[FeatureState]
public class LoadingState
{
    public bool Loading { get; }
    public LoadingState() { }
    public LoadingState(bool loading) { Loading = loading; }
}
