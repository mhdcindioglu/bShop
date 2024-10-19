namespace bShop.Data.Interfaces;

public interface IUniqueEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}