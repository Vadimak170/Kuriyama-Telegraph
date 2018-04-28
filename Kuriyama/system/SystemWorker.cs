using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Management;

namespace Kuriyama.system
{
    class SystemWorker
    {
        public SystemWorker()
        {

        }

        public void cmd(string command)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo("cmd", "/C "+command);
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        public void CreateRegKey(string key, string name, string value)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey
                        (key, true);
            rk.SetValue(name, value);
        }

        public bool FindProcess(string process)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {

                if (clsProcess.ProcessName.Contains(process.Replace(".exe", "")))
                {
                    return true;
                }
            }
            return false;
        }

        public void runProcess(string name)
        {
            Process file = new Process();
            file.StartInfo.FileName = name;
            file.Start();
        }
    }
}
