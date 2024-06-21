using bShop.Localization;
using bShop.Shared.Models;
using System.Globalization;
using System.Resources;

namespace bShop.Client.Extensions;

public static class DictionaryExtensions
{
    public static void Add(this Dictionary<string, List<string>> errors, string propertyName, List<ApiResultError> apiErrors)
    {
        var resourceManager = new ResourceManager("bShop.Localization.Resources", typeof(Resources).Assembly);
        errors.Add(propertyName, apiErrors
            .Where(x => !string.IsNullOrEmpty(x.Message) && x.ApiErrorType == ApiResultErrorType.InvalidModel && x.ObjectName == propertyName)
            .Select(x => resourceManager.GetString(x.Message!, CultureInfo.CurrentCulture) ?? x.Message!).ToList());
    }
}
