using EveScanner.Interfaces.SDE;
using System.Collections.Generic;

namespace EveScanner.Interfaces.Providers
{
    public interface IInventoryTypeProvider
    {
        IInventoryType GetInventoryTypeByTypeId(int typeId);
        IEnumerable<IInventoryType> GetInventoryTypesByGroupId(int groupId);
        IInventoryType GetInventoryTypeByTypeName(string typeName);
    }
}
