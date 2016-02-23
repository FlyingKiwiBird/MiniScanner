using System;

namespace EveScanner.IoC.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MapFromAttribute : Attribute
    {
        public string Name { get; set; }

        public MapFromAttribute(string name)
        {
            this.Name = name;
        }
    }
}
