using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Backup_Files
{
    public partial class Form1 : Form
    {
        private readonly Copier.Settings settings = new Copier.Settings();
        private Stopwatch _stopWatch;

        public Form1()
        {
            InitializeComponent();

            Copier.FileManager.LoadSettingsFile(settings.xSettings);
            settings.SettingsLoad();
        }

        private void log(string s)
        {
            txtOutput.AppendText(s + "\r\n");
            txtOutput.Refresh();
        }

        private void btnMakeBackup_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
                backgroundWorker.RunWorkerAsync();
        }


        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            _stopWatch = Stopwatch.StartNew();

            foreach (Copier.Watch foldersToBackup in settings.Watch)
            {
                Copier.FileManager.Backup(foldersToBackup.Original, foldersToBackup.Backup, foldersToBackup.Name, backgroundWorker, false);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            log(e.UserState.ToString());
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            _stopWatch.Stop();
            log("Elapsed Time: " + _stopWatch.Elapsed.Hours + "h " + _stopWatch.Elapsed.Minutes + "m " + _stopWatch.Elapsed.Seconds + "s " + _stopWatch.Elapsed.Milliseconds + "ms");
        }
    }
}
