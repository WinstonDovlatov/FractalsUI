using System;
using System.Drawing;


namespace FractalsPeer
{
    internal class Tree : Fractal
    {
        public float leftAngle, rightAngle;
        public float alpha;

        public Tree()
        {
            maxDepth = Settings.TreeSetting.depth;
            leftAngle = 3 * MathF.PI / 4;
            rightAngle = MathF.PI / 4;
            alpha = Settings.TreeSetting.alpha;
        }

        /// <summary>
        /// Основной конструктор класса.
        /// </summary>
        /// <param name="depth">Максмальная глубина рекурсии.</param>
        /// <param name="width">Ширина картики.</param>
        /// <param name="height">Высота картинки.</param>
        /// <param name="backgroundColor">Цвет фона.</param>
        /// <param name="startColor">Начальный цвет градиентой отрисовки.</param>
        /// <param name="endColor">Конечный цвет.</param>
        /// <param name="gradient">Нужна ли градиентая отрисовка.</param>
        /// <param name="thickness">Толщина линий.</param>
        /// <param name="rotation">Поворот картики.</param>
        /// <param name="leftAngle">Угол поворота левой ветки.</param>
        /// <param name="rightAngle">Угол поворота правой ветки.</param>
        /// <param name="alpha">Коэффициент - отношение длин отрезков на текщей и прошлой итерации.</param>
        public Tree(int depth, int width, int height,
            Color backgroundColor, Color startColor, Color endColor, bool gradient, int thickness, int rotation,
            float leftAngle, float rightAngle, float alpha) :
            base(depth, width, height, backgroundColor, startColor, endColor, gradient, thickness, rotation)
        {
            this.leftAngle = leftAngle;
            this.rightAngle = rightAngle;
            this.alpha = alpha;
        }

        /// <summary>
        /// Метод для рекурсивной отрисовки фрактала.
        /// </summary>
        /// <param name="g">Куда рисуем.</param>
        /// <param name="p1">Точка начала отрезка.</param>
        /// <param name="p2">Точка конца отрезка</param>
        /// <param name="k">Отношение для отрезков на текущей и прошлой итерациях.</param>
        /// <param name="alpha1">Поворт левой ветки.</param>
        /// <param name="alpha2">Поворот правой ветки.</param>
        /// <param name="lvl">Глубина рекурсии.</param>
        /// <param name="rotate">Накопленный поворот.</param>
        private void DrawTree(Graphics g, PointF p1, PointF p2, float k, float alpha1, float alpha2,
            int lvl, float rotate)
        {
            PointF p3_1, p3_2;

            var p = new Pen(GetColor(lvl), thickness);
            g.DrawLine(p, p1, p2);
            if (lvl == maxDepth)
            {
                return;
            }
            // Длина текущего отрезка.
            float len = (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));

            // Поворот относительно ОХ. 
            p3_1 = new PointF(p2.X + k * len * MathF.Cos(rotate + alpha1),
                              p2.Y + k * len * MathF.Sin(rotate + alpha1));

            p3_2 = new PointF(p2.X + k * len * MathF.Cos(rotate + alpha2),
                              p2.Y + k * len * MathF.Sin(rotate + alpha2));


            DrawTree(g, p2, p3_1, k, alpha1, alpha2, lvl + 1, rotate - MathF.PI / 2 + alpha1);
            DrawTree(g, p2, p3_2, k, alpha1, alpha2, lvl + 1, rotate - MathF.PI / 2 + alpha2);


        }

        public override void DrawFractal()
        {
            base.DrawFractal();
            using (var g = Graphics.FromImage(bmp))
            {
                PointF p1, p2;
                int n = width / 2;
                int m = height / 2;

                p1 = new PointF(n, MathF.Min(m, n) / 1.8f);
                p2 = new PointF(n, p1.Y + (MathF.Min(m, n) * (1f - alpha) * (1f + alpha / 1.2f)) * alpha * 0.95f);


                // Рекурсионная отрисовка фрактала.
                DrawTree(g, p1, p2, alpha, leftAngle, rightAngle,
                    lvl: 1, rotate: 0);

            }
        }
    }
}
