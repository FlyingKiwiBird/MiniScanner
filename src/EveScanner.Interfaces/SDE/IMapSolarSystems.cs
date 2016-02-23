using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces.SDE
{
    public interface IMapSolarSystems
    {
        int RegionId { get; }
        int ConstellationId { get; }
        int SolarSystemId { get; }
        string SolarSystemName { get; }
        double CoordinatesX { get; }
        double CoordinatesY { get; }
        double CoordinatesZ { get; }
        double DimensionsXMin { get; }
        double DimensionsXMax { get; }
        double DimensionsYMin { get; }
        double DimensionsYMax { get; }
        double DimensionsZMin { get; }
        double DimensionsZMax { get; }
        double Luminosity { get; }
        bool Border { get; }
        bool Fringe { get; }
        bool Corridor { get; }
        bool Hub { get; }
        bool International { get; }
        bool Regional { get; }
        bool Constellation { get; }
        double Security { get; }
        int FactionId { get; }
        double Radius { get; }
        int SunTypeId { get; }
        string SecurityClass { get; }
    }
}
