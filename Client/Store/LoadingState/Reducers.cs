namespace bShop.Client.Store.LoadingState;

public static class Reducers
{
    [ReducerMethod]
    public static LoadingState ReduceUpdateLoadingStateAction(LoadingState appState, UpdateLoadingStateAction action) => 
        new(loading: action.Loading);
}
