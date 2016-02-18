﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSIRT.Loggers
{
    public class WebsiteLog : BaseLog
    {
        private string url = "";

        public WebsiteLog(string loadedURL, string action) : base(action)
        {
            this.URL = loadedURL;

        }

        public WebsiteLog(string loadedURL) : this(loadedURL, "Loaded") { }
       


        public string URL
        {
            get
            {
                return url;
            }
            set
            {
                this.url = value;
            }
        }

    }
}
