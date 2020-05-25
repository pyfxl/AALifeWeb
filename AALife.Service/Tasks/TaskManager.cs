using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AALife.Service.Tasks
{
    /// <summary>
    /// Represents task manager
    /// </summary>
    public partial class TaskManager
    {
        private static readonly TaskManager _taskManager = new TaskManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();
        private const int _notRunTasksInterval = 60 * 30; //30 minutes

        private TaskManager()
        {
        }
        
        /// <summary>
        /// Initializes the task manager
        /// </summary>
        public void Initialize()
        {
            this._taskThreads.Clear();

            var scheduleTasks = GetTasks();

            //group by threads with the same seconds
            foreach (var scheduleTaskGrouped in scheduleTasks.GroupBy(x => x.Seconds))
            {
                //create a thread
                var taskThread = new TaskThread
                {
                    Seconds = scheduleTaskGrouped.Key
                };
                foreach (var scheduleTask in scheduleTaskGrouped)
                {
                    var task = new Task(scheduleTask);
                    taskThread.AddTask(task);
                }
                this._taskThreads.Add(taskThread);
            }

            //sometimes a task period could be set to several hours (or even days).
            //in this case a probability that it'll be run is quite small (an application could be restarted)
            //we should manually run the tasks which weren't run for a long time
            var notRunTasks = scheduleTasks
                //find tasks with "run period" more than 30 minutes
                .Where(x => x.Seconds >= _notRunTasksInterval)
                .Where(x => !x.LastStartDate.HasValue || x.LastStartDate.Value.AddSeconds(x.Seconds) < DateTime.Now)
                .ToList();
            //create a thread for the tasks which weren't run for a long time
            if (notRunTasks.Any())
            {
                var taskThread = new TaskThread
                {
                    RunOnlyOnce = true,
                    Seconds = 60 * 5 //let's run such tasks in 5 minutes after application start
                };
                foreach (var scheduleTask in notRunTasks)
                {
                    var task = new Task(scheduleTask);
                    taskThread.AddTask(task);
                }
                this._taskThreads.Add(taskThread);
            }
        }

        /// <summary>
        /// Starts the task manager
        /// </summary>
        public void Start()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        /// <summary>
        /// Stops the task manager
        /// </summary>
        public void Stop()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.Dispose();
            }
        }

        /// <summary>
        /// Gets the task mamanger instance
        /// </summary>
        public static TaskManager Instance
        {
            get
            {
                return _taskManager;
            }
        }

        /// <summary>
        /// Gets a list of task threads of this task manager
        /// </summary>
        public IList<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(this._taskThreads);
            }
        }

        /// <summary>
        /// 定时任务列表
        /// </summary>
        /// <returns></returns>
        public static List<ScheduleTask> GetTasks()
        {
            return new List<ScheduleTask>()
            {
                new ScheduleTask()
                {
                    Id = Guid.Parse("E0CCB205-D10B-4384-87A0-150BFA2A17D8"),
                    Name = "保持在线",
                    SystemName = "KeepAliveTask",
                    Seconds = 60,
                    Type = "AALife.Service.Tasks.KeepAliveTask",
                    Enabled = false,
                    StopOnError = true,
                    IsSystem = true
                },
                new ScheduleTask()
                {
                    Id = Guid.Parse("1BE5A616-C999-406C-960A-513FC652F21B"),
                    Name = "推送钉钉消息",
                    SystemName = "SendToDingTask",
                    Seconds = 12 * 60 * 60,
                    Type = "AALife.Service.Tasks.SendToDingTask",
                    Enabled = true,
                    StopOnError = true,
                    IsSystem = true
                }
            };
        }
    }
}
