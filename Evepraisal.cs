﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace EveScanner
{
    public class Evepraisal
    {
        private string uri = string.Empty;

        public Evepraisal() : this("evepraisal.com", false)
        {
        }

        public Evepraisal(string domain, bool https)
        {
            this.uri = (https ? "https" : "http") + "://" + domain + "/";
        }

        public string GetAppraisal(string data)
        {
            try
            {
                // Let's ask Evepraisal how much the cargo is worth...
                WebRequest req = WebRequest.Create("http://evepraisal.com/estimate");
                req.Method = WebRequestMethods.Http.Post;
                req.ContentType = "application/x-www-form-urlencoded";

                // Build the request string, Market is Jita
                string requestString = "raw_paste=" + data + "&market=30000142";
                Logger.Debug("Request String: {0}", requestString);
                byte[] encodedBytes = Encoding.UTF8.GetBytes(requestString);
                req.ContentLength = encodedBytes.Length;

                Stream ds = null;
                try
                {
                    ds = req.GetRequestStream();
                    ds.Write(encodedBytes, 0, encodedBytes.Length);
                }
                finally
                {
                    if (ds != null)
                    {
                        ds.Close();
                        ds = null;
                    }
                }

                string responseFromServer = string.Empty;

                using (HttpWebResponse rsp = (HttpWebResponse)req.GetResponse())
                {
                    Logger.Debug("Status {0}({1}), Length: {2}", rsp.StatusCode.ToString(), rsp.StatusDescription, rsp.ContentLength.ToString());
                    try
                    {
                        ds = rsp.GetResponseStream();
                        using (StreamReader rdr = new StreamReader(ds))
                        {
                            responseFromServer = rdr.ReadToEnd();
                            Logger.Debug("Response Html: {0}", responseFromServer);
                        }
                    }
                    finally
                    {
                        if (ds != null)
                        {
                            ds.Close();
                            ds = null;
                        }
                    }
                }
                return responseFromServer;
            }
            catch (Exception ex)
            {
                Logger.Debug("GetAppraisal error", ex.ToString());
                throw;
            }
        }
    }
}
