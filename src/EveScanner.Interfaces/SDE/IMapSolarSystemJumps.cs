using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces.SDE
{
    public interface IMapSolarSystemJumps
    {
        int FromRegionId { get; }
        int FromConstellationId { get; }
        int FromSolarSystemId { get; }
        int ToSolarSystemId { get; }
        int ToConstellationId { get; }
        int ToRegionId { get; }
    }
}
