using System.Drawing;


namespace FractalsPeer
{
    /// <summary>
    /// Класс - множество кантора.
    /// </summary>
    class KantorSet : Fractal
    {
        // Сдвиг между уровнями и высота элемента.
        private float shift, elementHeight;

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="maxDepth">Максмальная глубина рекурсии.</param>
        /// <param name="width">Ширина картики.</param>
        /// <param name="height">Высота картинки.</param>
        /// <param name="bgColor">Цвет фона.</param>
        /// <param name="startColor">Начальный цвет градиентой отрисовки.</param>
        /// <param name="endColor">Конечный цвет.</param>
        /// <param name="gradient">Нужна ли градиентая отрисовка.</param>
        /// <param name="thickness">Толщина линий.</param>
        /// <param name="rotation">Поворот.</param>
        /// <param name="shift">Сдвиг между уровнями</param>
        public KantorSet(int maxDepth, int width, int height,
            Color bgColor, Color startColor, Color endColor, bool gradient, int thickness, int rotation,
            float shift)
            : base(maxDepth, width, height, bgColor, startColor, endColor, gradient, thickness, rotation)
        {
            this.shift = shift;
            // Считаем, что по высоте фрактал должен занимать 0.7 от картики.
            elementHeight = (float)(0.7 * height - shift * (maxDepth - 1)) / maxDepth;

        }


        /// <summary>
        /// Рекурсивный метод для отрисовки фрактала.
        /// </summary>
        /// <param name="g">Куда рисуем.</param>
        /// <param name="p1">Левая верхняя точка прямоугольника на текущем шаге.</param>
        /// <param name="lenght">Длина элемента.</param>
        /// <param name="lvl">Уровень рекурсии.</param>
        private void DrawSet(Graphics g, PointF p1, float lenght, int lvl)
        {
            var brush = new SolidBrush(GetColor(lvl));
            g.FillRectangle(brush, p1.X, p1.Y, lenght, elementHeight);

            if (lvl == maxDepth)
            {
                return;
            }

            // Выкидываем центральный кусок.
            float y2 = p1.Y + elementHeight + shift;
            float x2 = p1.X + 2f / 3f * lenght;

            DrawSet(g, new PointF(p1.X, y2), lenght / 3, lvl + 1);
            DrawSet(g, new PointF(x2, y2), lenght / 3, lvl + 1);
        }


        public override void DrawFractal()
        {
            base.DrawFractal();


            using (var g = Graphics.FromImage(bmp))
            {
                float x1, y1;

                x1 = width * 0.15f;
                y1 = height * 0.15f;

                DrawSet(g, new PointF(x1, y1), width * 0.7f, 1);

            }

        }



    }
}
