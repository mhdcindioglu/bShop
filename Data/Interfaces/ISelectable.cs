namespace bShop.Data.Interfaces;

public interface ISelectable<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
    public bool Selected { get; set; }
}