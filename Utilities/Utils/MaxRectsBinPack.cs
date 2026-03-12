using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS
{
    public class RectangleModula
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }

        public RectangleModula(int id, int width, int height,string code)
        {
            Id = id;
            Width = width;
            Height = height;
            Code = code;
        }

        public (double X, double Y) GetCenter() => (X + Width / 2.0, Y + Height / 2.0);
    }
    public class MaxRectsBinPack
    {
        private int binWidth;
        private int binHeight;
        private List<RectangleModula> freeRectangles;

        public MaxRectsBinPack(int width, int height)
        {
            binWidth = width;
            binHeight = height;
            freeRectangles = new List<RectangleModula> { new RectangleModula(0, width, height, "") };
        }

        public (List<RectangleModula> placed, List<RectangleModula> notPlaced) Pack(List<RectangleModula> rectangles)
        {
            List<RectangleModula> placedRectangles = new List<RectangleModula>();
            List<RectangleModula> notPlacedRectangles = new List<RectangleModula>();

            foreach (var rect in rectangles)
            {
                var (bestX, bestY, bestScore) = FindBestPosition(rect);
                if (bestX == -1)
                {
                    notPlacedRectangles.Add(rect);
                    continue;
                }

                rect.X = bestX;
                rect.Y = bestY;
                placedRectangles.Add(rect);
                UpdateFreeRectangles(rect);
            }

            return (placedRectangles, notPlacedRectangles);
        }

        private (int x, int y, int score) FindBestPosition(RectangleModula rect)
        {
            int bestX = -1;
            int bestY = -1;
            int bestScore = int.MaxValue;

            foreach (var freeRect in freeRectangles)
            {
                if (rect.Width <= freeRect.Width && rect.Height <= freeRect.Height)
                {
                    int score = Math.Min(freeRect.Width - rect.Width, freeRect.Height - rect.Height);
                    if (score < bestScore)
                    {
                        bestScore = score;
                        bestX = freeRect.X;
                        bestY = freeRect.Y;
                    }
                }
            }

            return (bestX, bestY, bestScore);
        }

        private void UpdateFreeRectangles(RectangleModula placedRect)
        {
            List<RectangleModula> newFreeRectangles = new List<RectangleModula>();
            foreach (var freeRect in freeRectangles)
            {
                if (freeRect.X >= placedRect.X + placedRect.Width ||
                    freeRect.X + freeRect.Width <= placedRect.X ||
                    freeRect.Y >= placedRect.Y + placedRect.Height ||
                    freeRect.Y + freeRect.Height <= placedRect.Y)
                {
                    newFreeRectangles.Add(freeRect);
                    continue;
                }

                if (freeRect.X < placedRect.X + placedRect.Width && freeRect.X + freeRect.Width > placedRect.X)
                {
                    if (freeRect.Y < placedRect.Y)
                        newFreeRectangles.Add(new RectangleModula(0, freeRect.Width, placedRect.Y - freeRect.Y,"") { X = freeRect.X, Y = freeRect.Y });
                    if (freeRect.Y + freeRect.Height > placedRect.Y + placedRect.Height)
                        newFreeRectangles.Add(new RectangleModula(0, freeRect.Width, freeRect.Y + freeRect.Height - (placedRect.Y + placedRect.Height),"") { X = freeRect.X, Y = placedRect.Y + placedRect.Height });
                }

                if (freeRect.Y < placedRect.Y + placedRect.Height && freeRect.Y + freeRect.Height > placedRect.Y)
                {
                    if (freeRect.X < placedRect.X)
                        newFreeRectangles.Add(new RectangleModula(0, placedRect.X - freeRect.X, freeRect.Height, "") { X = freeRect.X, Y = freeRect.Y });
                    if (freeRect.X + freeRect.Width > placedRect.X + placedRect.Width)
                        newFreeRectangles.Add(new RectangleModula(0, freeRect.X + freeRect.Width - (placedRect.X + placedRect.Width), freeRect.Height, "") { X = placedRect.X + placedRect.Width, Y = freeRect.Y });
                }
            }

            freeRectangles = newFreeRectangles;
        }
    }
}
