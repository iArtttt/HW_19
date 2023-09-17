using Library.Common.Interface;

namespace Library
{
    internal static class SelectActionMenu
    {
        internal static int Index { get; private set; } = 0;
        internal static IName Current { get { return NamedList[Index]; } }
        private static bool _isExit = false;
        internal static List<IName> NamedList { get; set; } = new();
        internal static void SearchToManipulate(Action manipulation)
        {
            if (NamedList.Count > 0)
            {


                while (!_isExit)
                {
                    Console.Clear();

                    for (int i = 0; i < NamedList.Count; i++)
                    {
                        if (i == Index)
                        {
                            Console.WriteLine($"{NamedList[i].Name} <--");
                        }
                        else
                        {
                            Console.WriteLine($"{NamedList[i].Name}");
                        }
                    }

                    MoveEnter(manipulation);
                }
                _isExit = false;
                Index = 0;
                NamedList.Clear();
            }
            else
                Info.InformKey("Here is nothing in Library Base");
        }

        private static void MoveEnter(Action manipulation)
        {
            var key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.W:
                    Index = (Index - 1 < 0) ? NamedList.Count - 1 : Index - 1;
                    break;
                case ConsoleKey.UpArrow: goto case ConsoleKey.W;

                case ConsoleKey.S:
                    Index = (Index + 1 > NamedList.Count - 1) ? 0 : Index + 1;
                    break;
                case ConsoleKey.DownArrow: goto case ConsoleKey.S;

                case ConsoleKey.D:
                    try
                    {
                        manipulation.Invoke();
                        Info.Continue();
                    }
                    catch
                    {
                        Index = 0;
                    }
                    break;
                case ConsoleKey.RightArrow: goto case ConsoleKey.D;
                case ConsoleKey.Enter: goto case ConsoleKey.D;

                case ConsoleKey.A:
                    _isExit = true;
                    Index = 0;
                    break;
                case ConsoleKey.Backspace: goto case ConsoleKey.A;
                case ConsoleKey.LeftArrow: goto case ConsoleKey.A;
                case ConsoleKey.Escape: goto case ConsoleKey.A;
            }
        }
    }
}
