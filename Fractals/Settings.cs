using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace FractalsPeer
{
    static public class Settings
    {

        static public string t = Environment.NewLine;
        static public string sectretMessange = "Для дерева Пифагора я сделал достаточно большой макет для" + t +
                "всяких кнопок и настроек, однако оказалось, что для других это излишне." + t +
                "Поэтому было решено занять свободное " + t +
                "пространство фрактальным котэком. " + t + t +
                "Кажется, он привлекает слишком много внимания... ";

        static public Color bgColor = Color.Black;
        static public Color beginColor = Color.FromArgb(255, 0, 128);
        static public Color endColor = Color.FromArgb(0, 128, 255);

        static public Color carpetStartColor = Color.FromArgb(0, 64, 128);
        static public Color carpetEndColor = Color.Black;
        static public class TreeSetting
        {
            static public float alpha = 0.7f;
            static public int depth = 13;
            static public int minDepth = 1;
            static public int maxDepth = 18;
            static public float minAlpha = 0.2f;
            static public float maxAlpha = 0.9f;
        }

        static public class KohCurveSettings
        {
            static public int minDepth = 2;
            static public int maxDepth = 10;
        }
    }
}




// Банан большой, а кожура больше.