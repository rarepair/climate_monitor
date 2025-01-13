using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ClimateMonitorCharDesigner
{
    public partial class CharDesigner : Form
    {
        private static int PIXELWIDTH = 6;
        private static int BITSPERBYTE = 8;
        private static int LCDMAXROWS = 96;
        private static int LCDMAXCOLS = 96;
        private static int GRIDPENWIDTH = 1;
        private int LCDRows;
        private int LCDCols;
        private bool[,] pixels;
        
        public CharDesigner()
        {
            InitializeComponent();
            try
            {
                LCDRows = Convert.ToInt32(rowBox.Text);
                LCDCols = Convert.ToInt32(colBox.Text);
            }
            catch
            {
                statusBarLabel.Text = "impossible character size, defaulting to 96x96 pixels";
                LCDRows = LCDMAXROWS;
                LCDCols = LCDMAXCOLS;
                rowBox.Text = Convert.ToString(LCDMAXROWS);
                colBox.Text = Convert.ToString(LCDMAXCOLS);
            }
            pixels = new bool[LCDRows,LCDCols];
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = false;
                }
            }
            Extensions.TextBoxExtension.SetTabStopWidth(byteBox, 3);
            shiftLeftTimer.Enabled = true;
            shiftRightTimer.Enabled = true;
            shiftUpTimer.Enabled = true;
            shiftDownTimer.Enabled = true;
            shiftLeftTimer.Stop();
            shiftRightTimer.Stop();
            shiftUpTimer.Stop();
            shiftDownTimer.Stop();
        }

        private void pixelBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                setPixelByMouse(e.X, e.Y, true);
            }
            if (e.Button == MouseButtons.Right)
            {
                setPixelByMouse(e.X, e.Y, false);
            }
        }

        private void pixelBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                setPixelByMouse(e.X, e.Y, true);
            }
            if (e.Button == MouseButtons.Right)
            {
                setPixelByMouse(e.X, e.Y, false);
            }
        }

        private void setPixelByMouse(int mouseX, int mouseY, bool pixelState)
        {
            int pixelX = mouseX / PIXELWIDTH;
            int pixelY = mouseY / PIXELWIDTH;

            if (pixelX >= 0 && pixelY >= 0 && pixelX < LCDCols && pixelY < LCDRows)
            {
                if (pixelState)
                {
                    pixels[pixelY, pixelX] = true;
                }
                else
                {
                    pixels[pixelY, pixelX] = false;
                }
                pixelBox.Refresh();
                byteBox.Clear();
            }
        }

        private void pixelBox_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush greyBrush = new SolidBrush(Color.LightGray);
            Pen borderPen = new Pen(new SolidBrush(Color.DarkGray));
            borderPen.Width = GRIDPENWIDTH;
            
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    if (pixels[row,col])
                    {
                        e.Graphics.FillRectangle(blackBrush, col * PIXELWIDTH, row * PIXELWIDTH, PIXELWIDTH, PIXELWIDTH);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(greyBrush, col * PIXELWIDTH, row * PIXELWIDTH, PIXELWIDTH, PIXELWIDTH);
                    }
                }
            }

            for (int vertGridLine = 0; vertGridLine <= LCDCols; vertGridLine++)
            {
                e.Graphics.DrawLine(borderPen, vertGridLine * PIXELWIDTH, 0, vertGridLine * PIXELWIDTH, LCDRows * PIXELWIDTH);
            }
            for (int horzGridLine = 0; horzGridLine <= LCDRows; horzGridLine++)
            {
                e.Graphics.DrawLine(borderPen, 0, horzGridLine * PIXELWIDTH, LCDCols * PIXELWIDTH, horzGridLine * PIXELWIDTH);
            }
        }

        private void clearBytesButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            statusBarLabel.Text = "erased byte array";
        }

        private void getArrayButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            int numColBytes = (LCDCols / BITSPERBYTE) + (LCDCols % BITSPERBYTE != 0 ? 1 : 0);
            byte[][] bytes = new byte[LCDRows][];
            for (int line = 0; line < LCDRows; line++)
            {
                bytes[line] = new byte[numColBytes];
            }
            
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < (numColBytes * BITSPERBYTE); col += BITSPERBYTE)
                {
                    byte buildByte = 0x00;
                    for (int bit = 0; bit < BITSPERBYTE; bit++)
                    {
                        if (col + bit < LCDCols)
                        {
                            if (pixels[row, col + bit])
                            {
                                buildByte = (byte)(buildByte | (0x80 >> bit));
                            }
                        }
                    }
                    buildByte = (byte)~buildByte;
                    bytes[row][col / BITSPERBYTE] = buildByte;
                }
            }
            byteBox.Text += String.Format("char data[{0}] = {{\r\n", LCDRows * numColBytes);
            int lineCount = 0;
            foreach (byte[] line in bytes)
            {
                if (lineCount < (LCDRows - 1))
                {
                    byteBox.Text += Extensions.TextBoxExtension.toHex(bytes[lineCount]) + "\r\n";
                }
                else
                {
                    byteBox.Text += Extensions.TextBoxExtension.toHex(bytes[lineCount]) + " };";
                    byteBox.Text = byteBox.Text.Remove(byteBox.Text.Length - 4, 1);
                }
                lineCount++;
            }
            byteBox.SelectAll();
            byteBox.Copy();
            statusBarLabel.Text = "byte array generated and copied to clipboard";
        }

        private void flipHorizontalButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            bool[,] newPixels = new bool[LCDRows, LCDCols];
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    newPixels[row, col] = pixels[LCDRows - 1 - row, col];
                }
            }
            pixels = newPixels;
            pixelBox.Refresh();
            statusBarLabel.Text = "flipped horizontally";
        }

        private void flipVerticalButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            bool[,] newPixels = new bool[LCDRows, LCDCols];
            for (int col = 0; col < LCDCols; col++)
            {
                for (int row = 0; row < LCDRows; row++)
                {
                    newPixels[row, col] = pixels[row, LCDCols - 1 - col];
                }
            }
            pixels = newPixels;
            pixelBox.Refresh();
            statusBarLabel.Text = "flipped vertically";
        }

        private void rowBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            int newLCDRows = 0;
            try
            {
                newLCDRows = Convert.ToInt32(rowBox.Text);
            }
            catch (Exception exception)
            {
                statusBarLabel.Text = "impossible row size, defaulting to 96 pixels; exception caught: " + exception.ToString();
                newLCDRows = 96;
                rowBox.Text = "96";
                return;
            }
            if (newLCDRows < 1 || newLCDRows > LCDMAXROWS)
            {
                statusBarLabel.Text = "impossible row size, defaulting to 96 pixels";
                newLCDRows = 96;
                rowBox.Text = "96";
            }

            bool[,] newPixels = new bool[newLCDRows, LCDCols];
            for (int row = 0; row < newLCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    if (row < newLCDRows && row < LCDRows)
                    {
                        newPixels[row, col] = pixels[row, col];
                    }
                    else
                    {
                        newPixels[row, col] = false;
                    }
                }
            }
            LCDRows = newLCDRows;
            pixels = newPixels;
            byteBox.Clear();
            pixelBox.Refresh();
        }

        private void colBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            int newLCDCols = 0;
            try
            {
                newLCDCols = Convert.ToInt32(colBox.Text);
            }
            catch (Exception exception)
            {
                statusBarLabel.Text = "impossible column size, defaulting to 96 pixels; exception caught: " + exception.ToString();
                newLCDCols = 96;
                rowBox.Text = "96";
                return;
            }
            if (newLCDCols < 1 || newLCDCols > LCDMAXCOLS)
            {
                statusBarLabel.Text = "impossible column size, defaulting to 96 pixels";
                newLCDCols = 96;
                rowBox.Text = "96";
            }

            bool[,] newPixels = new bool[LCDRows, newLCDCols];
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < newLCDCols; col++)
                {
                    if (col < newLCDCols && col < LCDCols)
                    {
                        newPixels[row, col] = pixels[row, col];
                    }
                    else
                    {
                        newPixels[row, col] = false;
                    }
                }
            }
            LCDCols = newLCDCols;
            pixels = newPixels;
            byteBox.Clear();
            pixelBox.Refresh();
        }

        private void shiftRightButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            for (int row = 0; row < LCDRows; row++)
            {
                bool lastLed = pixels[row, LCDCols - 1];
                for (int col = LCDCols - 1; col > 0; col--)
                {
                    pixels[row, col] = pixels[row, col - 1];
                }
                if (wrapAroundCheckBox.Checked)
                { pixels[row, 0] = lastLed; }
                else
                { pixels[row, 0] = false; }
            }
            pixelBox.Refresh();
            this.Focus();
            statusBarLabel.Text = "shifted right";
        }

        private void shiftLeftButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            for (int row = 0; row < LCDRows; row++)
            {
                bool firstPixel = pixels[row, 0];
                for (int col = 0; col < LCDCols - 1; col++)
                {
                    pixels[row, col] = pixels[row, col + 1];
                }
                if (wrapAroundCheckBox.Checked)
                { pixels[row, LCDCols - 1] = firstPixel; }
                else
                { pixels[row, LCDCols - 1] = false; }
            }
            pixelBox.Refresh();
            this.Focus();
            statusBarLabel.Text = "shifted left";
        }

        private void shiftUpButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            bool[] firstRow = new bool[LCDCols];
            for (int col = 0; col < LCDCols; col++)
            {
                firstRow[col] = pixels[0, col];
            }

            for (int row = 1; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row - 1, col] = pixels[row, col];
                }
            }

            if (wrapAroundCheckBox.Checked)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[LCDRows - 1, col] = firstRow[col];
                }
            }
            else
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[LCDRows - 1, col] = false;
                }
            }
            pixelBox.Refresh();
            statusBarLabel.Text = "shifted up";
        }

        private void shiftDownButton_Click(object sender, EventArgs e)
        {
            byteBox.Clear();
            bool[] lastRow = new bool[LCDCols];
            for (int col = 0; col < LCDCols; col++)
            {
                lastRow[col] = pixels[LCDRows - 1, col];
            }

            for (int row = LCDRows - 1; row > 0; row--)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = pixels[row - 1, col];
                }
            }

            if (wrapAroundCheckBox.Checked)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[0, col] = lastRow[col];
                }
            }
            else
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[0, col] = false;
                }
            }
            pixelBox.Refresh();
            statusBarLabel.Text = "shifted down";
        }

        private void shiftRightButton_MouseDown(object sender, MouseEventArgs e)
        {
            shiftRightTimer.Start();
        }

        private void shiftRightButton_MouseUp(object sender, MouseEventArgs e)
        {
            shiftRightTimer.Stop();
            statusBarLabel.Text = "shifted right";
        }

        private void shiftLeftButton_MouseDown(object sender, MouseEventArgs e)
        {
            shiftLeftTimer.Start();
        }

        private void shiftLeftButton_MouseUp(object sender, MouseEventArgs e)
        {
            shiftLeftTimer.Stop();
            statusBarLabel.Text = "shifted left";
        }

        private void shiftUpButton_MouseDown(object sender, MouseEventArgs e)
        {
            shiftUpTimer.Start();
        }

        private void shiftUpButton_MouseUp(object sender, MouseEventArgs e)
        {
            shiftUpTimer.Stop();
            statusBarLabel.Text = "shifted up";
        }

        private void shiftDownButton_MouseDown(object sender, MouseEventArgs e)
        {
            shiftDownTimer.Start();
        }

        private void shiftDownButton_MouseUp(object sender, MouseEventArgs e)
        {
            shiftDownTimer.Stop();
            statusBarLabel.Text = "shifted down";
        }

        private void shiftRightTimer_Tick(object sender, EventArgs e)
        {
            for (int row = 0; row < LCDRows; row++)
            {
                bool lastLed = pixels[row, LCDCols - 1];
                for (int col = LCDCols - 1; col > 0; col--)
                {
                    pixels[row, col] = pixels[row, col - 1];
                }
                if (wrapAroundCheckBox.Checked)
                { pixels[row, 0] = lastLed; }
                else
                { pixels[row, 0] = false; }
            }
            pixelBox.Refresh();
        }

        private void shiftLeftTimer_Tick(object sender, EventArgs e)
        {
            for (int row = 0; row < LCDRows; row++)
            {
                bool firstPixel = pixels[row, 0];
                for (int col = 0; col < LCDCols - 1; col++)
                {
                    pixels[row, col] = pixels[row, col + 1];
                }
                if (wrapAroundCheckBox.Checked)
                { pixels[row, LCDCols - 1] = firstPixel; }
                else
                { pixels[row, LCDCols - 1] = false; }
            }
            pixelBox.Refresh();
        }

        private void shiftUpTimer_Tick(object sender, EventArgs e)
        {
            bool[] firstRow = new bool[LCDCols];
            for (int col = 0; col < LCDCols; col++)
            {
                firstRow[col] = pixels[0, col];
            }

            for (int row = 1; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row - 1, col] = pixels[row, col];
                }
            }

            if (wrapAroundCheckBox.Checked)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[LCDRows - 1, col] = firstRow[col];
                }
            }
            else
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[LCDRows - 1, col] = false;
                }
            }
            pixelBox.Refresh();
        }

        private void shiftDownTimer_Tick(object sender, EventArgs e)
        {
            bool[] lastRow = new bool[LCDCols];
            for (int col = 0; col < LCDCols; col++)
            {
                lastRow[col] = pixels[LCDRows - 1, col];
            }

            for (int row = LCDRows - 1; row > 0; row--)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = pixels[row - 1, col];
                }
            }

            if (wrapAroundCheckBox.Checked)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[0, col] = lastRow[col];
                }
            }
            else
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[0, col] = false;
                }
            }
            pixelBox.Refresh();
        }

        private void fillLCDButton_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = true;
                }
            }
            pixelBox.Refresh();
            byteBox.Clear();
            statusBarLabel.Text = "filled LCD";
        }

        private void invertLCDButton_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = !pixels[row,col];
                }
            }
            pixelBox.Refresh();
            byteBox.Clear();
            statusBarLabel.Text = "inverted LCD";
        }

        private void clearLCDButton_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < LCDRows; row++)
            {
                for (int col = 0; col < LCDCols; col++)
                {
                    pixels[row, col] = false;
                }
            }
            pixelBox.Refresh();
            byteBox.Clear();
            statusBarLabel.Text = "cleared LCD";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    shiftLeftButton.PerformClick();
                    return true;
                case Keys.Right:
                    shiftRightButton.PerformClick();
                    return true;
                case Keys.Up:
                    shiftUpButton.PerformClick();
                    return true;
                case Keys.Down:
                    shiftDownButton.PerformClick();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void transparencySlider_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = Convert.ToDouble(transparencySlider.Value) / 10d;
        }
    }
}

namespace Extensions
{
    public static class TextBoxExtension
    {
        private const int EM_SETTABSTOPS = 0x00CB;
        private static readonly string[] HexStringTable = new string[]
        {
            "0x00,", "0x01,", "0x02,", "0x03,", "0x04,", "0x05,", "0x06,", "0x07,", "0x08,", "0x09,", "0x0A,", "0x0B,", "0x0C,", "0x0D,", "0x0E,", "0x0F,",
            "0x10,", "0x11,", "0x12,", "0x13,", "0x14,", "0x15,", "0x16,", "0x17,", "0x18,", "0x19,", "0x1A,", "0x1B,", "0x1C,", "0x1D,", "0x1E,", "0x1F,",
            "0x20,", "0x21,", "0x22,", "0x23,", "0x24,", "0x25,", "0x26,", "0x27,", "0x28,", "0x29,", "0x2A,", "0x2B,", "0x2C,", "0x2D,", "0x2E,", "0x2F,",
            "0x30,", "0x31,", "0x32,", "0x33,", "0x34,", "0x35,", "0x36,", "0x37,", "0x38,", "0x39,", "0x3A,", "0x3B,", "0x3C,", "0x3D,", "0x3E,", "0x3F,",
            "0x40,", "0x41,", "0x42,", "0x43,", "0x44,", "0x45,", "0x46,", "0x47,", "0x48,", "0x49,", "0x4A,", "0x4B,", "0x4C,", "0x4D,", "0x4E,", "0x4F,",
            "0x50,", "0x51,", "0x52,", "0x53,", "0x54,", "0x55,", "0x56,", "0x57,", "0x58,", "0x59,", "0x5A,", "0x5B,", "0x5C,", "0x5D,", "0x5E,", "0x5F,",
            "0x60,", "0x61,", "0x62,", "0x63,", "0x64,", "0x65,", "0x66,", "0x67,", "0x68,", "0x69,", "0x6A,", "0x6B,", "0x6C,", "0x6D,", "0x6E,", "0x6F,",
            "0x70,", "0x71,", "0x72,", "0x73,", "0x74,", "0x75,", "0x76,", "0x77,", "0x78,", "0x79,", "0x7A,", "0x7B,", "0x7C,", "0x7D,", "0x7E,", "0x7F,",
            "0x80,", "0x81,", "0x82,", "0x83,", "0x84,", "0x85,", "0x86,", "0x87,", "0x88,", "0x89,", "0x8A,", "0x8B,", "0x8C,", "0x8D,", "0x8E,", "0x8F,",
            "0x90,", "0x91,", "0x92,", "0x93,", "0x94,", "0x95,", "0x96,", "0x97,", "0x98,", "0x99,", "0x9A,", "0x9B,", "0x9C,", "0x9D,", "0x9E,", "0x9F,",
            "0xA0,", "0xA1,", "0xA2,", "0xA3,", "0xA4,", "0xA5,", "0xA6,", "0xA7,", "0xA8,", "0xA9,", "0xAA,", "0xAB,", "0xAC,", "0xAD,", "0xAE,", "0xAF,",
            "0xB0,", "0xB1,", "0xB2,", "0xB3,", "0xB4,", "0xB5,", "0xB6,", "0xB7,", "0xB8,", "0xB9,", "0xBA,", "0xBB,", "0xBC,", "0xBD,", "0xBE,", "0xBF,",
            "0xC0,", "0xC1,", "0xC2,", "0xC3,", "0xC4,", "0xC5,", "0xC6,", "0xC7,", "0xC8,", "0xC9,", "0xCA,", "0xCB,", "0xCC,", "0xCD,", "0xCE,", "0xCF,",
            "0xD0,", "0xD1,", "0xD2,", "0xD3,", "0xD4,", "0xD5,", "0xD6,", "0xD7,", "0xD8,", "0xD9,", "0xDA,", "0xDB,", "0xDC,", "0xDD,", "0xDE,", "0xDF,",
            "0xE0,", "0xE1,", "0xE2,", "0xE3,", "0xE4,", "0xE5,", "0xE6,", "0xE7,", "0xE8,", "0xE9,", "0xEA,", "0xEB,", "0xEC,", "0xED,", "0xEE,", "0xEF,",
            "0xF0,", "0xF1,", "0xF2,", "0xF3,", "0xF4,", "0xF5,", "0xF6,", "0xF7,", "0xF8,", "0xF9,", "0xFA,", "0xFB,", "0xFC,", "0xFD,", "0xFE,", "0xFF,"
        };

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr h, int msg, int wParam, int[] lParam);

        public static Point GetCaretPosition(this TextBox textBox)
        {
            Point point = new Point(0, 0);

            if (textBox.Focused)
            {
                point.X = textBox.SelectionStart - textBox.GetFirstCharIndexOfCurrentLine() + 1;
                point.Y = textBox.GetLineFromCharIndex(textBox.SelectionStart) + 1;
            }

            return point;
        }

        public static void SetTabStopWidth(this TextBox textbox, int width)
        {
            SendMessage(textbox.Handle, EM_SETTABSTOPS, 1, new int[] { width * 4 });
        }

        public static string toHex(this byte[] value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (value != null)
            {
                foreach (byte b in value)
                {
                    stringBuilder.Append(HexStringTable[b]);
                }
            }
            return stringBuilder.ToString();
        }
    }
}