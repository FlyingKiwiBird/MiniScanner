//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="InjectionType.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Class to handle construction and general details about Injection Types
    /// </summary>
    public abstract class InjectionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionType"/> class.
        /// </summary>
        /// <param name="implementationType">Output Type</param>
        protected InjectionType(Type implementationType)
        {
            if (implementationType == null)
            {
                throw new ArgumentException("ImplementationType cannot be null.", "implementationType");
            }

            this.ImplementationType = implementationType;
            this.ConstructorDelegate = Expression.Lambda(Expression.New(implementationType)).Compile();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionType"/> class.
        /// </summary>
        /// <param name="implementationType">Output Type</param>
        /// <param name="friendlyName">Friendly Name</param>
        protected InjectionType(Type implementationType, string friendlyName) : this(implementationType)
        {
            if (string.IsNullOrEmpty(friendlyName))
            {
                throw new ArgumentException("FriendlyName cannot be null or empty.", "friendlyName");
            }

            this.FriendlyName = friendlyName;
        }

        /// <summary>
        /// Gets the Interface Type
        /// </summary>
        public abstract Type InterfaceType { get; }

        /// <summary>
        /// Gets or sets the Implemented Type
        /// </summary>
        public Type ImplementationType { get; protected set; }

        /// <summary>
        /// Gets or sets the Friendly Name
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the Constructor Delegate
        /// </summary>
        public Delegate ConstructorDelegate { get; protected set; }

        /// <summary>
        /// Constructs a new Object using the Constructor Delegate.
        /// </summary>
        /// <returns>New Object</returns>
        public virtual object Construct()
        {
            return (this.ConstructorDelegate as Func<object>)();
        }
    }

    /// <summary>
    /// Class to handle construction and general details about Injection Types
    /// </summary>
    /// <typeparam name="TInterfaceType">Output Type</typeparam>
    public class InjectionType<TInterfaceType> : InjectionType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionType{TInterfaceType}"/> class.
        /// </summary>
        /// <param name="implementationType">Output Type</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="InjectionType{TInterfaceType}"/> class.
        /// </summary>
        /// <param name="implementationType">Output Type</param>
        /// <param name="friendlyName">Friendly Name</param>
        public InjectionType(Type implementationType, string friendlyName) : this(implementationType)
        {
            if (string.IsNullOrEmpty(friendlyName))
            {
                throw new ArgumentException("FriendlyName cannot be null or empty.", "friendlyName");
            }

            this.FriendlyName = friendlyName;
        }

        /// <summary>
        /// Gets the Interface Type
        /// </summary>
        public override Type InterfaceType
        {
            get
            {
                return typeof(TInterfaceType);
            }
        }

        /// <summary>
        /// Constructs a new object of the <see cref="TInterfaceType"/> type.
        /// </summary>
        /// <returns>New <see cref="TInterfaceType"/></returns>
        public new TInterfaceType Construct()
        {
            return (TInterfaceType)(this.ConstructorDelegate as Func<object>)();
        }
    }
}
