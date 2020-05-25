using System;

namespace AALife.Service.Tasks
{
    /// <summary>
    /// Schedule task
    /// </summary>
    public partial class ScheduleTask
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the run period (in seconds)
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// Gets or sets the type of appropriate ITask class
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether a task is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether a task should be stopped on some error
        /// </summary>
        public bool StopOnError { get; set; }

        /// <summary>
        /// Gets or sets the datetime when it was started last time
        /// </summary>
        public DateTime? LastStartDate { get; set; }

        /// <summary>
        /// Gets or sets the datetime when it was finished last time (no matter failed ir success)
        /// </summary>
        public DateTime? LastEndDate { get; set; }

        /// <summary>
        /// 是否系统
        /// </summary>
        public bool IsSystem { get; set; }

    }
}
