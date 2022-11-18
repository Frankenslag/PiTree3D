using System.Drawing;
using Wingandprayer.PiTree3D.RgbXmasTree;

namespace Wingandprayer.PiTree3D.RandomSparkles
{
    internal class Program
    {
        private static void Main()
        {
            bool running = true;

            Random rnd = new();

            using RgbXmasTree.RgbXmasTree tree = new() { Mode = BrightnessMode.UseColorAlpha };

            Console.CancelKeyPress += (_, eventArgs) => { eventArgs.Cancel = true; running = false; };

            while (running)
            {
                tree[rnd.Next(0, 24)] = Color.FromArgb(rnd.Next(0, 255) / 2, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                tree.Send();
                //Thread.Sleep(1000);
            }

            tree.Off();
        }
    }
}