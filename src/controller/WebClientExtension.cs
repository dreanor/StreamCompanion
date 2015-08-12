using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace com.gmail.mikeundead.streamcompanion.controller
{
    public class WebClientExtension : WebClient
    {
        public bool HeadOnly { get; set; }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest req = base.GetWebRequest(address);
            if (HeadOnly && req.Method == "GET")
            {
                req.Method = "HEAD";
            }
            return req;
        }
    }
}
