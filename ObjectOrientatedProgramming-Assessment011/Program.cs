using System;
using System.Text;

namespace ObjectOrientatedProgramming_Assessment011
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(47, 30);
            Console.OutputEncoding = Encoding.Unicode;
            App app = AppHelpers.LoadExisting();
            app.Run();
            Console.Clear();
            AppHelpers.ExitAndSave(app);
            Console.WriteLine("Thank you for playing.");
        }
    }
}
