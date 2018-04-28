using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Kuriyama.net
{
    class Loader
    {
        public Loader()
        {

        }

        public void Download(string url, string name)
        {
            using (WebClient Client = new WebClient())
            {
                FileInfo file = new FileInfo(name);
                Client.DownloadFile(url, file.FullName);   
            }
        }
    }
}
