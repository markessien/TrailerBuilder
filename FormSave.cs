using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrailerBuilder
{
    public partial class FormSave : Form
    {
        public Form1 fParent = null;
        public long selectedFormat = -1;
        public long outputX = 0;
        public long outputY = 0;
        public Int32 selectedBitrate = 0;
        public Int32 selectedProfile = -1;

        public FormSave()
        {
            InitializeComponent();
        }

        private void FormSave_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < fParent.axVideoEditorCtrl1.OutputFormatCount; i++)
            {
                cbFormat.Items.Add(fParent.axVideoEditorCtrl1.get_OutputFormatDescription(i));
            }

            cbFormat.SelectedIndex = 7;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            selectedFormat = cbFormat.SelectedIndex;
            String selectedDimensions = comboBoxDimensions.Text;
            if (selectedDimensions != "")
            {
                string[] str = selectedDimensions.Split('x');
                if (str.Length > 1)
                {
                    outputX = Int32.Parse(str[0]);
                    outputY = Int32.Parse(str[1]);
                }

            }
            String strBitrate = comboBoxBitrate.Text;
            if (strBitrate != "" && strBitrate != "Automatic")
            {
                strBitrate = strBitrate.Replace("kb/sec", "");
                strBitrate = strBitrate.Replace("kb", "");
                strBitrate = strBitrate.Replace("kb/s", "");

                selectedBitrate = Int32.Parse(strBitrate);
            }

            selectedProfile = comboBoxProfiles.SelectedIndex;

            this.Close();
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFormat.SelectedIndex == 11)
            {
                comboBoxProfiles.Visible = true;
                comboBoxDimensions.Visible = false;
                comboBoxBitrate.Visible = false;
                labelBitrate.Visible = false;
                label2.Text = "Loading profiles....";
                Application.DoEvents();
                long nCount = fParent.axVideoEditorCtrl1.LoadWMVProfiles();
                for (int i = 0; i < nCount; i++)
                {
                    string profileName = "";
                    string profileDescription = "";
                    fParent.axVideoEditorCtrl1.GetWMVProfile(i, out profileName, out profileDescription);
                    comboBoxProfiles.Items.Add(profileName);
                }

                comboBoxProfiles.SelectedIndex = 1;

                label2.Text = "Select Profile:";
            }
            else
            {
                label2.Text = "Dimensions:";
                comboBoxProfiles.Visible = false;
                comboBoxDimensions.Visible = true;
                comboBoxBitrate.Visible = true;
                labelBitrate.Visible = true;
            }
        }
    }
}