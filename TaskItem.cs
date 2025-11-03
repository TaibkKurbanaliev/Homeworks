using Homework2.Enums;

namespace Homework2
{

    public class TaskItem
    {
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Priority Priority { get; private set; }
        public Category Category { get; private set; }
        public Status Status {  get; private set; }

        public TaskItem(string name, string? description, Priority priority, Category category)
        {
            Name = name;
            Description = description;
            Priority = priority;
            Category = category;
            Status = Status.New;
        }

        public string GetInfo()
        {
            return $"Название задачи - {Name}\nОписание - {Description ?? "Отсутвует"}\n" +
                $"Приоритет - {Priority}\nКатегория - {Category}\nСтатус - {Status}";
        }

        public void MarkCompleted()
        {
            Status = Status.Done;
        }
    }
}
