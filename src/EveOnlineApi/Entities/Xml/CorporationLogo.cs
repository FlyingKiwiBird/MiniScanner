//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CorporationLogo.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the elements required to construct a Corporation Logo
    /// </summary>
    [XmlRoot("logo")]
    public class CorporationLogo
    {
        /// <summary>
        /// Gets or sets the Graphic Id
        /// </summary>
        [XmlElement("graphicID")]
        public int GraphicId { get; set; }

        /// <summary>
        /// Gets or sets the First Shape
        /// </summary>
        [XmlElement("shape1")]
        public int Shape1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Shape
        /// </summary>
        [XmlElement("shape2")]
        public int Shape2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Shape
        /// </summary>
        [XmlElement("shape3")]
        public int Shape3 { get; set; }

        /// <summary>
        /// Gets or sets the First color
        /// </summary>
        [XmlElement("color1")]
        public int Color1 { get; set; }

        /// <summary>
        /// Gets or sets the Second Color
        /// </summary>
        [XmlElement("color2")]
        public int Color2 { get; set; }

        /// <summary>
        /// Gets or sets the Third Color
        /// </summary>
        [XmlElement("color3")]
        public int Color3 { get; set; }
    }
}
