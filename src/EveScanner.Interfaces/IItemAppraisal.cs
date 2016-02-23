namespace EveScanner.Interfaces
{
    public interface IItemAppraisal
    {
        int Quantity { get; }
        int TypeID { get; }
        int GroupID { get; }
        string TypeName { get; }
        double Volume { get; }
        double RepackagedVolume { get; }
        decimal BuyValue { get; }
        decimal SellValue { get; }
        bool IsBlueprintCopy { get; }
        bool IsError { get; }
        string ErrorMessage { get; }
    }
}
