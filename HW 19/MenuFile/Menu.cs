using Librar.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;

namespace Library
{
    internal class Menu : MenuItem, IMenu
    {
        private int _index = 0;
        private bool _isExit = false;
        private readonly List<IMenuItem> _items = new();
        public IEnumerable<IMenuItem> Items => _items;
        private static DbContextOptionsBuilder<LibraryContext> optionBuilder = new();

        public Menu(string? title, Action? process = null, string? description = null)
            : base(title, process, description)
        { }
        public Menu(string? title, string? description)
            : base(title, null, description)
        { }
        public Menu(string? title)
            : base(title, null, null)
        { }
        internal static void Start()
        {
            Init();

            //DetectMenu<EnterInSystem>().Process();
        }
        private static void Init()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            //optionBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionBuilder.UseSqlServer(configuration.GetConnectionString("Default")).LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Error).UseLazyLoadingProxies();

        }

        public override void Process()
        {

            while (!_isExit)
            {
                Console.Clear();

                Console.WriteLine($"\n{Title}\n");

                for (int i = 0; i < _items.Count; i++)
                {
                    if (i == _index)
                        Console.WriteLine($"{_items[i].Title} <-- {_items[i].Description}");
                    else
                        Console.WriteLine($"{_items[i].Title}");
                }

                MoveEnter();
            }
            _isExit = false;
        }
        private void MoveEnter()
        {
            try
            {

                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.W:
                        _index = (_index - 1 < 0) ? _items.Count - 1 : _index - 1;
                        break;
                    case ConsoleKey.UpArrow: goto case ConsoleKey.W;

                    case ConsoleKey.S:
                        _index = (_index + 1 > _items.Count - 1) ? 0 : _index + 1;
                        break;
                    case ConsoleKey.DownArrow: goto case ConsoleKey.S;

                    case ConsoleKey.D:
                        try
                        {
                            _items[_index].Process();
                        }
                        catch
                        {
                            _index = 0;
                        }
                        break;
                    case ConsoleKey.RightArrow: goto case ConsoleKey.D;
                    case ConsoleKey.Enter: goto case ConsoleKey.D;

                    case ConsoleKey.A:
                        _isExit = true;
                        _index = 0;
                        break;
                    case ConsoleKey.Backspace: goto case ConsoleKey.A;
                    case ConsoleKey.LeftArrow: goto case ConsoleKey.A;
                    case ConsoleKey.Escape: goto case ConsoleKey.A;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddMenuItem(IMenuItem item)
        {
            _items.Add(item);
        }
        internal static Menu DetectMenu<T>() where T : new() => DetectMenu(new Menu("Main Menu"), typeof(T), optionBuilder);
        internal static Menu DetectMenu<T>(params object[] values) where T : new() 
            => DetectMenu(new Menu("Main Menu"), typeof(T), values.Append(optionBuilder).ToArray());

        private static Menu DetectMenu(Menu newMenu, Type typeMenu, params object[] values)
        {

            var obj = Activator.CreateInstance(typeMenu);

            var menuItems = typeMenu.GetMethods()
                .Where(m => m.GetCustomAttribute<MenuActionAttribute>() != null)
                .Select(m =>
                {
                    var attribute = m.GetCustomAttribute<MenuActionAttribute>();
                    return new MenuItem(attribute!.Title, () => { m.Invoke(obj, MapValues(m, values) ); }, attribute.Description);
                });

            var subMenus = typeMenu.GetProperties()
                .Where(p => p.GetCustomAttribute<MenuSubmenuAttribute>() != null)
                .Select(p =>
                {
                    var attribute = p.GetCustomAttribute<MenuSubmenuAttribute>();
                    return new { Menu = new Menu(attribute!.Title ?? p.Name, attribute.Description), Type = p.PropertyType };
                });

            foreach (var menu in subMenus)
            {
                newMenu.AddMenuItem(DetectMenu(menu.Menu, menu.Type, values));
            }
            foreach (var item in menuItems)
            {
                newMenu.AddMenuItem(item);
            }


            return newMenu;
        }

        private static object?[] MapValues(MethodInfo m, object[] values)
        {
            List<object> result = new List<object>();
            
            var valuesList = values.ToList();
            foreach (var item in m.GetParameters())
            {
                var index = valuesList.FindIndex(t => t.GetType().IsAssignableTo(item.ParameterType));
                result.Add(valuesList[index]);
                valuesList.RemoveAt(index);
            }
            return result.ToArray();
        }
    }
}