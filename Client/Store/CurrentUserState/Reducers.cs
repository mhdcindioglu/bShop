namespace bShop.Client.Store.CurrentUserState;

public static class Reducers
{
    [ReducerMethod]
    public static CurrentUserState ReduceUpdateCurrentUserAction(CurrentUserState appState, UpdateCurrentUserAction action) => new(currnetUser: action.CurrnetUser);
}
