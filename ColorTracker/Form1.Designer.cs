using System;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace WoWMinimapColorScanner
{
    public class ScannerForm : Form
    {
        private System.Windows.Forms.Timer scanTimer;
        private Color targetColor = Color.FromArgb(228, 178, 9);
        private bool isDragging = false;
        private Point clickOffset;

        public ScannerForm()
        {
            this.Text = "WoW Minimap Scanner";
            this.Size = new Size(300, 200);

            this.BackColor = Color.Magenta;
            this.TransparencyKey = Color.Magenta;

            Label info = new Label
            {
                Text = "Scanning for node color in minimap...",
                ForeColor = Color.Lime,
                BackColor = Color.Black,
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(info);

            scanTimer = new System.Windows.Forms.Timer();
            scanTimer.Interval = 1000; // every second
            scanTimer.Tick += ScanForColor;
            scanTimer.Start();
        }

        private void ScanForColor(object sender, EventArgs e)
        {
            Rectangle scanArea = new Rectangle(this.Location, this.Size);

            using (Bitmap bmp = new Bitmap(scanArea.Width, scanArea.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(scanArea.Location, Point.Empty, scanArea.Size);
                }

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        Color currentPixel = bmp.GetPixel(x, y);
                        if (IsColorMatch(currentPixel, targetColor, 10)) // Tolerance of 10
                        {
                            Debug.WriteLine($"Krappa at ({x},{y}): R{currentPixel.R} G{currentPixel.G} B{currentPixel.B}");

                            //Input your path to your Sounds folder (The default sound is quite... violent, so I would suggest you to change xD
                            SoundPlayer player = new SoundPlayer(@"C:\Users\Tobias\source\repos\ColorTracker\ColorTracker\Sounds\Ding.wav");
                            player.Play();
                            return;
                        }
                    }
                }
            }
        }

        private bool IsColorMatch(Color c1, Color c2, int tolerance)
        {
            int rDiff = Math.Abs(c1.R - c2.R);
            int gDiff = Math.Abs(c1.G - c2.G);
            int bDiff = Math.Abs(c1.B - c2.B);

            return rDiff <= tolerance && gDiff <= tolerance && bDiff <= tolerance;
        }
    }
}
