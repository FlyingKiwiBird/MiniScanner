using System.Collections.Generic;
using EveScanner.Interfaces;

namespace EveScanner.SQLiteStorage
{
    public interface IItemAppraisalDataProvider
    {
        IEnumerable<IItemAppraisal> GetItemAppraisalsForItemList(string input);
        IItemAppraisal GetItemAppraisalByTypeId(int typeId);
        IItemAppraisal GetItemAppraisalByTypeName(string typeName);
    }
}