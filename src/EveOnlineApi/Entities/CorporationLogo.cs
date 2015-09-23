//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CorporationLogo.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    /// <summary>
    /// Defines the elements required to construct a Corporation Logo
    /// </summary>
    public class CorporationLogo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CorporationLogo"/> class.
        /// </summary>
        /// <param name="graphicId">Graphic Id</param>
        /// <param name="shape1">Shape 1 Id</param>
        /// <param name="shape2">Shape 2 Id</param>
        /// <param name="shape3">Shape 3 Id</param>
        /// <param name="color1">Color 1 Id</param>
        /// <param name="color2">Color 2 Id</param>
        /// <param name="color3">Color 3 Id</param>
        public CorporationLogo(int graphicId, int shape1, int shape2, int shape3, int color1, int color2, int color3)
        {
            this.GraphicId = graphicId;
            this.Shape1 = shape1;
            this.Shape2 = shape2;
            this.Shape3 = shape3;
            this.Color1 = color1;
            this.Color2 = color2;
            this.Color3 = color3;
        }

        /// <summary>
        /// Gets or sets the Graphic Id
        /// </summary>
        public int GraphicId { get; set; }

        /// <summary>
        /// Gets or sets the First Shape
        /// </summary>
        public int Shape1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Shape
        /// </summary>
        public int Shape2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Shape
        /// </summary>
        public int Shape3 { get; set; }

        /// <summary>
        /// Gets or sets the First Color
        /// </summary>
        public int Color1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Color
        /// </summary>
        public int Color2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Color
        /// </summary>
        public int Color3 { get; set; }
    }
}
