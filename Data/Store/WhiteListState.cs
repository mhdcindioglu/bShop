using Fluxor;

namespace bShop.Data.Store;

public record UpdateWhiteListAction(List<int> WhiteList);


[FeatureState]
public record WhiteListState
{
    public List<int> WhiteList { get; } = [];

    private WhiteListState() { }

    public WhiteListState(List<int> whiteList)
    {
        WhiteList = whiteList;
    }
}
public static class WhiteListStateReducers
{
    [ReducerMethod]
    public static WhiteListState ReduceUpdateWhiteListAction(WhiteListState state, UpdateWhiteListAction action) =>
        new(whiteList: action.WhiteList);
}

