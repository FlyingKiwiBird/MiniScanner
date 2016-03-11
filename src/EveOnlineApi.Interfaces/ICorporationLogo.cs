//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICorporationLogo.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    /// <summary>
    /// Defines the elements required to construct a Corporation Logo
    /// </summary>
    public interface ICorporationLogo
    {
        /// <summary>
        /// Gets or sets the First Color
        /// </summary>
        int Color1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Color
        /// </summary>
        int Color2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Color
        /// </summary>
        int Color3 { get; set; }

        /// <summary>
        /// Gets or sets the Graphic Id
        /// </summary>
        int GraphicId { get; set; }

        /// <summary>
        /// Gets or sets the First Shape
        /// </summary>
        int Shape1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Shape
        /// </summary>
        int Shape2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Shape
        /// </summary>
        int Shape3 { get; set; }
    }
}