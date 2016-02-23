using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces.EDE
{
    public interface IInventoryRepackaged
    {
        int GroupId { get; }
        double Volume { get; }
    }
}
