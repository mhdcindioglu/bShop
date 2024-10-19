namespace bShop.Data.ViewModels;

public class PageList<T> : List<T> where T : class
{
    public int AllCount { get; set; }
}
