using System.ComponentModel;

namespace DominationsBot.Services
{
    public class TaskRunner
    {
        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public void Start()
        {
            _backgroundWorker.DoWork += backgroundWorker1_DoWork;
            _backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            _backgroundWorker.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.RunWorkerAsync();
        }
        private void backgroundWorker1_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
        }
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void backgroundWorker1_DoWork(object sender,
            DoWorkEventArgs e)
        {
        }
    }
}
