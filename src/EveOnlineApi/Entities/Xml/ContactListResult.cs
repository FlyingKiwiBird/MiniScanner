//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ContactListResult.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities.Xml
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml.Base;

    /// <summary>
    /// Result tag for the EVE Contact List API - Does not support writing to file.
    /// </summary>
    [XmlRoot("result")]
    public class ContactListResult : EveApiResult, IXmlSerializable
    {
        /// <summary>
        /// Gets or sets the Personal Contact List
        /// </summary>
        public PersonalContactListRowset ContactList { get; set; }

        /// <summary>
        /// Gets or sets Personal Contact Labels
        /// </summary>
        public ContactLabelRowset ContactLabels { get; set; }

        /// <summary>
        /// Gets or sets the Corporate Contact List
        /// </summary>
        public GroupContactListRowset CorporateContactList { get; set; }

        /// <summary>
        /// Gets or sets Corporate Contact Labels
        /// </summary>
        public ContactLabelRowset CorporateContactLabels { get; set; }

        /// <summary>
        /// Gets or sets the Alliance Contact List
        /// </summary>
        public GroupContactListRowset AllianceContactList { get; set; }

        /// <summary>
        /// Gets or sets Alliance Contact Labels
        /// </summary>
        public ContactLabelRowset AllianceContactLabels { get; set; }

        /// <summary>
        /// This function is for internal use, and returns null by default.
        /// </summary>
        /// <returns>null for all cases.</returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Overrides the default reading mechanism for XML documents.
        /// </summary>
        /// <param name="reader">XML Reader</param>
        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentException("reader cannot be null", "reader");
            }

            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "result")
            {
                if (reader.ReadToDescendant("rowset"))
                {
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "rowset")
                    {
                        switch (reader.GetAttribute("name"))
                        {
                            case "contactList":
                                this.ContactList = XmlSerialization.DeserializeString<PersonalContactListRowset>(reader.ReadOuterXml());
                                break;
                            case "contactLabels":
                                this.ContactLabels = XmlSerialization.DeserializeString<ContactLabelRowset>(reader.ReadOuterXml());
                                break;
                            case "corporateContactList":
                                this.CorporateContactList = XmlSerialization.DeserializeString<GroupContactListRowset>(reader.ReadOuterXml());
                                break;
                            case "corporateContactLabels":
                                this.CorporateContactLabels = XmlSerialization.DeserializeString<ContactLabelRowset>(reader.ReadOuterXml());
                                break;
                            case "allianceContactList":
                                this.AllianceContactList = XmlSerialization.DeserializeString<GroupContactListRowset>(reader.ReadOuterXml());
                                break;
                            case "allianceContactLabels":
                                this.AllianceContactLabels = XmlSerialization.DeserializeString<ContactLabelRowset>(reader.ReadOuterXml());
                                break;
                        }
                    }
                }

                reader.Read();
            }
        }

        /// <summary>
        /// Overrides the default writing mechanism for XML documents. Not implemented.
        /// </summary>
        /// <param name="writer">XML Writer</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}
