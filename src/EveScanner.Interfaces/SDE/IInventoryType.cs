namespace EveScanner.Interfaces.SDE
{
    public interface IInventoryType
    {
        int TypeId { get; }
        int? GroupId { get; }
        string TypeName { get; }
        string Description { get; }
        double? Mass { get; }
        double? Volume { get; }
        double? Capacity { get; }
        int? PortionSize { get; }
        int? RaceId { get; }
        decimal? BasePrice { get; }
        bool? Published { get; }
        int? MarketGroupId { get; }
        int? IconId { get; }
        int? SoundId { get; }
        int? GraphicId { get; }
    }
}
