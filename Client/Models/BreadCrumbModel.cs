using System;

namespace bShop.Client.Models;

public class BreadCrumbModel
{
    public BreadCrumbModel(string title, string? url = null)
    {
        Title = title;
        Url = url;
    }
    public string? Url { get; init; } 
    public string Title { get; init; } 
}
