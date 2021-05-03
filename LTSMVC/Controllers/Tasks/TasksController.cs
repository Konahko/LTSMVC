using LTSMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LTSMVC.Classes.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LTSMVC.Controllers.Jobs.Tasks
{
    public class TasksController : Controller
    {

        private readonly Lts2Context _context;

        public TasksController(Lts2Context context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index(Char typePage, short page, string searchString)       //TypePage A- Active F - Finised S - Search
        {
            
            if (User.IsInRole("NEW1HORIZONT\\Eban"))
            {

                if (typePage == '\0')
                    typePage = 'A';
                if (page == 0)
                    page = 1;

                if (typePage == 'A')
                {
                    short iAM = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var myActiveTasksId = await _context.StaffsTasks
                        .Where(s => s.Status == 0 && s.StaffId == iAM)
                        .Select(s => s.Task)
                        .Skip(page*25-25)
                        .Take(25)
                        .ToListAsync();

                    var countActiveTasks = await _context.StaffsTasks
                        .Where(s => s.Status == 0 && s.StaffId == iAM)
                        .CountAsync();

                    var countFinishedTasks = await _context.StaffsTasks
                        .Where(s => s.Status != 0 && s.StaffId == iAM)
                        .CountAsync();

                    var taskToLists = new List<TasksToList>();

                    foreach (var item in myActiveTasksId)
                    {
                        int itemInt = Convert.ToInt32(item.ToString());
                        var ActiveTask = await _context.Tasks
                            .Where(s => s.Id == itemInt &&s.Status==0)
                            .FirstOrDefaultAsync();
                        var lastComment = await _context.TasksComments
                            .Include(l => l.Staff)
                            .Where(l => l.Task == itemInt)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefaultAsync();
                        if (ActiveTask.Job.Length > 100)
                            ActiveTask.Job = ActiveTask.Job.Substring(0, 100) + "...";
                        string StaffName = await _context.Staff
                            .Where(s => s.Id == ActiveTask.TaskSendler)
                            .Select(s => s.StaffName)
                            .FirstOrDefaultAsync();

                        if (lastComment != null)
                        {
                            if (lastComment.CommentText.Length > 170)
                                lastComment.CommentText = lastComment.CommentText.Substring(0, 170) + "...";
                            taskToLists.Add(new TasksToList()
                            {
                                Id = Convert.ToInt32(item.ToString()),
                                TaskJob = ActiveTask.Job,
                                DateOpen = ActiveTask.DateOpen,
                                Status = 0,
                                TaskName = ActiveTask.Name,
                                StaffId = ActiveTask.TaskSendler,
                                StaffName = StaffName,
                                DeadLine = ActiveTask.Deadline,
                                LastComment = lastComment.CommentText,
                                DateSend = lastComment.Date,
                                NameSenderLastCommentId = lastComment.FromUser,
                                NameSenderCommentName = lastComment.Staff.StaffName
                            });
                        }
                        else
                            taskToLists.Add(new TasksToList()
                            {
                                Id = Convert.ToInt32(item.ToString()),
                                TaskJob = ActiveTask.Job,
                                DateOpen = ActiveTask.DateOpen,
                                Status = 0,
                                TaskName = ActiveTask.Name,
                                StaffId = ActiveTask.TaskSendler,
                                StaffName = StaffName,
                                DeadLine = ActiveTask.Deadline,
                            });
                    }

                    var result = new TasksList
                    {  
                        CountActiveTasks = countActiveTasks,
                        CountFinishedTasks = countFinishedTasks,
                        PageNum = page,
                        Tasks = taskToLists,
                        TypePage = typePage
                    };
                    return View(result);
                }
                if(typePage =='F')
                {
                    short iAM = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var myFinisedTasks = await _context.StaffsTasks
                        .Where(s => s.Status != 0 && s.StaffId == iAM)
                        .Skip(page * 25 - 25)
                        .Take(25)
                        .ToListAsync();

                    var countActiveTasks = await _context.StaffsTasks
                        .Where(s => s.Status == 0 && s.StaffId == iAM)
                        .CountAsync();

                    var countFinishedTasks = await _context.StaffsTasks
                        .Where(s => s.Status != 0 && s.StaffId == iAM)
                        .CountAsync();

                    var taskToLists = new List<TasksToList>();

                    foreach(var item in myFinisedTasks)
                    {
                        int itemInt = Convert.ToInt32(item.ToString());
                        var lastComment = await _context.TasksComments
                            .Include(l => l.Staff)
                            .Where(l => l.Task == itemInt)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefaultAsync();
                        if (item.Tasks.Job.Length > 100)
                            item.Tasks.Job = item.Tasks.Job.Substring(0, 100) + "...";
                        string StaffName = await _context.Staff
                            .Where(s => s.Id == item.Tasks.TaskSendler)
                            .Select(s => s.StaffName)
                            .FirstOrDefaultAsync();

                        if (lastComment != null)
                        {
                            if (lastComment.CommentText.Length > 170)
                                lastComment.CommentText = lastComment.CommentText.Substring(0, 170) + "...";
                            taskToLists.Add(new TasksToList()
                            {
                                Id = Convert.ToInt32(item.ToString()),
                                TaskJob = item.Tasks.Job,
                                DateOpen = item.Tasks.DateOpen,
                                Status = item.Tasks.Status,
                                TaskName = item.Tasks.Name,
                                StaffId = item.Tasks.TaskSendler,
                                StaffName = StaffName,
                                DeadLine = item.Tasks.Deadline,
                                LastComment = lastComment.CommentText,
                                DateSend = lastComment.Date,
                                NameSenderLastCommentId = lastComment.FromUser,
                                NameSenderCommentName = lastComment.Staff.StaffName
                            });
                        }
                        else
                            taskToLists.Add(new TasksToList()
                            {
                                Id = Convert.ToInt32(item.ToString()),
                                TaskJob = item.Tasks.Job,
                                DateOpen = item.Tasks.DateOpen,
                                Status = 0,
                                TaskName = item.Tasks.Name,
                                StaffId = item.Tasks.TaskSendler,
                                StaffName = StaffName,
                                DeadLine = item.Tasks.Deadline,
                            });
                    }

                    var result = new TasksList
                    {
                        CountActiveTasks = countActiveTasks,
                        CountFinishedTasks = countFinishedTasks,
                        PageNum = page,
                        Tasks = taskToLists,
                        TypePage = typePage

                    };
                    return View(result);
                }
                if (typePage == 'S')
                {
                    short iAM = _context.Staff
                        .Where(s => s.ADName == User.Identity.Name.ToString())
                        .Select(s => s.Id)
                        .FirstOrDefault();

                    var countActiveTasks = await _context.StaffsTasks
                        .Where(s => s.Status == 0 && s.StaffId == iAM)
                        .CountAsync();

                    var countFinishedTasks = await _context.StaffsTasks
                        .Where(s => s.Status != 0 && s.StaffId == iAM)
                        .CountAsync();

                    var SearchedTasks = await _context.StaffsTasks
                        .Include(s => s.Tasks)
                        .Where(s => s.StaffId == iAM &&
                        (EF.Functions.Like(s.Tasks.Job, "%" + searchString + "%") || EF.Functions.Like(s.Tasks.Name, "%" + searchString + "%")))
                        .Skip(page * 25 - 25)
                        .Take(25)
                        .ToListAsync();

                    var tasksToLists = new List<TasksToList>();

                    foreach (var item in SearchedTasks)
                    {
                        var lastComment = await _context.TasksComments
                            .Include(l => l.Staff)
                            .Where(l => l.Task == item.Task)
                            .OrderByDescending(l => l.Id)
                            .Take(1)
                            .FirstOrDefaultAsync();
                        if (item.Tasks.Job.Length > 100)
                            item.Tasks.Job = item.Tasks.Job.Substring(0, 100) + "...";
                        string StaffName = await _context.Staff
                            .Where(s => s.Id == item.Tasks.TaskSendler)
                            .Select(s => s.StaffName)
                            .FirstOrDefaultAsync();
                        if(lastComment!=null)
                            tasksToLists.Add(new TasksToList()
                            {
                                Id = item.Tasks.Id,
                                Status = item.Tasks.Status,
                                DateOpen = item.Tasks.DateOpen,
                                DateClose = item.Tasks.DateClose,
                                StaffName = StaffName,
                                TaskJob = item.Tasks.Job,
                                TaskName = item.Tasks.Name,
                                DeadLine = item.Tasks.Deadline,
                                LastComment = lastComment.CommentText,
                                NameSenderLastCommentId = lastComment.FromUser,
                                NameSenderCommentName = lastComment.Staff.StaffName,
                                DateSend = lastComment.Date
                            });
                        else
                            tasksToLists.Add(new TasksToList()
                            {
                                Id = item.Tasks.Id,
                                Status = item.Tasks.Status,
                                DateOpen = item.Tasks.DateOpen,
                                DateClose = item.Tasks.DateClose,
                                StaffName = StaffName,
                                TaskJob = item.Tasks.Job,
                                TaskName = item.Tasks.Name,
                                DeadLine = item.Tasks.Deadline,
                            });

                    }
                    var result = new TasksList
                    {
                        CountActiveTasks = countActiveTasks,
                        CountFinishedTasks = countFinishedTasks,
                        TypePage = typePage,
                        Tasks = tasksToLists,
                        PageNum = page

                    };

                    return View(result);
                }
                return StatusCode(400);
            }
            else
                return Unauthorized();
        }

        //GET Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");


            return View();
        }

        //POST Create
        [HttpPost]
        public IActionResult Create(string theme, string job, string comment, List<short> selectedStaff, string dateTimeString)
        {
            short user = _context.Staff
                .Where(s => s.ADName == User.Identity.Name)
                .Select(s => s.Id)
                .FirstOrDefault();
            var dateTime = new DateTime();
            dateTime = DateTime.Parse(dateTimeString);

            var task = new LTSMVC.Models.Task();
            task.Name = theme;
            if (job == null)
                job = "Задача не указана";
            task.Job = job;
            task.Status = 0;
            task.DateOpen = DateTime.Now;
            task.TaskSendler = user;
            if(dateTimeString!=null)
            {
                task.Deadline = dateTime;
            }
            _context.Add(task);
            _context.SaveChanges();
            if(comment!=null)
            {
                var taskComment = new TasksComments();
                taskComment.Task = task.Id;
                taskComment.FromUser = user;
                taskComment.Date = DateTime.Now;
                taskComment.CommentText = comment;
                _context.Add(taskComment);
                _context.SaveChanges();
            }
            foreach(var item in selectedStaff)
            {
                var staffsTasks = new StaffsTasks();
                staffsTasks.StaffId = item;
                staffsTasks.Task = task.Id;
                staffsTasks.Status = 0;
                _context.Add(staffsTasks);
                _context.SaveChanges();
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
