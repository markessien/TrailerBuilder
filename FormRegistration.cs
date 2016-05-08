using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32; 

namespace TrailerBuilder
{
    public partial class FormRegistration : Form
    {
        public FormRegistration()
        {
            InitializeComponent();
        }

        private void buttonActivate_Click(object sender, EventArgs e)
        {
            String strKey = textBoxKey.Text;
            if (VerifyLicense(strKey))
            {
                RegistryKey regKeyAppRoot = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TrailerBuilder");
                regKeyAppRoot.SetValue("Key", strKey);
                regKeyAppRoot.Close();

                MessageBox.Show("Your software has been successfully registered");

                this.Close();
                
            }
            else
                MessageBox.Show("Your registration key was invalid");
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

        private bool VerifyPair(string strFirst, string strSecond)
        {
            if (strFirst.Length != strSecond.Length)
                return false;

            if (strFirst.Length != 4)
                return false;

            for (int i = 0; i < 4; i++)
            {
                if (strSecond[i] != TransformChar(strFirst[i]))
                    return false;
            }

            return true;
        }

        public bool VerifyLicense(string strLicense)
        {
            if (strLicense == null)
                return false;

            String[] arr = strLicense.Split('-');
            if (arr.Length < 4)
                return false;

            for (int i=0;i<3;i++)
            {
                if (VerifyPair(arr[i], arr[i+1]) == false)
                    return false;
            }

            return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.snappyeditor.com/order.aspx");
        }
    }
}