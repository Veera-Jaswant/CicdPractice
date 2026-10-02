using CicdPractice.Models;

namespace CicdPractice.Services
{
    public interface ITodoService
    {
        IReadOnlyList<TodoItem> GetAll();
        TodoItem? GetById(int id);
        TodoItem Add(string title);
    }

    public class TodoService : ITodoService
    {
        private readonly List<TodoItem> _items = new();
        private readonly object _lock = new();
        private int _nextId = 1;

        public IReadOnlyList<TodoItem> GetAll()
        {
            lock (_lock)
            {
                return _items.ToList();
            }
        }

        public TodoItem? GetById(int id)
        {
            lock (_lock)
            {
                return _items.FirstOrDefault(x => x.Id == id);
            }
        }

        public TodoItem Add(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is required.", nameof(title));
            }

            lock (_lock)
            {
                var item = new TodoItem
                {
                    Id = _nextId++,
                    Title = title.Trim(),
                    IsDone = false
                };
                _items.Add(item);
                return item;
            }
        }
    }
}
