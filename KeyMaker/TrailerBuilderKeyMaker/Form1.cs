using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrailerBuilderKeyMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private char TransformChar(char c)
        {
            int result = Convert.ToInt16(c) + 10;
            while (result > 85)
                result -= 7;

            while (result < 65)
                result += 6;

            return Convert.ToChar(result);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random RandomClass = new Random();
            for (int k = 0; k < 30; k++)
            {
                String strLicense = "";
                for (int i = 0; i < 4; i++)
                    strLicense += Convert.ToChar(RandomClass.Next(65, 89)).ToString();
                strLicense += "-";
                
                for (int i = 0; i < 4; i++)
                    strLicense += TransformChar(strLicense[i]).ToString();
                strLicense += "-";

                for (int i = 5; i < 9; i++)
                    strLicense += TransformChar(strLicense[i]).ToString();
                strLicense += "-";

                for (int i = 10; i < 14; i++)
                    strLicense += TransformChar(strLicense[i]).ToString();

                textBox1.Text += strLicense;
                textBox1.Text += "\r\n";
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}