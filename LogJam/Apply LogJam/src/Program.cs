using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Diagnostics;

namespace LogJam
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            try
            { 
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
             }
        catch (Exception ex)
            {
                try
                {
                    System.IO.StreamWriter logFile = new System.IO.StreamWriter(@"C:\ProgramData\LogJam.dat", true);
                    logFile.WriteLine(ex.ToString());
                    logFile.Flush();
                    logFile.Close();
                }
                catch (Exception exx)
                {
                    try
                    {
                        EventLog.WriteEntry("Application", exx.ToString(), EventLogEntryType.Error);
                    }
                    catch { }

                }
            }
        }
    }
}
