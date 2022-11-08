using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace FractalsPeer
{
    /// <summary>
    /// Класс - кривая Коха.
    /// </summary>
    class KohCurve : Fractal
    {
        /// <summary>
        /// Основной конструктор.
        /// </summary>
        public KohCurve(int maxDepth, int width, int height,
            Color backgroundColor, Color startColor, Color endColor, bool gradient, int thickness,
            int rotation) :
            base(maxDepth, width, height,
                 backgroundColor, startColor, endColor, gradient, thickness, rotation)
        {

        }


        /// <summary>
        /// Метод для рекурсивной отрисовки фрактала.
        /// </summary>
        /// <param name="g">Куда рисуем.</param>
        /// <param name="p1">Левая точка отрезка.</param>
        /// <param name="p2">Правая точка отрезка.</param>
        /// <param name="lvl">Уровень рекурсии.</param>
        /// <param name="paintingLvl">Уровень, согласно которому необходимо отрисовать элемент.</param>
        private void DrawCurve(Graphics g, PointF p1, PointF p2, int lvl, int paintingLvl)
        {
            if (maxDepth == lvl)
            {
                g.DrawLine(new Pen(gradient ? GetColor(paintingLvl) : endColor,
                                   thickness == 1 ? (maxDepth - paintingLvl) * (2 / 3f) : thickness),
                                   p1, p2);
                return;
            }

            PointF pn1, pn3, vector, pn2;

            //         pn2
            //         / \
            //        /   \
            // p1---pn1   pn3---p2

            pn1 = new PointF(p1.X + (p2.X - p1.X) * (1f / 3),
                                   p1.Y + (p2.Y - p1.Y) * (1f / 3));
            pn3 = new PointF(p1.X + (p2.X - p1.X) * (2f / 3),
                                   p1.Y + (p2.Y - p1.Y) * (2f / 3));

            vector = new PointF(pn3.X - pn1.X, pn3.Y - pn1.Y);

            float sn = MathF.Sin(MathF.PI / 3f);
            float cs = MathF.Cos(MathF.PI / 3f);
            // Домножаем на матрицу поворота на 60 градусов.

            pn2 = new PointF(pn1.X + vector.X * cs - vector.Y * sn,
                             pn1.Y + vector.X * sn + vector.Y * cs);

            DrawCurve(g, p1, pn1, lvl + 1, paintingLvl);
            DrawCurve(g, pn1, pn2, lvl + 1, lvl + 1);
            DrawCurve(g, pn2, pn3, lvl + 1, lvl + 1);
            DrawCurve(g, pn3, p2, lvl + 1, paintingLvl);

        }

        public override void DrawFractal()
        {
            base.DrawFractal();

            using (var g = Graphics.FromImage(bmp))
            {
                PointF p1, p2;


                p1 = new PointF(width * 0.07f, height * 0.3f);
                p2 = new PointF(width * 0.93f, height * 0.3f);


                DrawCurve(g, p1, p2, 1, 1);

            }


        }
    }
}
