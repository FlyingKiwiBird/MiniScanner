//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="Alliance.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using EveOnlineApi.Common;
    using EveOnlineApi.Entities.Xml;
    using EveOnlineApi.Interfaces;

    using EveScanner.Interfaces;
    using EveScanner.IoC;

    /// <summary>
    /// Represents an Eve Online Alliance
    /// </summary>
    public class Alliance : IAlliance
    {
        /// <summary>
        /// Holds the Executor Corporation information.
        /// </summary>
        private ICorporation executorCorporation = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance"/> class.
        /// </summary>
        /// <param name="row">Alliance XML Row</param>
        public Alliance(AllianceRow row)
        {
            if (row == null)
            {
                throw new ArgumentException("row cannot be null", "row");
            }

            this.Name = row.Name;
            this.ShortName = row.ShortName;
            this.Id = row.AllianceId;
            this.ExecutorCorporationId = row.ExecutorCorpId;
            this.MemberCount = row.MemberCount;
            this.StartDate = DateTime.Parse(row.StartDate + "Z", CultureInfo.InvariantCulture).ToUniversalTime();
            this.MemberCorporations = AllianceMemberCorporation.GetMemberCorporationsFromXml(row.MemberCorporations);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance"/> class.
        /// </summary>
        /// <param name="name">Alliance Name</param>
        /// <param name="shortName">Alliance Ticker</param>
        /// <param name="id">Alliance Id</param>
        /// <param name="executorCorpId">Alliance Executor Corporation Id</param>
        /// <param name="memberCount">Alliance Member Count</param>
        /// <param name="startDate">Alliance Start Date</param>
        /// <param name="memberCorporations">Alliance Member Corporations</param>
        public Alliance(string name, string shortName, int id, int executorCorpId, int memberCount, DateTime startDate, IEnumerable<AllianceMemberCorporation> memberCorporations)
        {
            this.Name = name;
            this.ShortName = shortName;
            this.Id = id;
            this.ExecutorCorporationId = executorCorpId;
            this.MemberCount = memberCount;
            this.StartDate = startDate;
            this.MemberCorporations = memberCorporations;
        }

        /// <summary>
        /// Gets the name of the Alliance.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Alliance ticker.
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets the Alliance id.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the Corporation Id of the Alliance's Executor Corporation
        /// </summary>
        public int ExecutorCorporationId { get; private set; }

        /// <summary>
        /// Gets the Executor Corporation as an object.
        /// </summary>
        public ICorporation ExecutorCorporation
        {
            get
            {
                if (this.executorCorporation == null)
                {
                    this.executorCorporation = EveOnlineApi.Entities.Corporation.GetCorporationByCorporationId(this.ExecutorCorporationId);
                }

                return this.executorCorporation;
            }
        }

        /// <summary>
        /// Gets the count of members in the Alliance.
        /// </summary>
        public int MemberCount { get; private set; }

        /// <summary>
        /// Gets the start date of the Alliance.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets or sets the Member Corporations in the Alliance.
        /// </summary>
        public IEnumerable<IAllianceMemberCorporation> MemberCorporations { get; set; }

        /// <summary>
        /// Gets an Alliance by its Alliance Id
        /// </summary>
        /// <param name="allianceId">Alliance Id</param>
        /// <returns>Alliance Object</returns>
        public static IAlliance GetAllianceByAllianceId(int allianceId)
        {
            IAllianceDataProvider adp = Injector.Create<IAllianceDataProvider>();
            return adp.GetAllianceInfo(allianceId);
        }
    }
}
