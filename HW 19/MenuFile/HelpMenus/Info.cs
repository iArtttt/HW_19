namespace Library
{
    internal static class Info
    {
        internal static void Continue()
        {
            Console.WriteLine("Press Any button to continue");
            Console.ReadKey();
        }
        internal static void SuccedKey(string messege)
        {
            Succed(messege);
            Continue();
        }
        internal static void InformKey(string messege)
        {
            Inform(messege);
            Continue();
        }
        internal static void ErrorKey(string messege)
        {
            Error(messege);
            Continue();
        }
        internal static void Succed(string messege)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(messege);
            Console.ResetColor();
        }
        internal static void Inform(string messege)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(messege);
            Console.ResetColor();
        }
        internal static void Error(string messege)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(messege);
            Console.ResetColor();
        }
    }
}
