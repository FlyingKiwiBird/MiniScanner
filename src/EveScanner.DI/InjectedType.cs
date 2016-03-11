namespace EveScanner.IoC
{
    using System;
    using System.Linq.Expressions;

    public abstract class InjectionType
    {
        protected InjectionType(Type implementationType)
        {
            if (implementationType == null)
            {
                throw new ArgumentException("ImplementationType cannot be null.", "implementationType");
            }

            this.ImplementationType = implementationType;
            this.ConstructorDelegate = Expression.Lambda(Expression.New(implementationType)).Compile();
        }

        protected InjectionType(Type implementationType, string friendlyName) : this(implementationType)
        {
            if (string.IsNullOrEmpty(friendlyName))
            {
                throw new ArgumentException("FriendlyName cannot be null or empty.", "friendlyName");
            }

            this.FriendlyName = friendlyName;
        }

        public abstract Type InterfaceType { get; }
        public Type ImplementationType { get; set; }
        public string FriendlyName { get; set; }
        public Delegate ConstructorDelegate { get; set; }

        public virtual object Construct()
        {
            return (this.ConstructorDelegate as Func<object>)();
        }
    }

    public class InjectionType<TInterfaceType> : InjectionType
    {
        public InjectionType(Type implementationType) : base(implementationType)
        {
            if (!typeof(TInterfaceType).IsInterface)
            {
                throw new ArgumentException("InterfaceType must be an Interface");
            }

            if (!typeof(TInterfaceType).IsAssignableFrom(implementationType))
            {
                throw new ArgumentException("ImplementationType must be an implementation of the InterfaceType");
            }
        }

        public InjectionType(Type implementationType, string friendlyName) : this(implementationType)
        {
            if (string.IsNullOrEmpty(friendlyName))
            {
                throw new ArgumentException("FriendlyName cannot be null or empty.", "friendlyName");
            }

            this.FriendlyName = friendlyName;
        }

        public override Type InterfaceType
        {
            get
            {
                return typeof(TInterfaceType);
            }
        }

        public new TInterfaceType Construct()
        {
            return (TInterfaceType)(this.ConstructorDelegate as Func<object>)();
        }
    }
}
