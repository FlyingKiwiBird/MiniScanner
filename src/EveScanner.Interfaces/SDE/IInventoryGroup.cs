namespace EveScanner.Interfaces.SDE
{
    public interface IInventoryGroup
    {
        int GroupId { get; }
        int? CategoryId { get; }
        string GroupName { get; }
        int? IconId { get; }
        bool? UseBasePrice { get; }
        bool? Anchored { get; }
        bool? Anchorable { get; }
        bool? FittableNonSingleton { get; }
        bool? Published { get; }
    }
}
