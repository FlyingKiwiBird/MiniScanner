using System;

namespace EveScanner.IoC.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class MapFromAttribute : Attribute
    {
        public string Name { get; set; }

        public MapFromAttribute()
        {
        }
    }
}
