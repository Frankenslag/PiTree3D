using System.Drawing;

namespace Wingandprayer.PiTree3D.Swirl
{
    internal class Program
    {
        private static readonly int[] SpiralData = { 12, 6, 15, 16, 0, 7, 19, 24, 11, 5, 14, 17, 1, 8, 20, 23, 10, 4, 13, 18, 2, 9, 21, 22, 3 };
        private static readonly int[][] LayersData = { new[] { 12, 6, 15, 16, 0, 7, 19, 24 }, new[] { 11, 5, 14, 17, 1, 8, 20, 23 }, new[] { 10, 4, 13, 18, 2, 9, 21, 22 } };
        private static readonly int[][] RotateData = { new[] { 12, 11, 10 }, new[] { 6, 5, 4 }, new[] { 15, 14, 13 }, new[] { 16, 17, 18 }, new[] { 0, 1, 2 }, new[] { 7, 8, 9 }, new[] { 19, 20, 21 }, new[] { 24, 23, 22 } };
        private static readonly Color[] ColorData = { Color.Cyan, Color.Red, Color.Purple, Color.White, Color.Green, Color.Blue, Color.Yellow, Color.Magenta };
        private const int TopLed = 3;

        private static void SetLed(RgbXmasTree.RgbXmasTree tree, int index, Color c)
        {
            tree[SpiralData[index]] = c;
            tree.Send();
            Thread.Sleep(10);
        }

        private static void Spiral(RgbXmasTree.RgbXmasTree tree)
        {
            for (int i = 0; i < SpiralData.Length; i++)
            {
                // ReSharper disable once PossibleLossOfFraction
                SetLed(tree, i, Hsv.FromHsv(0.8, i / SpiralData.Length, 1));
            }

            for (int i = SpiralData.Length - 1; i >= 0; i--)
            {
                // ReSharper disable once PossibleLossOfFraction
                SetLed(tree, i, Hsv.FromHsv(0.8, i / SpiralData.Length, 1));
            }
        }

        private static void Layers(RgbXmasTree.RgbXmasTree tree, Random rnd)
        {
            foreach (int[] ary in LayersData)
            {
                foreach (int i in ary)
                {
                    SetLed(tree, i, Hsv.FromHsv(rnd.Next(0, 255), 1, 1));
                }

                SetLed(tree, TopLed, Color.White);
            }
        }

        private static void Rotate(RgbXmasTree.RgbXmasTree tree)
        {
            for (int i = 0; i < RotateData.GetLength(0); i++)
            {
                foreach (int v in RotateData[i])
                {
                    SetLed(tree, v, ColorData[i]);
                }
            }
        }

        private static void Main()
        {
            bool running = true;

            Random rnd = new();

            using RgbXmasTree.RgbXmasTree tree = new() { Brightness = 0.05F };

            Console.CancelKeyPress += (_, eventArgs) =>
            {
                eventArgs.Cancel = true;
                running = false;
            };

            while (running)
            {
                tree.Off();
                Spiral(tree);

                tree.Off();
                Layers(tree, rnd);

                tree.Off();
                Rotate(tree);
            }

            tree.Off();
        }

    }
}
