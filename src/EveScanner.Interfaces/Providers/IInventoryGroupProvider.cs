using EveScanner.Interfaces.SDE;
using System.Collections.Generic;

namespace EveScanner.Interfaces.Providers
{
    public interface IInventoryGroupProvider
    {
        IInventoryGroup GetInventoryGroupById(int groupId);
        IEnumerable<IInventoryGroup> GetInventoryGroupsByCategoryId(int categoryId);
        IInventoryGroup GetInventoryGroupByName(string groupName);
    }
}
