using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTSMVC.Classes.Tasks
{
    public class TasksList
    {
        public int CountActiveTasks;
        public int CountFinishedTasks;
        public char TypePage;
        public short PageNum;
        public List<TasksToList> Tasks;
    }
}
