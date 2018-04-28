using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace Kuriyama.info
{
    class SysInfo
    {
        public SysInfo()
        {

        }

        public string hwid()
        {

            string HoldingAdress = "";
            try
            {
                string drive = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"" + drive + ":\"");
                disk.Get();
                string diskLetter = (disk["VolumeSerialNumber"].ToString());
                HoldingAdress = diskLetter;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return HoldingAdress;
        }

        public string os()
        {
            var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        select x.GetPropertyValue("Caption")).FirstOrDefault();
            return name != null ? name.ToString() : "Unknown";
        }

        public string username()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

        public int kernels()
        {
            return Environment.ProcessorCount;
        }
    }
}
