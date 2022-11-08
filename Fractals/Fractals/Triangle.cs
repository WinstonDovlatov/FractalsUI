using System;
using System.Drawing;


namespace FractalsPeer
{
    class Triangle : Fractal
    {
        /// <summary>
        /// Основной конструктор.
        /// </summary>

        public Triangle(int maxDepth, int width, int height,
            Color bgColor, Color startColor, Color endColor, bool gradient, int thickness, int rotation)
            : base(maxDepth, width, height, bgColor, startColor, endColor, gradient, thickness, rotation)
        {

        }

        /// <summary>
        /// Рекурсивный метод для отрисовки фрактала.
        /// </summary>
        /// <param name="g">Куда рисуем.</param>
        /// <param name="p1">Первая точка треугольника.</param>
        /// <param name="p2">Вторая точка треугольника.</param>
        /// <param name="p3">Третья точка треугольника.</param>
        /// <param name="lvl">Уровень рекурсии.</param>
        private void DrawTriangle(Graphics g, PointF p1, PointF p2, PointF p3, int lvl)
        {
            Pen pen = new Pen(GetColor(lvl), thickness);

            if (lvl == 1)
            {
                g.DrawLine(pen, p1, p2);
                g.DrawLine(pen, p3, p1);
            }

            g.DrawLine(pen, p2, p3);


            if (lvl == maxDepth)
            {
                return;
            }

            PointF pt12, pt23, pt31;

            pt12 = new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            pt23 = new PointF((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);
            pt31 = new PointF((p3.X + p1.X) / 2, (p3.Y + p1.Y) / 2);

            DrawTriangle(g, p1, pt12, pt31, lvl + 1);
            DrawTriangle(g, p2, pt12, pt23, lvl + 1);
            DrawTriangle(g, p3, pt23, pt31, lvl + 1);


        }

        public override void DrawFractal()
        {
            base.DrawFractal();


            using (var g = Graphics.FromImage(bmp))
            {
                float x1, x2, y1, x3, y3;

                int side = (Math.Min(width, height) * 4) / 5;

                x1 = (width - side) / 2f;
                y1 = (height - side) / 2f;
                x2 = (width - side) / 2f + side;
                x3 = (x1 + x2) / 2f;
                y3 = y1 + side * MathF.Sin(MathF.PI / 3);


                DrawTriangle(g, new PointF(x1, y1), new PointF(x2, y1), new PointF(x3, y3), 1);

            }

        }
    }
}
