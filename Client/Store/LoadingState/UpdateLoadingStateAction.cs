using bShop.Client.Models;

namespace bShop.Client.Store.LoadingState;

public class UpdateLoadingStateAction(bool loading)
{
    public bool Loading { get; set; } = loading;
}
