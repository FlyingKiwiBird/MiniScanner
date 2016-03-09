using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces
{
    public interface IUIImageGroup
    {
        int UIImageGroupId { get; }
        string UIImageGroupName { get; }
        string UIImagePath { get; }
        bool RequireAny { get; }
        IEnumerable<IImageCriteria> Criteria { get; }
    }

    public interface IImageCriteria
    {
        int CriteriaId { get; }
        bool Evaluate(IScanResult scan);
    }

    public class ImageCriteria : IImageCriteria
    {
        private int criteriaId;
        private string criteriaType;
        private string criteriaTarget;
        private string criteriaValue;

        public int CriteriaId
        {
            get
            {
                return this.criteriaId;
            }
        }

        public bool Evaluate(IScanResult scan)
        {
            throw new NotImplementedException();
        }
    }


}
