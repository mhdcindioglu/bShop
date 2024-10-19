using bShop.Data.Interfaces;

namespace bShop.Data.Extensions;

public static class ISelectableListExtensions
{
    public static void Select<T>(this IEnumerable<T> items, int? id) where T : ISelectable<int>
    {
        if (id.HasValue)
            foreach (var item in items)
                item.Selected = item.Id == id.Value;
    }
    public static void Select<T>(this IEnumerable<T> items, int[] ids) where T : ISelectable<int>
    {
        if (ids.Length > 0)
            foreach (var item in items)
                item.Selected = ids.Contains(item.Id);
    }
    public static void Select<T>(this IEnumerable<T> items, T? selected) where T : ISelectable<int>
    {
        if (selected is not null)
            foreach (var item in items)
                item.Selected = item.Id == selected.Id;
    }
    public static T? SelectedItem<T>(this IEnumerable<T> items) where T : ISelectable<int> =>
        items.FirstOrDefault(x => x.Selected);
    public static T[] SelectedItems<T>(this IEnumerable<T> items) where T : ISelectable<int> =>
        items.Where(x => x.Selected).ToArray();
}
