using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Threading;


namespace LogJam
{
    public partial class Service1 : ServiceBase
    {

        private System.Timers.Timer programKeepalive;
        private System.Timers.Timer performLogout;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadStart start = new ThreadStart(InitLogJam);
            Thread USBMonitorThread = new Thread(start);
            USBMonitorThread.Start();
        }

        protected override void OnStop()
        {
        }

        void InitLogJam()
        {
            programKeepalive = new System.Timers.Timer();
            programKeepalive.Interval = 6000000;
            programKeepalive.Elapsed += new System.Timers.ElapsedEventHandler(checkState);
            programKeepalive.Enabled = true;

            performLogout = new System.Timers.Timer();
            performLogout.Interval = 10000;
            performLogout.Elapsed += new System.Timers.ElapsedEventHandler(issueLogoutCommand);
            performLogout.Enabled = true;
        }

        private void issueLogoutCommand(object sender, ElapsedEventArgs e)
        {

            System.Diagnostics.Process logOut = new Process();
            System.Diagnostics.ProcessStartInfo SI_logout = new System.Diagnostics.ProcessStartInfo();
            SI_logout.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            SI_logout.UseShellExecute = false;
            SI_logout.RedirectStandardOutput = true;
            SI_logout.FileName = "powershell.exe";
            SI_logout.Arguments = "-Command \"quser | ForEach {logoff ($_.tostring() -split ‘ +’)[2]}\"";
            logOut.StartInfo = SI_logout;
            logOut.Start();
            string processOutput = logOut.StandardOutput.ReadToEnd();
            logOut.WaitForExit();

        }

        private void checkState(object sender, ElapsedEventArgs e)
        {
            logToFile("Service reported OK at " + DateTime.Now);

        }

        public static void logToFile(string text)
        {
            try
            {
                System.IO.StreamWriter logFile = new System.IO.StreamWriter(@"C:\ProgramData\LogJam.dat", true);
                logFile.WriteLine(text);
                logFile.Flush();
                logFile.Close();
            }
            catch (Exception e)
            { System.Environment.Exit(5); } //If we can't log anything, no point in even running - exit with Windows 0x5 ERROR_ACCESS_DENIED
        }
    }
}
