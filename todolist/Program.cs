using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList
{
    public class Task
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; } //date
        public string Priority { get; set; } //high / medium / low
        public bool IsCompleted { get; set; }

        public Task(string id, string name, DateTime dueDate, string priority)
        {
            Id = id;
            Name = name;
            DueDate = dueDate;
            Priority = priority;
            IsCompleted = false;
        }

        public void Display()
        {
            string status = IsCompleted ? "[finish]" : "[in progress]";
            Console.WriteLine($"{Id}. {status} {Name} | Due: {DueDate:dd.MM.yyyy} | Priority: {Priority}");
        }
    }

    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(string id, string name, DateTime dueDate, string priority)
        {
            if (tasks.Any(t => t.Id == id))
            {
                Console.WriteLine($"Task with this ID '{id}' already exists");
                return;
            }

            tasks.Add(new Task(id, name, dueDate, priority));
            Console.WriteLine("Task added successfully!");
        }

        public void EditTask(string id, string newName, DateTime newDueDate, string newPriority)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found");
                return;
            }

            task.Name = newName;
            task.DueDate = newDueDate;
            task.Priority = newPriority;
            Console.WriteLine("Task updated");
        }

        public void DeleteTask(string id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found!");
                return;
            }

            tasks.Remove(task);
            Console.WriteLine("Task deleted!");
        }

        public void MarkComplete(string id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                Console.WriteLine("Task not found!");
                return;
            }

            task.IsCompleted = true;
            Console.WriteLine("Task marked as complete!");
        }

        public void ViewAllTasks()
        {
            Console.Clear();
            Console.WriteLine("=== ALL TASKS ===");

            
            
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var task in tasks)
                    task.Display();

                Console.WriteLine($"\nTotal: {tasks.Count} tasks");
            }

            Pause();
        }

        public void ViewByDate()
        {
            Console.Clear();
            Console.WriteLine("=== TASKS SORTED BY DATE ===");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var task in tasks.OrderBy(t => t.DueDate))
                    task.Display();
            }

            Pause();
        }

        public void ViewByPriority()
        {
            Console.Clear();
            Console.WriteLine("=== TASKS SORTED BY PRIORITY ===");

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                var sorted = tasks.OrderBy(t =>
                {
                    return t.Priority switch
                    {
                        "high" => 1,    // 3
                        "medium" => 2,  // 2
                        "low" => 3,     // 1
                        _ => 4
                    };
                });

                foreach (var task in sorted)
                    task.Display();
            }

            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }

        public bool TaskExists(string id)
        {
            return tasks.Any(t => t.Id == id);
        }
    }

    class Program
    {
        static void Main()
        {
            TaskManager manager = new TaskManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("(=== TO-DO LIST ===)");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Edit Task");
                Console.WriteLine("3. Delete Task");
                Console.WriteLine("4. Mark Complete");
                Console.WriteLine("5. View All Tasks");
                Console.WriteLine("6. View by Date");
                Console.WriteLine("7. View by Priority ");
                Console.WriteLine("8. Exit");
                Console.Write("Choose:   ");

                switch (Console.ReadLine())
                {
                    case "1": AddTask(manager); break;
                    case "2": EditTask(manager); break;
                    case "3": DeleteTask(manager); break;
                    case "4": MarkComplete(manager); break;
                    case "5": manager.ViewAllTasks(); break;
                    case "6": manager.ViewByDate(); break;
                    case "7": manager.ViewByPriority(); break;
                    case "8": return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AddTask(TaskManager manager)
        {
            Console.Clear();
            Console.Write("ID: ");
            string id = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Due date (dd.mm.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date!");
                Console.ReadKey();
                return;
            }

            Console.Write("Priority (high/medium/low): ");
            string priority = Console.ReadLine().ToLower();

            manager.AddTask(id, name, date, priority);
            Console.ReadKey();
        }

        static void EditTask(TaskManager manager)
        {
            Console.Clear();
            Console.Write("Task ID: ");
            string id = Console.ReadLine();

            
            if (!manager.TaskExists(id))
            {
                Console.WriteLine("Task not found!");
                Console.ReadKey();
                return;
            }

            Console.Write("New name: ");
            string name = Console.ReadLine();

            
            Console.Write("New due date (dd.mm.yyyy): ");
            if (!DateTime.TryParseExact(Console.ReadLine(), "dd.mm.yyyy", null,
                System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                Console.WriteLine("Invalid date!");
                Console.ReadKey();
                return;
            }
            Console.Write("New priority (high/medium/low): ");
            string priority = Console.ReadLine().ToLower();

            manager.EditTask(id, name, date, priority);
            Console.ReadKey();
        }

        static void DeleteTask(TaskManager manager)
        {
            Console.Clear();
            Console.Write("Task ID: ");
            manager.DeleteTask(Console.ReadLine());
            Console.ReadKey();
        }

        
        
        static void MarkComplete(TaskManager manager)
        {
            Console.Clear();
            Console.Write("Task ID: ");
            manager.MarkComplete(Console.ReadLine());
            Console.ReadKey();
        }
    }
}