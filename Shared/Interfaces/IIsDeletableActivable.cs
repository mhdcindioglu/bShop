namespace bShop.Shared.Interfaces;
public interface IIsDeletableActivable
{
    bool IsDeleted { get; set; }
    bool IsActive { get; set; }
}
