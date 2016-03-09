using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EveScanner.Interfaces
{
    public interface IWebClient : IDisposable
    {
        Stream GetUriToStream(Uri uri);

        string GetUriToString(Uri uri);

        void GetUriToFile(Uri uri, string path);

        Stream PostUriToStream(Uri uri, byte[] content);

        string PostUriToString(Uri uri, byte[] content);
    }
}
