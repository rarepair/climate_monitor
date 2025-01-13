using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TemperatureConverter
{
    public partial class TempConverter : Form
    {
        int leftByte;
        int rightByte;
        public TempConverter()
        {
            InitializeComponent();
            leftByte = 0;
            rightByte = 0;
        }

        private void leftByteBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte enteredValue = 0x00;
                try
                {
                    enteredValue = byte.Parse(leftByteBox.Text, System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    leftByteBox.BackColor = Color.Red;
                    return;
                }
                leftByteBox.BackColor = Color.White;
                leftByte = Convert.ToInt32(enteredValue);

                leftBitBox.Text = convertByteToBitString(enteredValue);

                tempBox.Text = convertTempDoubleToString(convertBytesToTemp(leftByte, rightByte));
                
            }
            
        }

        private void rightByteBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte enteredValue = 0x00;
                try
                {
                    enteredValue = byte.Parse(rightByteBox.Text, System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    rightByteBox.BackColor = Color.Red;
                    return;
                }
                rightByteBox.BackColor = Color.White;
                rightByte = Convert.ToInt32(enteredValue);

                rightBitBox.Text = convertByteToBitString(enteredValue);

                tempBox.Text = convertTempDoubleToString(convertBytesToTemp(leftByte, rightByte));
            }
        }

        private void leftBitBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte enteredValue = 0x00;
                try
                {
                    enteredValue = convertBitStringToByte(leftBitBox.Text);
                }
                catch
                {
                    leftBitBox.BackColor = Color.Red;
                    return;
                }

                leftBitBox.BackColor = Color.White;
                leftByte = Convert.ToInt32(enteredValue);
                leftByteBox.Text = Extensions.TextBoxExtension.toHex(enteredValue);
                tempBox.Text = convertTempDoubleToString(convertBytesToTemp(leftByte, rightByte));
            }
        }

        private void rightBitBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                byte enteredValue = 0x00;
                try
                {
                    enteredValue = convertBitStringToByte(rightBitBox.Text);
                }
                catch
                {
                    rightBitBox.BackColor = Color.Red;
                    return;
                }

                rightBitBox.BackColor = Color.White;
                rightByte = Convert.ToInt32(enteredValue);
                rightByteBox.Text = Extensions.TextBoxExtension.toHex(enteredValue);
                tempBox.Text = convertTempDoubleToString(convertBytesToTemp(leftByte, rightByte));
            }
        }

        public double convertBytesToTemp(int leftByte, int rightByte)
        {
            double temperature = 9999.99;
            int twosComplementInt = 0;

            leftByte = leftByte << 8;
            twosComplementInt = leftByte | rightByte;
            if ((twosComplementInt & 0x8000) == 0x8000)
            {
                twosComplementInt = twosComplementInt - 65536;
            }

            temperature = (double)twosComplementInt / 128.0;
            return temperature;
        }

        public string convertTempDoubleToString(double temp)
        {
            String tempString = temp.ToString();
            try
            {
                int decimalIndex = 0;
                decimalIndex = tempString.IndexOf(".");
                tempString = tempString.Remove(tempString.IndexOf(".") + 5);
            }
            catch { }

            return tempString;
        }

        public String convertByteToBitString(byte b)
        {
            StringBuilder str = new StringBuilder(8);
            int[] bl = new int[8];

            for (int i = 0; i < bl.Length; i++)
            {
                bl[bl.Length - 1 - i] = ((b & (1 << i)) != 0) ? 1 : 0;
            }

            foreach (int num in bl) str.Append(num);

            str.Insert(4, " ");

            return str.ToString();
        }

        public byte convertBitStringToByte(string bitString)
        {
            byte convertedByte = 0x00;
            String workingString = bitString;

            workingString = workingString.Remove(workingString.IndexOf(" "), 1);
            
            for (int index = 0; index < 8; index++)
            {
                if ((workingString.ToCharArray()[index] != '0') && (workingString.ToCharArray()[index] != '1'))
                {
                    throw new System.Exception("bitString contains invalid values");
                }
                
                if (workingString[index] == '1')
                {
                    convertedByte = (byte)(convertedByte | (0x80 >> index));
                }
            }

            return convertedByte;
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
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
            "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
            "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
            "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
            "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
            "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
            "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"
        };

        public static string toHex(this byte value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (value != null)
            {
                stringBuilder.Append(HexStringTable[value]);
            }
            return stringBuilder.ToString();
        }
    }
}