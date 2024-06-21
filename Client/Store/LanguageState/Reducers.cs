using bShop.Client.Store.CurrentUserState;
using bShop.Shared.Models.Languages;

namespace bShop.Client.Store.LanguageState;

public static class Reducers
{
    [ReducerMethod]
    public static LanguageState ReduceUpdateLanguageAction(LanguageState language, UpdateLanguageAction action) => new(language: action.Language);
}
