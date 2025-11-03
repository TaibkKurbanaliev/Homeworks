using Homework2.Enums;
using Newtonsoft.Json;

namespace Homework2
{
    public class TaskManager
    {

        private List<TaskItem> _tasks = new List<TaskItem>();
        private IStorage _storage;

        public TaskManager(IStorage storage)
        {
            _storage = storage;
            Load();
        }
        
        public int NumberOfTask => _tasks.Count; 

        public void AddNewTaskItem(TaskItem item)
        {
            _tasks.Add(item);
        }

        public void ShowAllTasks()
        {
            for (int i = 0; i < _tasks.Count; i++) 
            {
                Console.WriteLine($"{i}: {_tasks[i].GetInfo()}");
            }
        }

        public void SortBy(SortingType sortingType)
        {
            switch (sortingType)
            {
                case SortingType.Category:
                    _tasks = _tasks.OrderBy(t => t.Category).ToList();
                    break;
                case SortingType.Priority:
                    _tasks = _tasks.OrderBy(t => t.Priority).ToList();
                    break;
                case SortingType.Status:
                    _tasks = _tasks.OrderBy(t => t.Status).ToList();
                    break;
                default:
                    throw new NotImplementedException(nameof(sortingType));
            }
        }

        public void ShowStatistics()
        {
            var statistics = _tasks.GroupBy(t => t.Status)
                                    .Select(g => new { Status = g.Key, Count = g.Count() })
                                    .ToList();

            foreach (var task in statistics)
            {
                Console.WriteLine($"{task.Status} - {task.Count}");
            } 
        }

        public void MarkAsCompleted(int index)
        {
            if (index >= _tasks.Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            _tasks[index].MarkCompleted();
        }

        public void Delete(int index)
        {
            if (index >= _tasks.Count || index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            _tasks.RemoveAt(index);
        }

        public void Save()
        {
            var saveObject = new TaskManagerData()
            {
                Items = _tasks
            };

            _storage.Save(saveObject);
        }

        public void Load()
        {
            var data = _storage.Load<TaskManagerData>();

            if (data != null)
                _tasks = data.Items;
        }
    }

    public class TaskManagerData
    {
        public List<TaskItem> Items;
    }
}
