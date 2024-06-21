using bShop.Shared.Models.Languages;
using System.Globalization;

namespace bShop.Client.Store.LanguageState;

[FeatureState]
public class LanguageState
{
    public LanguageVM Language { get; }

    private LanguageState() { }
    public LanguageState(LanguageVM language) =>
        Language = new LanguageVM { Id = language.Id, Name = language.Name, Culture = language.Culture, SeoCode = language.SeoCode, Image = language.Image, Rtl = language.Rtl, };
}
