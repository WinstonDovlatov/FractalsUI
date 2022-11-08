using System;
using System.Drawing;

namespace FractalsPeer
{
    // Родительский класс - фрактал.
    internal class Fractal
    {
        // Максимальная глубина рекурсии и толщина линий, если таковые участвуют в построении.
        public int maxDepth, thickness;
        // Картинка.
        protected Bitmap bmp;
        // Нужна ли градиентная отрисовка.
        public bool gradient;
        // Цвета.
        public Color startColor, endColor, backgroundColor;
        // Высота, ширина, поворот картинки.
        public int width, height, rotation;

        public Fractal()
        {

        }


        /// <summary>
        /// Основной конструктор класса.
        /// </summary>
        /// <param name="depth">Максмальная глубина рекурсии.</param>
        /// <param name="width">Ширина картики.</param>
        /// <param name="height">Высота картинки.</param>
        /// <param name="backgroundColor">Цвет фона.</param>
        /// <param name="startColor">Начальный цвет градиентой отрисовки.</param>
        /// <param name="endColorInp">Конечный цвет.</param>
        /// <param name="gradient">Нужна ли градиентая отрисовка.</param>
        /// <param name="thickness">Толщина линий.</param>
        /// <param name="rotation">Поворот.</param>
        public Fractal(int depth, int width, int height,
            Color backgroundColor, Color startColor, Color endColorInp, bool gradient, int thickness, int rotation)
        {
            this.thickness = thickness;
            maxDepth = depth;
            this.startColor = startColor;
            this.backgroundColor = backgroundColor;
            endColor = endColorInp;
            this.width = rotation % 2 == 0 ? width : height;
            this.height = rotation % 2 == 0 ? height : width;
            this.gradient = gradient;
            this.rotation = rotation;
        }


        /// <summary>
        /// Метод для получения цвета градиентного окрашивания на текущем шаге рекурсии.
        /// </summary>
        /// <param name="step">Номер шага рекурсии.</param>
        /// <returns>Цвет.</returns>
        public Color GetColor(int step)
        {
            step--;
            int stepR = (endColor.R - startColor.R) / Math.Max(maxDepth - 1, 1);
            int stepG = (endColor.G - startColor.G) / Math.Max(maxDepth - 1, 1);
            int stepB = (endColor.B - startColor.B) / Math.Max(maxDepth - 1, 1);

            return Color.FromArgb(startColor.R + stepR * step, startColor.G + stepG * step, startColor.B + stepB * step);
        }

        /// <summary>
        /// Метод для получения картики с отрисованный фракталом.
        /// </summary>
        /// <returns>Картинка с фракталом.</returns>
        public virtual Bitmap GetBitmap()
        {
            switch (rotation)
            {
                case 1:
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 2:
                    bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 3:
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
            }

            // Копируем, иначе говорим пока инкапсуляции.
            return (Bitmap)bmp.Clone();
        }

        /// <summary>
        /// Отрисовка фраткала. Не возвращает результат.
        /// </summary>
        public virtual void DrawFractal()
        {

                bmp = new Bitmap(width, height);
                var g = Graphics.FromImage(bmp);
                g.Clear(backgroundColor);

        }



    }
}
