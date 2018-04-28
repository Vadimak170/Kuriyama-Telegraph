using System;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
namespace Kuriyama.vmdetect
{
    class VmWorker
    {
        public VmWorker()
        {

        }

        public void Run()
        {
            if (DetectVirtualMachine())
                Quit();
        }
        
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string running);
        public bool DetectVirtualMachine()
        {
            using (var searcher = new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem"))
            {
                using (var items = searcher.Get())
                {
                    foreach (var item in items)
                    {
                        string manufacturer = item["Manufacturer"].ToString().ToLower();
                        if ((manufacturer == "microsoft corporation" && item["Model"].ToString().ToUpperInvariant().Contains("VIRTUAL"))
                            || manufacturer.Contains("vmware")
                            || item["Model"].ToString() == "VirtualBox" || GetModuleHandle("cmdvrt32.dll").ToInt32() != 0 || GetModuleHandle("SxIn.dll").ToInt32() != 0
                   || GetModuleHandle("SbieDll.dll").ToInt32() != 0 || GetModuleHandle("Sf2.dll").ToInt32() != 0 ||
                   GetModuleHandle("snxhk.dll").ToInt32() != 0)
                        {
                            return true;
                        }

                       
                        var hypervisorPresentProperty
                          = item.Properties
                                .OfType<PropertyData>()
                                .FirstOrDefault(p => p.Name == "HypervisorPresent");

                        if ((bool?)hypervisorPresentProperty?.Value == true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void Quit()
        {
            Environment.Exit(0);
        }
    }
}
