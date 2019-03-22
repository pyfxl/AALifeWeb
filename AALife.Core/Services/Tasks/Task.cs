using System;
using Autofac;
using AALife.Core.Caching;
using AALife.Core.Configuration;
using AALife.Core.Domain.Tasks;
using AALife.Core.Infrastructure;
using AALife.Core.Services.Logging;

namespace AALife.Core.Services.Tasks
{
    /// <summary>
    /// Task
    /// </summary>
    public partial class Task
    {
        #region Ctor

        /// <summary>
        /// Ctor for Task
        /// </summary>
        private Task()
        {
            this.Enabled = true;
        }

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        public Task(ScheduleTask task)
        {
            this.Type = task.Type;
            this.Enabled = task.Enabled;
            this.StopOnError = task.StopOnError;
            this.Name = task.Name;
        }

        #endregion

        #region Utilities

        private ITask CreateTask(ILifetimeScope scope)
        {
            ITask task = null;
            if (this.Enabled)
            {
                var type2 = System.Type.GetType(this.Type);
                if (type2 != null)
                {
                    object instance;
                    if (!EngineContext.Current.ContainerManager.TryResolve(type2, scope, out instance))
                    {
                        //not resolved
                        instance = EngineContext.Current.ContainerManager.ResolveUnregistered(type2, scope);
                    }
                    task = instance as ITask;
                }
            }
            return task;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the task
        /// </summary>
        /// <param name="throwException">A value indicating whether exception should be thrown if some error happens</param>
        /// <param name="dispose">A value indicating whether all instances should be disposed after task run</param>
        /// <param name="ensureRunOnOneWebFarmInstance">A value indicating whether we should ensure this task is run on one farm node at a time</param>
        public void Execute(bool throwException = false, bool dispose = true, bool ensureRunOnOneWebFarmInstance = true)
        {
            //background tasks has an issue with Autofac
            //because scope is generated each time it's requested
            //that's why we get one single scope here
            //this way we can also dispose resources once a task is completed
            var scope = EngineContext.Current.ContainerManager.Scope();
            var scheduleTaskService = EngineContext.Current.ContainerManager.Resolve<IScheduleTaskService>("", scope);
            var scheduleTask = scheduleTaskService.GetTaskByType(this.Type);

            try
            {
                //flag that task is already executed
                var taskExecuted = false;

                //execute task in case if is not executed yet
                if (!taskExecuted)
                {
                    //initialize and execute
                    var task = this.CreateTask(scope);
                    if (task != null)
                    {
                        this.LastStartDate = DateTime.Now;
                        if (scheduleTask != null)
                        {
                            //update appropriate datetime properties
                            scheduleTask.LastStartDate = this.LastStartDate;
                            scheduleTaskService.UpdateTask(scheduleTask);
                        }
                        task.Execute();
                        this.LastEndDate = DateTime.Now;
                    }
                }
            }
            catch (Exception exc)
            {
                this.Enabled = !this.StopOnError;
                this.LastEndDate = DateTime.Now;

                //log error
                var logger = EngineContext.Current.ContainerManager.Resolve<ILogger>("", scope);
                logger.Error(string.Format("Error while running the '{0}' schedule task. {1}", this.Name, exc.Message), exc);
                if (throwException)
                    throw;
            }

            if (scheduleTask != null)
            {
                //update appropriate datetime properties
                scheduleTask.LastEndDate = this.LastEndDate;
                scheduleTaskService.UpdateTask(scheduleTask);
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Datetime of the last start
        /// </summary>
        public DateTime? LastStartDate { get; private set; }

        /// <summary>
        /// Datetime of the last end
        /// </summary>
        public DateTime? LastEndDate { get; private set; }

        /// <summary>
        /// A value indicating type of the task
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// A value indicating whether to stop task on error
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// Get the task name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A value indicating whether the task is enabled
        /// </summary>
        public bool Enabled { get; set; }

        #endregion
    }
}
