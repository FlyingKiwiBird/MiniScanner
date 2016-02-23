using EveScanner.Interfaces.EDE;

namespace EveScanner.Interfaces.Providers
{
    public interface IInventoryRepackagedProvider
    {
        IInventoryRepackaged GetRepackagedVolumesForGroup(int groupId);
    }
}
