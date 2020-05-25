using System.Net;

namespace AALife.Service.Tasks
{
    /// <summary>
    /// Represents a task for keeping the site alive
    /// </summary>
    public partial class KeepAliveTask : ITask
    {
        /// <summary>
        /// Executes a task
        /// </summary>
        public void Execute()
        {
            string url = "http://www.fxlweb.com/Default.aspx";
            using (var wc = new WebClient())
            {
                wc.DownloadString(url);
            }
        }
    }
}
