using System.Drawing;

namespace Wingandprayer.PiTree3D.RGB
{
    internal class Program
    {
        private static void Main()
        {
            bool running = true;

            Color[] colours =
            {
                Color.Red,
                Color.Green,
                Color.Blue
            };

            using RgbXmasTree.RgbXmasTree tree = new();

            Console.CancelKeyPress += (_, eventArgs) => { eventArgs.Cancel = true; running = false; };

            while (running)
            {
                foreach (Color c in colours)
                {
                    tree.Color = c;
                    Thread.Sleep(1000);
                }
            }

            tree.Off();
        }
    }
}