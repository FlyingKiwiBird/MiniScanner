namespace EveScanner.Core
{
    using EveScanner.Interfaces;
    using System.Data;

    public class ScanItem : IItemAppraisal
    {
        public int Quantity { get; set; }
        public int TypeID { get; set; }
        public int GroupID { get; set; }
        public string TypeName { get; set; }
        public double Volume { get; set; }
        public double RepackagedVolume { get; set; }
        public decimal BuyValue { get; set; }
        public decimal SellValue { get; set; }
        public bool IsBlueprintCopy { get; set; }
        public bool IsError { get; set; }
        public string ErrorMessage { get; set; }

        public ScanItem()
        {
            this.IsBlueprintCopy = false;
            this.IsError = false;
            this.ErrorMessage = string.Empty;
        }
    }
}
