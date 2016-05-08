using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VIDEOEDITORLib;
using Microsoft.Win32; 

namespace TrailerBuilder
{
    public partial class Form1 : Form
    {
        IVideoClip videoClip = new VIDEOEDITORLib.VideoClipClass();
        IVideoClip videoClipPreview = new VIDEOEDITORLib.VideoClipClass();
        IMediaFile mainMediaFileVideo = null;
        IMediaFile mainMediaFileAudio = null;
        int currentButtonRow = 3;
        bool isSaving = false;
        bool isLoading = false;
        bool isRegistered = false;

        public Form1()
        {
            InitializeComponent();
        }

        string secondsToString(double d)
        {
            TimeSpan ts = TimeSpan.FromSeconds(d);

            string x = string.Format("{0:00}:{1:00}:{2:00}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            return x;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                clearFields();
                loadOriginalFile();
            }
        }

        private void clearFields()
        {
            startTime1.Text = "";
            startTime1.Text = "";
            endTime1.Text = "";
            loopCount1.Text = "";
            speed1.Text = "";

            startTime2.Text = "";
            startTime2.Text = "";
            endTime2.Text = "";
            loopCount2.Text = "";
            speed2.Text = "";

            startTime3.Text = "";
            startTime3.Text = "";
            endTime3.Text = "";
            loopCount3.Text = "";
            speed3.Text = "";

            startTime4.Text = "";
            startTime4.Text = "";
            endTime4.Text = "";
            loopCount4.Text = "";
            speed4.Text = "";

            startTime5.Text = "";
            startTime5.Text = "";
            endTime5.Text = "";
            loopCount5.Text = "";
            speed5.Text = "";

            startTime6.Text = "";
            startTime6.Text = "";
            endTime6.Text = "";
            loopCount6.Text = "";
            speed6.Text = "";
        }

        private void loadOriginalFile()
        {
            isLoading = true;
            videoClip = null;
            videoClip = new VIDEOEDITORLib.VideoClipClass();
            textBoxFile.Text = openFileDialog.FileName;
            mainMediaFileVideo = videoClip.AddVideo(0, textBoxFile.Text);
            mainMediaFileAudio = videoClip.AddAudio(0, textBoxFile.Text);
            labelDuration.Text = secondsToString(mainMediaFileVideo.Duration);

            axVideoEditorCtrl1.CurrentClip = (VideoClip)videoClip;

            if (isRegistered)
                axVideoEditorCtrl1.PassThruCmd(0, "DOVEBRAIN_ACTIVATE!");

            axVideoEditorCtrl1.Prime(pictureBoxPreview.Handle.ToInt32(), 5, 5, 355, 173);

            trackBarProgress.Maximum = (Int32)axVideoEditorCtrl1.Duration;
            timerProgress.Enabled = true;
            isLoading = false;
        }

        private double timeToSeconds(string str)
        {
            if (str.IndexOf(':') < 0)
                return Convert.ToInt32(str);

            string[] arr = str.Split(':');
            
            int seconds = Convert.ToInt32(arr[arr.Length - 1]);
            int minutes = Convert.ToInt32(arr[arr.Length - 2]);
            int hours = 0;
            if (arr.Length > 2)
                hours = Convert.ToInt32(arr[arr.Length - 3]);

            TimeSpan secs = TimeSpan.FromSeconds(seconds);
            TimeSpan mins = TimeSpan.FromMinutes(minutes);
            TimeSpan hour = TimeSpan.FromHours(hours);

            TimeSpan span = new TimeSpan();
            span = span.Add(secs);
            span = span.Add(mins);
            span = span.Add(hour);

            return span.TotalSeconds;
        }

        private bool addFile(string startTime, string endTime, string loopCount, string speed)
        {
            if (startTime == "") return false;
            if (endTime == "") return false;
            if (timeToSeconds(endTime) <= 0) return false;
            if (timeToSeconds(endTime) - timeToSeconds(startTime) <= 0) return false;

            int nLoopCount = 1;
            if (loopCount != "")
                nLoopCount = Int32.Parse(loopCount);
            if (nLoopCount < 1)
                nLoopCount = 1;

            String filepath = textBoxFile.Text;
            if (startTime != "")
            {
                double dStartTime = timeToSeconds(startTime);
                double dEndTime = timeToSeconds(endTime);
                double dSpeedPercent = 1;
                
                if (speed != "")
                    dSpeedPercent = 100.0 / Convert.ToDouble(speed);
                
                for (int i=0;i<nLoopCount;i++)
                {
                    IMediaFile pVideoFile1 = videoClipPreview.AddVideo(videoClipPreview.VideoCount, filepath);
                    pVideoFile1.MediaDuration = dEndTime - dStartTime;
                    pVideoFile1.Duration = pVideoFile1.MediaDuration * dSpeedPercent;
                    pVideoFile1.MediaStartOffset= dStartTime;
                    
                    pVideoFile1.Transition = "";
                    pVideoFile1.TransitionLength = 0;

                    IMediaFile pAudioFile1 = videoClipPreview.AddAudio(videoClipPreview.AudioCount, filepath);
                    pAudioFile1.MediaDuration = dEndTime - dStartTime;
                    pAudioFile1.Duration = pAudioFile1.MediaDuration * dSpeedPercent;
                    pAudioFile1.MediaStartOffset = dStartTime;
                    pAudioFile1.Transition = "";
                    pAudioFile1.TransitionLength = 0;
                }
            }

            return true;
        }

        private bool buildFile()
        {
            videoClipPreview = null;
            videoClipPreview = new VIDEOEDITORLib.VideoClipClass();

            if (addFile(startTime1.Text, endTime1.Text, loopCount1.Text, speed1.Text) == false)
                return false;

            addFile(startTime2.Text, endTime2.Text, loopCount2.Text, speed2.Text);
            addFile(startTime3.Text, endTime3.Text, loopCount3.Text, speed3.Text);
            addFile(startTime4.Text, endTime4.Text, loopCount4.Text, speed4.Text);
            addFile(startTime5.Text, endTime5.Text, loopCount5.Text, speed5.Text);
            addFile(startTime6.Text, endTime6.Text, loopCount6.Text, speed6.Text);
            addFile(startTime7.Text, endTime7.Text, loopCount7.Text, speed7.Text);
            addFile(startTime8.Text, endTime8.Text, loopCount8.Text, speed8.Text);
            addFile(startTime9.Text, endTime9.Text, loopCount9.Text, speed9.Text);
            addFile(startTime10.Text, endTime10.Text, loopCount10.Text, speed10.Text);

            return true;
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            isLoading = true;
            if (buildFile())
                axVideoEditorCtrl1.CurrentClip = (VideoClip)videoClipPreview;
            else
                axVideoEditorCtrl1.CurrentClip = (VideoClip)videoClip;

            if (isRegistered)
                axVideoEditorCtrl1.PassThruCmd(0, "DOVEBRAIN_ACTIVATE!");

            axVideoEditorCtrl1.StartPreview(pictureBoxPreview.Handle.ToInt32(), 5, 5, 355, 173);
            trackBarProgress.Maximum = (Int32)axVideoEditorCtrl1.Duration;
            labelDuration.Text = secondsToString(axVideoEditorCtrl1.Duration);
            isLoading = false;
        }

        private void trackBarProgress_Scroll(object sender, EventArgs e)
        {
            axVideoEditorCtrl1.Seek(trackBarProgress.Value);
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            labelPosition.Text = secondsToString(axVideoEditorCtrl1.Progress);
            trackBarProgress.Value = (Int32)axVideoEditorCtrl1.Progress;

            if (isSaving)
            {
                toolStripProgressBar1.Visible = true;
                toolStripProgressBar1.Maximum = (Int32)axVideoEditorCtrl1.Duration;
                toolStripProgressBar1.Value = trackBarProgress.Value;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel1.Text = "Building File...";
            }
        }

        private void pictureBoxStop_Click(object sender, EventArgs e)
        {
            axVideoEditorCtrl1.Stop();
        }

        private void pictureBoxPlay_Click(object sender, EventArgs e)
        {
            axVideoEditorCtrl1.Play();
        }

        private void pictureBoxPause_Click(object sender, EventArgs e)
        {
            axVideoEditorCtrl1.Pause();
        }

        private void buttonShowMore_Click(object sender, EventArgs e)
        {
            currentButtonRow++;
            showButtonRow(currentButtonRow);
        }

        private void showButtonRow(int index)
        {
            Control cp = null;
            Control c = GetNextControl(speed3, true);
            while (c != null && c.Visible == true)
            {
                c = GetNextControl(c, true);
            }

            for (int i = 0;i < 8;i++)
            {
                cp = c;
                c.Visible = true;
                c = GetNextControl(c, true);

                if (c.Name == "speed10")
                    buttonShowMore.Visible = false;
            }

            buttonShowMore.Top = cp.Bottom + 10;

            this.Height += 20;
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            FormSave formSave = new FormSave();
            formSave.fParent = this;
            formSave.ShowDialog(this);

            if (formSave.selectedFormat == -1)
                return;

            int formatIndex = 0;
            switch (formSave.selectedFormat)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    formatIndex = 1;
                    break;
                case 4:
                    formatIndex = 5;
                    break;
                case 5:
                    formatIndex = 6;
                    break;
                case 6:
                    formatIndex = 4;
                    break;
                case 11: // wmv
                    formatIndex = 3;
                    break;
                default: // mp4
                    formatIndex = 2;
                    break;

            }
            
            saveFileDialog.FilterIndex = formatIndex;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                isLoading = true;

                VideoClip clip = null;
                if (buildFile())
                    clip = (VideoClip)videoClipPreview;
                else
                    clip = (VideoClip)videoClip;

                axVideoEditorCtrl1.CurrentClip = clip;
                clip.OutputProfile = formSave.selectedProfile;
                clip.OutputFormat = (int)formSave.selectedFormat;
                clip.Width = (int)formSave.outputX;
                clip.Height = (int)formSave.outputY;
                int bit = formSave.selectedBitrate;
                clip.OutputBitrateString = bit.ToString();
                if (isRegistered)
                    axVideoEditorCtrl1.PassThruCmd(0, "DOVEBRAIN_ACTIVATE!");

                axVideoEditorCtrl1.CompileToFile(saveFileDialog.FileName);
                trackBarProgress.Maximum = (Int32)axVideoEditorCtrl1.Duration;
                isSaving = true;
                isLoading = false;
            }
        }

        private void axVideoEditorCtrl1_Complete(object sender, EventArgs e)
        {
            isSaving = false;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Text = "Done";

            if (!isLoading)
                timerReload.Enabled = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormRegistration form = new FormRegistration();
            form.ShowDialog(this);
            Auth();
        }

        private void Auth()
        {
            FormRegistration form = new FormRegistration();
            RegistryKey regKeyAppRoot = Registry.CurrentUser.CreateSubKey("SOFTWARE\\TrailerBuilder");
            String key = (String)regKeyAppRoot.GetValue("Key");
            regKeyAppRoot.Close();

            if (form.VerifyLicense(key))
            {
                isRegistered = true;
                linkLabelRegistration.Visible = false;
                pictureBoxReg.Visible = false;
                label47.Visible = false;
            }
        }

        private void timerReload_Tick(object sender, EventArgs e)
        {
            timerReload.Enabled = false;
            loadOriginalFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Auth();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                clearFields();
                loadOriginalFile();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onlineHelpPagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.snappyeditor.com/help.html");
        }

        private void aboutSnappyEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.snappyeditor.com/about.html");
        }
    }
}