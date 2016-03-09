namespace EveScanner.SQLiteStorage
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using EveScanner.Core;
    using EveScanner.Interfaces;
    using EveScanner.Interfaces.Providers;
    using EveScanner.Interfaces.SDE;
    using EveScanner.IoC;

    public class SQLiteItemAppraisalProvider : IItemAppraisalDataProvider
    {
        public IEnumerable<IItemAppraisal> GetItemAppraisalsForItemList(string input)
        {
            foreach (string line in Validators.ExtractLinesFromInput(input))
            {
                MatchCollection mc = cargoScanRegex.Matches(line);
                ScanItem scanItem = null;

                if (mc.Count > 0)
                {
                    Match m = mc[0];

                    scanItem = (ScanItem)this.GetItemAppraisalByTypeName(m.Groups["item"].Value);
                    if (scanItem != null)
                    {
                        scanItem.Quantity = int.Parse(m.Groups["qty"].Value);
                        if (m.Groups["bpind"] != null)
                        {
                            if (m.Groups["bpind"].Value == "Copy")
                            {
                                scanItem.IsBlueprintCopy = true;
                            }
                        }
                    }
                }

                if (scanItem == null)
                {
                    scanItem = new ScanItem() { IsError = true, ErrorMessage = line };
                }

                yield return scanItem;
            }

            yield break;
        }

        public IItemAppraisal GetItemAppraisalByTypeId(int typeId)
        {
            IInventoryTypeProvider itprovider = Injector.Create<IInventoryTypeProvider>();
            
            var itype = itprovider.GetInventoryTypeByTypeId(typeId);
            
            if (itype != null)
            {
                return this.GetScanItemFromInventoryType(itype);
            }

            return null;
        }

        public IItemAppraisal GetItemAppraisalByTypeName(string typeName)
        {
            IInventoryTypeProvider itprovider = Injector.Create<IInventoryTypeProvider>();

            var itype = itprovider.GetInventoryTypeByTypeName(typeName);

            if (itype != null)
            {
                return this.GetScanItemFromInventoryType(itype);
            }

            return null;
        }

        private IItemAppraisal GetScanItemFromInventoryType(IInventoryType itype)
        {
            IInventoryRepackagedProvider irprovider = Injector.Create<IInventoryRepackagedProvider>();

            var irpkg = irprovider.GetRepackagedVolumesForGroup(itype.GroupId.Value);

            return new ScanItem()
            {
                TypeID = itype.TypeId,
                GroupID = itype.GroupId.Value,
                TypeName = itype.TypeName,
                Volume = itype.Volume.Value,
                RepackagedVolume = irpkg != null ? irpkg.Volume : itype.Volume.Value
            };
        }
    }
}
