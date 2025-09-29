#region Using directives

using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

#endregion

namespace Entity
{
    /// <summary>
    /// Summary description for HeadLabel.
    /// </summary>
    public class HeadLabel : Control
    {
        public HeadLabel()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Font != null)
            {
                Graphics g = e.Graphics;
                SizeF sizef = g.MeasureString(Text, Font);
                Size size = new Size((int)sizef.Width + 1, (int)sizef.Height + 1);

                g.FillRegion(new SolidBrush(Parent.BackColor), g.Clip);

                int r = Height / 3;
                Point[] pr = new Point[r];
                int x = 0, y = r, p = 3 - 2 * r;
                int i = 0;
                while (x < y)
                {
                    pr[i++] = new Point(x, y);
                    if (p < 0)
                    {
                        p += (4 * x + 6);
                    }
                    else
                    {
                        p += (4 * (x - y) + 10);
                        y--;
                    }
                    x++;
                }
                Point[] ps = new Point[2 * i + 3];
                for (int j = 0; j < i; j++)
                {
                    ps[j] = new Point(pr[j].X + r + size.Width, r - pr[j].Y);
                    ps[2 * i - j - 1] = new Point(pr[j].Y + r + size.Width, r - pr[j].X);
                }
                ps[2 * i] = new Point(size.Width + r + r, Height);
                ps[2 * i + 1] = new Point(0, Height);
                ps[2 * i + 2] = new Point(0, 0);
                g.FillPolygon(new SolidBrush(BackColor), ps);
                g.FillRegion(new SolidBrush(BackColor),
                    new Region(new Rectangle(0, Height - 3, Width, 3)));

                g.DrawString(Text, Font, new SolidBrush(ForeColor),
                    new RectangleF(r, 0, size.Width, size.Height));

                g.Dispose();
            }

            //base.OnPaint(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate();
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}
