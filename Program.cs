
using Homework2.Enums;

namespace Homework2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var storage = new JsonStorage("TaskManager");
            var taskManager = new TaskManager(storage);

            var isRunning = true;

            while (isRunning)
            {
                ShowComands();

                Console.Write(">");
                var input = Console.ReadLine();

                if (int.TryParse(input, out var value))
                {
                    switch (value)
                    {
                        case 0:
                            isRunning = false;
                            break;
                        case 1:
                            taskManager.AddNewTaskItem(GetTask());
                            break;
                        case 2:
                            taskManager.ShowAllTasks();
                            Console.WriteLine("Нажмите enter чтобы продолжить");
                            Console.ReadLine();
                            break;
                        case 3:
                            taskManager.SortBy(SortingType.Status);
                            break;
                        case 4:
                            taskManager.SortBy(SortingType.Category);
                            break;
                        case 5:
                            taskManager.SortBy(SortingType.Priority);
                            break;
                        case 6:
                            MarkCompleted(taskManager);
                            break;
                        case 7:
                            DeleteTask(taskManager);
                            break;
                        case 8:
                            taskManager.ShowStatistics();
                            Console.WriteLine("Нажмите enter чтобы продолжить");
                            Console.ReadLine();
                            break;
                        case 9:
                            taskManager.Save();
                            break;
                    }
                }
            }
        }

        private static void DeleteTask(TaskManager taskManager)
        {
            if (taskManager.NumberOfTask == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            taskManager.ShowAllTasks();

            while (true)
            {
                Console.WriteLine("Введите номер который необходимо удалить - ");

                if (int.TryParse(Console.ReadLine(), out var index) && index >= 0 && index <= taskManager.NumberOfTask)
                {
                    taskManager.Delete(index);
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод");
                }
            }
        }

        private static void MarkCompleted(TaskManager taskManager)
        {
            if (taskManager.NumberOfTask == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

            taskManager.ShowAllTasks();

            while (true)
            {
                Console.WriteLine("Введите номер который отметить - ");

                if (int.TryParse(Console.ReadLine(), out var index) && index >= 0 && index <= taskManager.NumberOfTask)
                {
                    taskManager.MarkAsCompleted(index);
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод");
                }
            }
        }

        private static TaskItem GetTask()
        {
            Console.Write("Введите название задачи - ");
            var name = Console.ReadLine();
            Console.Write("Введите описание - ");
            var description = Console.ReadLine();
            Priority priority;
            Category category;

            while (true)
            {
                Console.Write("Выберите приоритет(Low, Medium, High) - ");
                if (Enum.TryParse<Priority>(Console.ReadLine(), true, out var result))
                {
                    priority = result;
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод!!!");
                }
            }

            while (true)
            {
                Console.Write("Выберите категорию(Study, Work, Home, Other) - ");
                if (Enum.TryParse<Category>(Console.ReadLine(), true, out var result))
                {
                    category = result;
                    break;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод!!!");
                }
            }

            return new TaskItem(name ?? "", description, priority, category);
        }

        private static void ShowComands()
        {
            Console.Clear();
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Посмотреть все задачи");
            Console.WriteLine("3. Фильтр по статусу");
            Console.WriteLine("4. Фильтр по категории");
            Console.WriteLine("5. Фильтр по приоритету");
            Console.WriteLine("6. Отметить задачу как выполненную");
            Console.WriteLine("7. Удалить задачу");
            Console.WriteLine("8. Показать статистику");
            Console.WriteLine("9. Сохранить задачи");
            Console.WriteLine("0. Выход");
        }
    }
}
