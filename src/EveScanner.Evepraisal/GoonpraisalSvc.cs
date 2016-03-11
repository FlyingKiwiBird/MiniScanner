//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="GoonpraisalSvc.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Evepraisal
{
    /// <summary>
    /// Stub class to turn EvepraisalSvc into GoonpraisalSvc
    /// </summary>
    public class GoonpraisalSvc : EvepraisalSvc
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoonpraisalSvc"/> class.
        /// </summary>
        public GoonpraisalSvc() : base("goonpraisal.apps.goonswarm.org", true)
        {
        }
    }
}
