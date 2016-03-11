using System;

namespace EveScanner.IoC.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IgnoreMemberAttribute : Attribute
    {
        public IgnoreMemberAttribute()
        {
        }
    }
}
