using System.Drawing;
using Wingandprayer.PiTree3D.RgbXmasTree;

namespace Wingandprayer.PiTree3D.Tinsel
{
    internal class Program
    {
        private static void Main()
        {
            bool running = true;

            Random rnd = new();

            using RgbXmasTree.RgbXmasTree tree = new() { IndexMode = IndexMode.Tinsel, Mode = BrightnessMode.UseMasterBrightness, Brightness = 16 };

            Console.CancelKeyPress += (_, eventArgs) => { eventArgs.Cancel = true; running = false; };

            while (running)
            {
                for (int i = 0; i < 25 & running; i++)
                {
                    tree.Off();
                    tree[(i + 0) % 25] = Color.Red;
                    tree[(i + 1) % 25] = Color.Green;
                    tree[(i + 2) % 25] = Color.Green;
                    tree[(i + 3) % 25] = Color.Red;
                    tree.Send();
                    Thread.Sleep(rnd.Next(5, 100));
                }
            }

            tree.Off();
        }
    }
}