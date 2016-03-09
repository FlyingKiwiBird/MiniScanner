namespace EveScanner.Core
{
    using EveScanner.Interfaces;
    using Interfaces.Providers;
    using EveScanner.IoC;
    using System.Data;
    using Interfaces.SDE;
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

        public ScanItem(int quantity, string typeName, bool isBlueprintCopy) : this()
        {
            this.Quantity = quantity;
            this.TypeName = typeName;
            this.IsBlueprintCopy = isBlueprintCopy;
        }

        public void ResolveByTypeId()
        {
        }

        public void ResolveByTypeName()
        {
            IInventoryTypeProvider itprovider = Injector.Create<IInventoryTypeProvider>();
            if (itprovider != null)
            {
                IInventoryType output = itprovider.GetInventoryTypeByTypeName(this.TypeName);
                if (output != null)
                {
                    this.CopyDataFromInventoryType(output);
                }
                else
                {
                    this.IsError = true;
                    this.ErrorMessage = "Could not resolve type by name.";
                }
            }
        }

        private void CopyDataFromInventoryType(IInventoryType t)
        {
            this.TypeID = t.TypeId;
            this.GroupID = t.GroupId ?? 0;
            this.TypeName = t.TypeName;
            this.Volume = t.Volume ?? 0;
        }
    }
}
