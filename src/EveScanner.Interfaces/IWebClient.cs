//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="IWebClient.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.Interfaces
{
    using System;
    using System.IO;

    /// <summary>
    /// Interface for basic requests for information.
    /// </summary>
    public interface IWebClient : IDisposable
    {
        /// <summary>
        /// Returns a stream pointing to a given URI.
        /// </summary>
        /// <param name="uri">Uri to download from</param>
        /// <returns>Stream to content</returns>
        Stream GetUriToStream(Uri uri);

        /// <summary>
        /// Returns the contents of a Uri to a string.
        /// </summary>
        /// <param name="uri">Uri to Download From</param>
        /// <returns>Content as string</returns>
        string GetUriToString(Uri uri);

        /// <summary>
        /// Writes the content of a Uri to a file.
        /// </summary>
        /// <param name="uri">Uri to Download From</param>
        /// <param name="path">File to Write To</param>
        void GetUriToFile(Uri uri, string path);

        /// <summary>
        /// Posts content to a Uri, returning response to a Stream
        /// </summary>
        /// <param name="uri">Uri to Post to</param>
        /// <param name="content">Content to Post</param>
        /// <returns>Response to Stream</returns>
        Stream PostUriToStream(Uri uri, byte[] content);

        /// <summary>
        /// Posts content to a Uri, returning response to a String
        /// </summary>
        /// <param name="uri">Uri to Post to</param>
        /// <param name="content">Content to Post</param>
        /// <returns>Response as String</returns>
        string PostUriToString(Uri uri, byte[] content);
    }
}
