using bShop.Shared.Models;
using bShop.Shared.Models.Languages;
using Refit;

namespace bShop.Client.Pages;

public interface ILanguagesClient
{
    [Post("/Languages")]
    Task<ApiResult<LanguageVM[]>> Languages();
}
