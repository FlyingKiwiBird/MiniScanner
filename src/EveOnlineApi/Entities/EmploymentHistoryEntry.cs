//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EmploymentHistoryEntry.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System;
    using System.Collections.Generic;

    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines a record of Employment for an Eve Online character.
    /// </summary>
    public class EmploymentHistoryEntry
    {
        /// <summary>
        /// Holds our corporation object.
        /// </summary>
        private Corporation corporation = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmploymentHistoryEntry"/> class.
        /// </summary>
        /// <param name="row">Character Employment XML Object</param>
        public EmploymentHistoryEntry(CharacterEmploymentRow row)
        {
            this.Id = row.RecordId;
            this.CorporationId = row.CorporationId;
            this.StartDate = DateTime.Parse(row.StartDate + "Z").ToUniversalTime();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmploymentHistoryEntry"/> class.
        /// </summary>
        /// <param name="recordId">Employment Record Id</param>
        /// <param name="corporationId">Corporation Id</param>
        /// <param name="startDate">Employment Start Date in UTC</param>
        public EmploymentHistoryEntry(int recordId, int corporationId, DateTime startDate)
        {
            this.Id = recordId;
            this.CorporationId = corporationId;
            this.StartDate = startDate;
        }

        /// <summary>
        /// Gets or sets the Employment Record Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Corporation Id
        /// </summary>
        public int CorporationId { get; set; }

        /// <summary>
        /// Gets the Corporation Object
        /// </summary>
        public Corporation Corporation
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
        /// Gets or sets the Start of Employment in UTC.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the End of Employment in UTC.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Creates Employment History Entries from the XML Equivalents
        /// </summary>
        /// <param name="employment">Character Employment XML Row Set</param>
        /// <returns>Employment History Entries</returns>
        public static IEnumerable<EmploymentHistoryEntry> CreateEmploymentHistory(CharacterEmploymentRowset employment)
        {
            List<EmploymentHistoryEntry> entries = new List<EmploymentHistoryEntry>();

            foreach (CharacterEmploymentRow row in employment.Rows)
            {
                entries.Add(new EmploymentHistoryEntry(row));
            }

            for (int i = entries.Count - 1; i > 0; i--)
            {
                entries[i].EndDate = entries[i - 1].StartDate;
            }

            entries[0].EndDate = DateTime.MaxValue;

            return entries;
        }
    }
}
