using bShop.Client.Models;
using bShop.Shared.Models.Languages;

namespace bShop.Client.Store.LanguageState;

public class UpdateLanguageAction(LanguageVM language)
{
    public LanguageVM Language { get; set; } = language;
}
