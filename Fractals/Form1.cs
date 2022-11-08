using System;
using System.Drawing;
using System.Windows.Forms;

namespace FractalsPeer
{
    public partial class Form1 : Form
    {
        // Текущее изображение.
        Bitmap bmp;
        // Коэффициент приближения.
        private int zoom = 1;
        // Поворот картики. Угол = 90 * rotation.
        private int rotation = 0;
        // Цвет фона, начальный и конечный цвета градиентной отрисовки.
        Color bgColor, startColor, endColor;

        #region Инициализация.
        public Form1()
        {
            InitializeComponent();


            // Установка цветов.
            backgroundColorDialog.FullOpen = true;
            startColorDialog.FullOpen = true;
            endColorDialog.FullOpen = true;

            SetStartEndColors(Settings.beginColor, Settings.endColor);

            backgroundColorDialog.Color = Settings.bgColor;
            mainPicture.BackColor = Settings.bgColor;
            bgColor = bgColorPB.BackColor;

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            new ToolTip().SetToolTip(catPB5, Settings.sectretMessange);
            new ToolTip().SetToolTip(catPB4, Settings.sectretMessange);
            new ToolTip().SetToolTip(catPB3, Settings.sectretMessange);
            new ToolTip().SetToolTip(catPB2, Settings.sectretMessange);

            SetImageSize();
        }

        // При зуме или растягивании формы необходимо обновить размер изображения и положение ползунков.
        private void SetImageSize()
        {
            double vertical = picturePanel.VerticalScroll.Value / (double)picturePanel.VerticalScroll.Maximum;
            double horizontal = picturePanel.HorizontalScroll.Value / (double)picturePanel.HorizontalScroll.Maximum;

            var size = picturePanel.Size;
            size.Width *= zoom;
            size.Height *= zoom;
            mainPicture.Size = size;


            if (vertical == 0 && horizontal == 0)
            {
                vertical = 0.5;
                horizontal = 0.4;
            }

            picturePanel.VerticalScroll.Value = (int)(vertical * picturePanel.VerticalScroll.Maximum);

            picturePanel.HorizontalScroll.Value = (int)(horizontal * picturePanel.HorizontalScroll.Maximum);
            picturePanel.HorizontalScroll.Value = (int)(horizontal * picturePanel.HorizontalScroll.Maximum);
            timer1.Stop();
            timer1.Start();
        }

        #endregion

        #region Отрисовка фракталов.


        /// <summary>
        /// Метод для отрисовки картинки на форму.
        /// </summary>
        private Fractal GetCarpet()
        {
            return new Carpet(maxDepth: (int)carpetNumericUpDown.Value,
                                 width: mainPicture.Width,
                                 height: mainPicture.Height,
                                 bgColor: bgColor,
                                 startColor: startColor,
                                 endColor: gradientCheckBox.Checked ? endColor : startColor,
                                 gradient: gradientCheckBox.Checked,
                                 thickness: (int)thicknessNumericUpDown.Value,
                                 rotation: 0);
        }

        private Fractal GetTree()
        {
            return new Tree(depth: (int)treeDepthNumericUpDown.Value,
                                 width: mainPicture.Width,
                                 height: mainPicture.Height,
                                 backgroundColor: bgColor,
                                 startColor: startColor,
                                 endColor: gradientCheckBox.Checked ? endColor : startColor,
                                 gradient: gradientCheckBox.Checked,
                                 thickness: (int)thicknessNumericUpDown.Value,
                                 rotation: rotation,
                                 leftAngle: (2 * MathF.PI) * (-0.25f + 1 - treeLeftTrackBar.Value / (float)treeLeftTrackBar.Maximum),
                                 rightAngle: (2 * MathF.PI) * (-0.25f + 1 - treeRightTrackBar.Value / (float)treeRightTrackBar.Maximum),
                                 alpha: (float)treeAlphaNumericUpDown.Value);
        }


        private Fractal GetKoh()
        {
            return new KohCurve(maxDepth: (int)KohDepthNumericUpDown.Value,
                                    width: mainPicture.Width,
                                    height: mainPicture.Height,
                                    backgroundColor: bgColor,
                                    startColor: startColor,
                                    endColor: gradientCheckBox.Checked ? endColor : startColor,
                                    rotation: rotation,
                                    gradient: gradientCheckBox.Checked,
                                    thickness: (int)thicknessNumericUpDown.Value);
        }


        private Fractal GetTriangle()
        {
            return new Triangle(maxDepth: (int)triangleUpDown2.Value,
                                     width: mainPicture.Width,
                                     height: mainPicture.Height,
                                     bgColor: bgColor,
                                     startColor: startColor,
                                     endColor: gradientCheckBox.Checked ? endColor : startColor,
                                     gradient: gradientCheckBox.Checked,
                                     rotation: rotation,
                                     thickness: (int)thicknessNumericUpDown.Value);
        }


        private Fractal GetKantor()
        {
            return new KantorSet(maxDepth: (int)KantorSetNumericUpDown.Value,
                                     width: mainPicture.Width,
                                     height: mainPicture.Height,
                                     bgColor: bgColor,
                                     startColor: startColor,
                                     endColor: gradientCheckBox.Checked ? endColor : startColor,
                                     gradient: gradientCheckBox.Checked,
                                     thickness: (int)thicknessNumericUpDown.Value,
                                     rotation: (2 + rotation) % 4,
                                     shift: shiftTrackBar.Value);
        }


        /// <summary>
        /// Метод для установки начального и конечного цветов рекурсии.
        /// </summary>
        /// <param name="startColor">Новый начальный цвет.</param>
        /// <param name="endColor">Новый конечный цвет.</param>
        private void SetStartEndColors(Color startColor, Color endColor)
        {

            this.startColor = startColor;

            this.endColor = endColor;

            startColorDialog.Color = startColor;
            endColorDialog.Color = endColor;

            startColorPB.BackColor = startColor;
            endColorPB.BackColor = endColor;

        }

        /// <summary>
        /// Отрисовка фраткла в форму.
        /// </summary>
        private void PrintFractal()
        {
            // Если свернули окно.
            if (mainPicture.Width < 1 || mainPicture.Height < 1)
            {
                return;
            }
            Fractal fractal = new Fractal();


            if (fractalTabs.SelectedTab == carpetPage)
            {
                // При просмотре фракталов с стандартными настройками цветов, для ковра с градиентным
                // отображением будет установлена красивая палитра.
                if (gradientCheckBox.Checked && startColor == Settings.beginColor && endColor == Settings.endColor)
                {
                    SetStartEndColors(Settings.carpetStartColor, Settings.carpetEndColor);
                }

                fractal = GetCarpet();
            }

            else
            {
                if (startColor == Settings.carpetStartColor && endColor == Settings.carpetEndColor)
                {
                    SetStartEndColors(Settings.beginColor, Settings.endColor);
                }
            }


            // При громадном желании написать switch expression, переменные, отвечающие за название страницами,
            // не могут быть варианитами switch.

            if (fractalTabs.SelectedTab == treePage)
            {
                fractal = GetTree();
            }

            if (fractalTabs.SelectedTab == kohPage)
            {
                fractal = GetKoh();
            }

            if (fractalTabs.SelectedTab == trianglePage)
            {
                fractal = GetTriangle();
            }

            if (fractalTabs.SelectedTab == setPage)
            {
                fractal = GetKantor();
            }


            // Рисуем и переворчиваем, потому что рисовали вверх ногами.
            fractal.DrawFractal();
            bmp = fractal.GetBitmap();
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            mainPicture.Image = bmp;
        }


        // Например, при растягивании изображения нам необходимо его часто отрисоывать, если
        // делать это в лоб, это приведет к знатным тормозам.
        // Поэтому при необходимости перерисоывть фрактал, запускается таймер.
        // За время тика ничего не произошло? Рисуйте фрактал.
        // Если пришел новый запрос на перерисовку, то обнуляем таймер.
        // Еще было бы прекрасно прикрутить сюда BackgroundWorker'a, чтобы форма совсем не висла при 
        // отрисовке.
        private void timer1_Tick(object sender, EventArgs e)
        {
            PrintFractal();
            timer1.Stop();
        }

        // Событие - запрос на отрисовку текущего изображения.
        private void RedrawMainPicture(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
        }

        #endregion

        #region События с элементов.


        #region Отрисовка.

        // Изменение масштаба.
        private void zoomTrackBar_Scroll(object sender, EventArgs e)
        {
            zoomLabel.Text = $"x{zoomTrackBar.Value}";
            zoom = zoomTrackBar.Value;


            SetImageSize();
        }


        // Изменение начального цвета градиентной отрисовки.
        private void changeStartColButton_Click(object sender, EventArgs e)
        {
            if (startColorDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            startColor = startColorDialog.Color;
            startColorPB.BackColor = startColorDialog.Color;
            timer1.Stop();
            timer1.Start();
        }

        // Изменение конечного цвета градиентной отрисовки.
        private void changeEndColButton_Click(object sender, EventArgs e)
        {
            if (endColorDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            endColorPB.BackColor = endColorDialog.Color;
            endColor = endColorDialog.Color;
            timer1.Stop();
            timer1.Start();
        }

        // Изменение цвета фона.
        private void changeBGColorButton_Click(object sender, EventArgs e)
        {
            if (backgroundColorDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            bgColor = backgroundColorDialog.Color;
            bgColorPB.BackColor = backgroundColorDialog.Color;
            mainPicture.BackColor = backgroundColorDialog.Color;
            timer1.Stop();
            timer1.Start();

        }

        // Изменение в необходимости градиентной отрисовки.
        private void gradientCheckBoxChanged(object sender, EventArgs e)
        {
            if (gradientCheckBox.Checked)
            {
                endColorLabel.ForeColor = SystemColors.ControlText;
                changeEndColButton.Enabled = true;
            }

            else
            {
                endColorLabel.ForeColor = SystemColors.ButtonShadow;
                changeEndColButton.Enabled = false;
            }

            timer1.Stop();
            timer1.Start();
        }
        #endregion

        private void fractalTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            zoomTrackBar.Value = 1;
            zoomTrackBar_Scroll(sender, e);
            timer1.Stop();
            timer1.Start();
            rotation = 0;


            // Загадка: почему на множестве Кантора заблокирована кнопка поворота картики ?
            // На размышление дается 30 секунд.

            if (fractalTabs.SelectedTab == setPage)
            {
                rotateButton.Enabled = false;
            }

            else
            {
                rotateButton.Enabled = true;
            }

            if (fractalTabs.SelectedTab == carpetPage)
            {
                gradientCheckBox.Checked = false;

            }
            else
            {
                gradientCheckBox.Checked = true;
            }
        }


        // Поворот картинки.
        private void rotateButton_Click(object sender, EventArgs e)
        {
            rotation++;
            rotation %= 4;
            timer1.Stop();
            timer1.Start();
        }



        // Растягивание формы.
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            SetImageSize();
        }


        // Сохранение картики.
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (mainPicture.Image != null) //если в pictureBox есть изображение
            {
                //создание диалогового окна "Сохранить как..", для сохранения изображения
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Сохранить картинку как...";
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        mainPicture.Image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

    }
}