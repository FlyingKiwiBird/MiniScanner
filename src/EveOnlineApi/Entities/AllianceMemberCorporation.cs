//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="AllianceMemberCorporation.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Interfaces;

    /// <summary>
    /// Represents an Eve Online Alliance Member Corporation
    /// </summary>
    public class AllianceMemberCorporation : IAllianceMemberCorporation
    {
        /// <summary>
        /// Holds the Corporation object.
        /// </summary>
        private ICorporation corporation = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberCorporation"/> class.
        /// </summary>
        /// <param name="row">Member Corporation Row from XML</param>
        public AllianceMemberCorporation(MemberCorporationRow row)
        {
            if (row == null)
            {
                throw new ArgumentException("row cannot be null", "row");
            }

            this.CorporationId = row.CorporationId;
            this.StartTime = DateTime.Parse(row.StartDate + "Z", CultureInfo.InvariantCulture).ToUniversalTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberCorporation"/> class.
        /// </summary>
        /// <param name="corporationId">Corporation Id</param>
        /// <param name="startTime">Time Corporation joined Alliance</param>
        public AllianceMemberCorporation(int corporationId, DateTime startTime)
        {
            this.CorporationId = corporationId;
            this.StartTime = startTime;
        }

        /// <summary>
        /// Gets the Corporation Id
        /// </summary>
        public int CorporationId { get; private set; }

        /// <summary>
        /// Gets the Corporation object..
        /// </summary>
        public ICorporation Corporation
        {
            get
            {
                if (this.corporation == null)
                {
                    this.corporation = EveOnlineApi.Entities.Corporation.GetCorporationByCorporationId(this.CorporationId);
                }

                return this.corporation;
            }
        }

        /// <summary>
        /// Gets the time the corporation joined the Alliance
        /// </summary>
        public DateTime StartTime { get; private set; }

        /// <summary>
        /// Converts an XML Row set for MemberCorporations into an IEnumerable of AllianceMemberCorporation.
        /// </summary>
        /// <param name="rowset">XML Row Set of MemberCorporationsRow</param>
        /// <returns>Set of AllianceMemberCorporation</returns>
        public static IEnumerable<AllianceMemberCorporation> GetMemberCorporationsFromXml(MemberCorporationsRowset rowset)
        {
            foreach (MemberCorporationRow row in rowset.Rows)
            {
                yield return new AllianceMemberCorporation(row);
            }

            yield break;
        }
    }
}
