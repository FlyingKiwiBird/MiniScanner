using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces
{
    public interface IAppraisalService
    {
        IScanResult GetAppraisalFromScan(string data);
        IScanResult GetAppraisalFromUrl(string url);
    }
}
