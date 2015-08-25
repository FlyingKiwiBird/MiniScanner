using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces
{
    public delegate string ClipboardDataArrived(object sender, EventArgs e);
    
    interface IClipboardProvider : IDisposable
    {
        event ClipboardDataArrived DataArrived;
    }
}
