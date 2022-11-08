using System;
using System.Drawing;


namespace FractalsPeer
{
    /// <summary>
    /// Класс - ковер Серпинского.
    /// </summary>
    internal class Carpet : Fractal
    {
        Brush brush;
        public Carpet(int maxDepth, int width, int height,
            Color bgColor, Color startColor, Color endColor, bool gradient, int thickness, int rotation)
            : base(maxDepth, width, height, bgColor, startColor, endColor, gradient, thickness, rotation)
        {

        }

        /// <summary>
        /// Рекурсивная отрисовка фрактала.
        /// </summary>
        /// <param name="g">Куда рисуем.</param>
        /// <param name="p1">Левая верхняя точка квадрата на текущей итерации.</param>
        /// <param name="side">Длина стороны на текущей интерации.</param>
        /// <param name="lvl">Номер итерации.</param>
        private void DrawCarpet(Graphics g, Point p1, int side, int lvl)
        {
            if (lvl == maxDepth)
            {
                return;
            }

            // Разделим на сетку 3х3.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Если центральный сигмент.
                    if (i == 1 && j == 1)
                    {
                        if (gradient)
                        {
                            brush = new SolidBrush(GetColor(lvl + 1));
                        }
                        else
                        {
                            brush = new SolidBrush(backgroundColor);
                        }
                        g.FillRectangle(brush,
                            new Rectangle(p1.X + side, p1.Y + side, side, side));
                    }
                    else
                    {
                        DrawCarpet(g, new Point(p1.X + side * i, p1.Y + side * j), side / 3, lvl + 1);
                    }
                }
            }
        }

        public override void DrawFractal()
        {
            base.DrawFractal();


            using (var g = Graphics.FromImage(bmp))
            {


                int side = (Math.Min(width, height) * 4) / 5;
                int x = (width - side) / 2;
                int y = (height - side) / 2;

                g.FillRectangle(new SolidBrush(startColor),
                            new Rectangle(x, y, side, side));

                DrawCarpet(g, new Point(x, y), side / 3, 1);

            }

        }

    }
}
