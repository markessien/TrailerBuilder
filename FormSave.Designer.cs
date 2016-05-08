namespace TrailerBuilder
{
    partial class FormSave
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSave));
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDimensions = new System.Windows.Forms.ComboBox();
            this.comboBoxBitrate = new System.Windows.Forms.ComboBox();
            this.labelBitrate = new System.Windows.Forms.Label();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownHeight = 146;
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.IntegralHeight = false;
            this.cbFormat.Location = new System.Drawing.Point(12, 35);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(351, 21);
            this.cbFormat.TabIndex = 0;
            this.cbFormat.SelectedIndexChanged += new System.EventHandler(this.cbFormat_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select the output format:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dimensions:";
            // 
            // comboBoxDimensions
            // 
            this.comboBoxDimensions.FormattingEnabled = true;
            this.comboBoxDimensions.Items.AddRange(new object[] {
            "320 x 240",
            "640 x 480"});
            this.comboBoxDimensions.Location = new System.Drawing.Point(12, 86);
            this.comboBoxDimensions.Name = "comboBoxDimensions";
            this.comboBoxDimensions.Size = new System.Drawing.Size(119, 21);
            this.comboBoxDimensions.TabIndex = 3;
            this.comboBoxDimensions.Text = "Same as file";
            // 
            // comboBoxBitrate
            // 
            this.comboBoxBitrate.FormattingEnabled = true;
            this.comboBoxBitrate.Items.AddRange(new object[] {
            "Automatic",
            "1000kb/sec",
            "500kb/sec",
            "100kb/sec"});
            this.comboBoxBitrate.Location = new System.Drawing.Point(191, 86);
            this.comboBoxBitrate.Name = "comboBoxBitrate";
            this.comboBoxBitrate.Size = new System.Drawing.Size(172, 21);
            this.comboBoxBitrate.TabIndex = 5;
            this.comboBoxBitrate.Text = "Automatic";
            // 
            // labelBitrate
            // 
            this.labelBitrate.AutoSize = true;
            this.labelBitrate.Location = new System.Drawing.Point(188, 70);
            this.labelBitrate.Name = "labelBitrate";
            this.labelBitrate.Size = new System.Drawing.Size(40, 13);
            this.labelBitrate.TabIndex = 4;
            this.labelBitrate.Text = "Bitrate:";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Location = new System.Drawing.Point(191, 135);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(171, 40);
            this.buttonBuild.TabIndex = 6;
            this.buttonBuild.Text = "Start Build";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(13, 87);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(349, 21);
            this.comboBoxProfiles.TabIndex = 7;
            this.comboBoxProfiles.Visible = false;
            // 
            // FormSave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 196);
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.comboBoxBitrate);
            this.Controls.Add(this.labelBitrate);
            this.Controls.Add(this.comboBoxDimensions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFormat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Build File";
            this.Load += new System.EventHandler(this.FormSave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDimensions;
        private System.Windows.Forms.ComboBox comboBoxBitrate;
        private System.Windows.Forms.Label labelBitrate;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
    }
}