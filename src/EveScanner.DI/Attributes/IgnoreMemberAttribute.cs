using System;

namespace EveScanner.IoC.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
        public IgnoreMemberAttribute()
        {
        }
    }
}
