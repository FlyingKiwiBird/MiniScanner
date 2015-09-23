//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="CallListApi.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System.Xml.Serialization;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Base for the EVE Call List API
    /// </summary>
    [XmlRoot("eveapi")]
    public class CallListApi : EveApi
    {
        /// <summary>
        /// Gets or sets the Result object. This class defines
        /// the result directly since it holds two row sets unlike
        /// the normal result object.
        /// </summary>
        [XmlElement("result")]
        public CallListResult Result { get; set; }
    }
}