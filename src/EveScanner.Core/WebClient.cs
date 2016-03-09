//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="WebClient.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Core
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;

    using EveScanner.Interfaces;

    /// <summary>
    /// Contains basic requests for information using HttpWebRequest
    /// </summary>
    public class WebClient : IWebClient
    {
        /// <summary>
        /// Determines if the instance is disposed.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Holds the response which can't be closed until after the stream is read.
        /// </summary>
        private HttpWebResponse response = null;

        /// <summary>
        /// Writes the content of a Uri to a file.
        /// </summary>
        /// <param name="uri">Uri to Download From</param>
        /// <param name="path">File to Write To</param>
        public void GetUriToFile(Uri uri, string path)
        {
            using (FileStream stream = File.Create(path))
            {
                this.GetUriToStream(uri).CopyTo(stream);
            }
        }

        public Stream GetUriToStream(Uri uri)
        {
            return this.WebRequester(uri, WebRequestMethods.Http.Get, null, null);
        }

        public string GetUriToString(Uri uri)
        {
            Stream stream = this.GetUriToStream(uri);

            using (StreamReader sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }

        public Stream PostUriToStream(Uri uri, byte[] content)
        {
            return this.WebRequester(uri, WebRequestMethods.Http.Post, content, "application/x-www-form-urlencoded");
        }

        public string PostUriToString(Uri uri, byte[] content)
        {
            using (StreamReader sr = new StreamReader(this.PostUriToStream(uri, content)))
            {
                return sr.ReadToEnd();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                if (this.response != null)
                {
                    this.response.Close();
                    this.response = null;
                }
            }

            this.disposed = true;
        }

        private Stream WebRequester(Uri uri, string method, byte[] content, string contentType)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Method = method;

            if (!string.IsNullOrEmpty(contentType))
            {
                webrequest.ContentType = contentType;
            }

            if (content != null && content.Length > 0)
            {
                webrequest.ContentLength = content.Length;

                using (Stream ds = webrequest.GetRequestStream())
                {
                    ds.Write(content, 0, content.Length);
                }
            }

            this.response = (HttpWebResponse)webrequest.GetResponse();

            Logger.Debug("Status {0}({1}), Length: {2}", this.response.StatusCode.ToString(), this.response.StatusDescription, this.response.ContentLength.ToString(CultureInfo.InvariantCulture));
            return this.response.GetResponseStream();
        }
    }
}
