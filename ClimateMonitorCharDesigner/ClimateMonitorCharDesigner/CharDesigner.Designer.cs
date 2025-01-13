namespace ClimateMonitorCharDesigner
{
    partial class CharDesigner
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CharDesigner));
            this.pixelBox = new System.Windows.Forms.PictureBox();
            this.byteBox = new System.Windows.Forms.TextBox();
            this.clearBytesButton = new System.Windows.Forms.Button();
            this.getArrayButton = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.statusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.shiftGroup = new System.Windows.Forms.GroupBox();
            this.wrapAroundCheckBox = new System.Windows.Forms.CheckBox();
            this.shiftUpButton = new System.Windows.Forms.Button();
            this.shiftRightButton = new System.Windows.Forms.Button();
            this.shiftDownButton = new System.Windows.Forms.Button();
            this.shiftLeftButton = new System.Windows.Forms.Button();
            this.flipGroup = new System.Windows.Forms.GroupBox();
            this.flipHorizontalButton = new System.Windows.Forms.Button();
            this.flipVerticalButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colBox = new System.Windows.Forms.TextBox();
            this.rowBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.shiftLeftTimer = new System.Windows.Forms.Timer(this.components);
            this.shiftRightTimer = new System.Windows.Forms.Timer(this.components);
            this.shiftDownTimer = new System.Windows.Forms.Timer(this.components);
            this.shiftUpTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.invertLCDButton = new System.Windows.Forms.Button();
            this.clearLCDButton = new System.Windows.Forms.Button();
            this.fillLCDButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.transparencySlider = new System.Windows.Forms.TrackBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pixelBox)).BeginInit();
            this.statusBar.SuspendLayout();
            this.shiftGroup.SuspendLayout();
            this.flipGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // pixelBox
            // 
            this.pixelBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pixelBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pixelBox.Location = new System.Drawing.Point(12, 6);
            this.pixelBox.Name = "pixelBox";
            this.pixelBox.Size = new System.Drawing.Size(579, 579);
            this.pixelBox.TabIndex = 0;
            this.pixelBox.TabStop = false;
            this.pixelBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pixelBox_Paint);
            this.pixelBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pixelBox_MouseDown);
            this.pixelBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pixelBox_MouseMove);
            // 
            // byteBox
            // 
            this.byteBox.AcceptsReturn = true;
            this.byteBox.AcceptsTab = true;
            this.byteBox.Font = new System.Drawing.Font("Calibri", 8F);
            this.byteBox.Location = new System.Drawing.Point(6, 52);
            this.byteBox.Multiline = true;
            this.byteBox.Name = "byteBox";
            this.byteBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.byteBox.Size = new System.Drawing.Size(345, 154);
            this.byteBox.TabIndex = 1;
            // 
            // clearBytesButton
            // 
            this.clearBytesButton.Location = new System.Drawing.Point(9, 19);
            this.clearBytesButton.Name = "clearBytesButton";
            this.clearBytesButton.Size = new System.Drawing.Size(65, 27);
            this.clearBytesButton.TabIndex = 2;
            this.clearBytesButton.Text = "Clear";
            this.clearBytesButton.UseVisualStyleBackColor = true;
            this.clearBytesButton.Click += new System.EventHandler(this.clearBytesButton_Click);
            // 
            // getArrayButton
            // 
            this.getArrayButton.Location = new System.Drawing.Point(80, 19);
            this.getArrayButton.Name = "getArrayButton";
            this.getArrayButton.Size = new System.Drawing.Size(65, 27);
            this.getArrayButton.TabIndex = 3;
            this.getArrayButton.Text = "Get Array";
            this.getArrayButton.UseVisualStyleBackColor = true;
            this.getArrayButton.Click += new System.EventHandler(this.getArrayButton_Click);
            // 
            // statusBar
            // 
            this.statusBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 593);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(969, 22);
            this.statusBar.TabIndex = 4;
            this.statusBar.Text = "statusBar";
            // 
            // statusBarLabel
            // 
            this.statusBarLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusBarLabel.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.statusBarLabel.Name = "statusBarLabel";
            this.statusBarLabel.Size = new System.Drawing.Size(187, 17);
            this.statusBarLabel.Text = "Sharp Memory LCD Char Designer";
            // 
            // shiftGroup
            // 
            this.shiftGroup.Controls.Add(this.wrapAroundCheckBox);
            this.shiftGroup.Controls.Add(this.shiftUpButton);
            this.shiftGroup.Controls.Add(this.shiftRightButton);
            this.shiftGroup.Controls.Add(this.shiftDownButton);
            this.shiftGroup.Controls.Add(this.shiftLeftButton);
            this.shiftGroup.Location = new System.Drawing.Point(600, 6);
            this.shiftGroup.Name = "shiftGroup";
            this.shiftGroup.Size = new System.Drawing.Size(157, 192);
            this.shiftGroup.TabIndex = 9;
            this.shiftGroup.TabStop = false;
            this.shiftGroup.Text = "Shift";
            // 
            // wrapAroundCheckBox
            // 
            this.wrapAroundCheckBox.AutoSize = true;
            this.wrapAroundCheckBox.Location = new System.Drawing.Point(34, 168);
            this.wrapAroundCheckBox.Name = "wrapAroundCheckBox";
            this.wrapAroundCheckBox.Size = new System.Drawing.Size(89, 17);
            this.wrapAroundCheckBox.TabIndex = 8;
            this.wrapAroundCheckBox.Text = "Wrap Around";
            this.wrapAroundCheckBox.UseVisualStyleBackColor = true;
            // 
            // shiftUpButton
            // 
            this.shiftUpButton.Image = ((System.Drawing.Image)(resources.GetObject("shiftUpButton.Image")));
            this.shiftUpButton.Location = new System.Drawing.Point(54, 14);
            this.shiftUpButton.Name = "shiftUpButton";
            this.shiftUpButton.Size = new System.Drawing.Size(48, 49);
            this.shiftUpButton.TabIndex = 5;
            this.shiftUpButton.TabStop = false;
            this.shiftUpButton.UseVisualStyleBackColor = true;
            this.shiftUpButton.Click += new System.EventHandler(this.shiftUpButton_Click);
            this.shiftUpButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shiftUpButton_MouseDown);
            this.shiftUpButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.shiftUpButton_MouseUp);
            // 
            // shiftRightButton
            // 
            this.shiftRightButton.Image = ((System.Drawing.Image)(resources.GetObject("shiftRightButton.Image")));
            this.shiftRightButton.Location = new System.Drawing.Point(100, 64);
            this.shiftRightButton.Name = "shiftRightButton";
            this.shiftRightButton.Size = new System.Drawing.Size(48, 48);
            this.shiftRightButton.TabIndex = 2;
            this.shiftRightButton.TabStop = false;
            this.shiftRightButton.UseVisualStyleBackColor = true;
            this.shiftRightButton.Click += new System.EventHandler(this.shiftRightButton_Click);
            this.shiftRightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shiftRightButton_MouseDown);
            this.shiftRightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.shiftRightButton_MouseUp);
            // 
            // shiftDownButton
            // 
            this.shiftDownButton.Image = ((System.Drawing.Image)(resources.GetObject("shiftDownButton.Image")));
            this.shiftDownButton.Location = new System.Drawing.Point(54, 114);
            this.shiftDownButton.Name = "shiftDownButton";
            this.shiftDownButton.Size = new System.Drawing.Size(48, 48);
            this.shiftDownButton.TabIndex = 6;
            this.shiftDownButton.TabStop = false;
            this.shiftDownButton.UseVisualStyleBackColor = true;
            this.shiftDownButton.Click += new System.EventHandler(this.shiftDownButton_Click);
            this.shiftDownButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shiftDownButton_MouseDown);
            this.shiftDownButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.shiftDownButton_MouseUp);
            // 
            // shiftLeftButton
            // 
            this.shiftLeftButton.Image = ((System.Drawing.Image)(resources.GetObject("shiftLeftButton.Image")));
            this.shiftLeftButton.Location = new System.Drawing.Point(9, 63);
            this.shiftLeftButton.Name = "shiftLeftButton";
            this.shiftLeftButton.Size = new System.Drawing.Size(48, 49);
            this.shiftLeftButton.TabIndex = 0;
            this.shiftLeftButton.TabStop = false;
            this.shiftLeftButton.UseVisualStyleBackColor = true;
            this.shiftLeftButton.Click += new System.EventHandler(this.shiftLeftButton_Click);
            this.shiftLeftButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.shiftLeftButton_MouseDown);
            this.shiftLeftButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.shiftLeftButton_MouseUp);
            // 
            // flipGroup
            // 
            this.flipGroup.Controls.Add(this.flipHorizontalButton);
            this.flipGroup.Controls.Add(this.flipVerticalButton);
            this.flipGroup.Location = new System.Drawing.Point(600, 204);
            this.flipGroup.Name = "flipGroup";
            this.flipGroup.Size = new System.Drawing.Size(157, 78);
            this.flipGroup.TabIndex = 10;
            this.flipGroup.TabStop = false;
            this.flipGroup.Text = "Flip";
            // 
            // flipHorizontalButton
            // 
            this.flipHorizontalButton.Location = new System.Drawing.Point(40, 16);
            this.flipHorizontalButton.Name = "flipHorizontalButton";
            this.flipHorizontalButton.Size = new System.Drawing.Size(81, 23);
            this.flipHorizontalButton.TabIndex = 4;
            this.flipHorizontalButton.Text = "Flip Horizontal";
            this.flipHorizontalButton.UseVisualStyleBackColor = true;
            this.flipHorizontalButton.Click += new System.EventHandler(this.flipHorizontalButton_Click);
            // 
            // flipVerticalButton
            // 
            this.flipVerticalButton.Location = new System.Drawing.Point(40, 45);
            this.flipVerticalButton.Name = "flipVerticalButton";
            this.flipVerticalButton.Size = new System.Drawing.Size(81, 23);
            this.flipVerticalButton.TabIndex = 3;
            this.flipVerticalButton.Text = "Flip Vertical";
            this.flipVerticalButton.UseVisualStyleBackColor = true;
            this.flipVerticalButton.Click += new System.EventHandler(this.flipVerticalButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.colBox);
            this.groupBox1.Controls.Add(this.rowBox);
            this.groupBox1.Location = new System.Drawing.Point(600, 288);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(157, 79);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Character Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Columns";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rows";
            // 
            // colBox
            // 
            this.colBox.Location = new System.Drawing.Point(94, 46);
            this.colBox.Name = "colBox";
            this.colBox.Size = new System.Drawing.Size(42, 20);
            this.colBox.TabIndex = 1;
            this.colBox.Text = "96";
            this.colBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.colBox_KeyDown);
            // 
            // rowBox
            // 
            this.rowBox.Location = new System.Drawing.Point(94, 20);
            this.rowBox.Name = "rowBox";
            this.rowBox.Size = new System.Drawing.Size(42, 20);
            this.rowBox.TabIndex = 0;
            this.rowBox.Text = "96";
            this.rowBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rowBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rowBox_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.byteBox);
            this.groupBox2.Controls.Add(this.clearBytesButton);
            this.groupBox2.Controls.Add(this.getArrayButton);
            this.groupBox2.Location = new System.Drawing.Point(600, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(357, 212);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Code Output";
            // 
            // shiftLeftTimer
            // 
            this.shiftLeftTimer.Interval = 150;
            this.shiftLeftTimer.Tick += new System.EventHandler(this.shiftLeftTimer_Tick);
            // 
            // shiftRightTimer
            // 
            this.shiftRightTimer.Interval = 150;
            this.shiftRightTimer.Tick += new System.EventHandler(this.shiftRightTimer_Tick);
            // 
            // shiftDownTimer
            // 
            this.shiftDownTimer.Interval = 150;
            this.shiftDownTimer.Tick += new System.EventHandler(this.shiftDownTimer_Tick);
            // 
            // shiftUpTimer
            // 
            this.shiftUpTimer.Interval = 150;
            this.shiftUpTimer.Tick += new System.EventHandler(this.shiftUpTimer_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.invertLCDButton);
            this.groupBox3.Controls.Add(this.clearLCDButton);
            this.groupBox3.Controls.Add(this.fillLCDButton);
            this.groupBox3.Location = new System.Drawing.Point(763, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(157, 104);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Global Actions";
            // 
            // invertLCDButton
            // 
            this.invertLCDButton.Location = new System.Drawing.Point(38, 45);
            this.invertLCDButton.Name = "invertLCDButton";
            this.invertLCDButton.Size = new System.Drawing.Size(81, 23);
            this.invertLCDButton.TabIndex = 16;
            this.invertLCDButton.Text = "Invert";
            this.invertLCDButton.UseVisualStyleBackColor = true;
            this.invertLCDButton.Click += new System.EventHandler(this.invertLCDButton_Click);
            // 
            // clearLCDButton
            // 
            this.clearLCDButton.Location = new System.Drawing.Point(38, 74);
            this.clearLCDButton.Name = "clearLCDButton";
            this.clearLCDButton.Size = new System.Drawing.Size(81, 23);
            this.clearLCDButton.TabIndex = 15;
            this.clearLCDButton.Text = "Clear";
            this.clearLCDButton.UseVisualStyleBackColor = true;
            this.clearLCDButton.Click += new System.EventHandler(this.clearLCDButton_Click);
            // 
            // fillLCDButton
            // 
            this.fillLCDButton.Location = new System.Drawing.Point(38, 16);
            this.fillLCDButton.Name = "fillLCDButton";
            this.fillLCDButton.Size = new System.Drawing.Size(81, 23);
            this.fillLCDButton.TabIndex = 14;
            this.fillLCDButton.Text = "Fill";
            this.fillLCDButton.UseVisualStyleBackColor = true;
            this.fillLCDButton.Click += new System.EventHandler(this.fillLCDButton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.transparencySlider);
            this.groupBox4.Location = new System.Drawing.Point(763, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(157, 82);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Window Opacity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "100%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "30%";
            // 
            // transparencySlider
            // 
            this.transparencySlider.Location = new System.Drawing.Point(6, 19);
            this.transparencySlider.Minimum = 3;
            this.transparencySlider.Name = "transparencySlider";
            this.transparencySlider.Size = new System.Drawing.Size(145, 45);
            this.transparencySlider.TabIndex = 0;
            this.transparencySlider.Value = 10;
            this.transparencySlider.ValueChanged += new System.EventHandler(this.transparencySlider_ValueChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CharDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(969, 615);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flipGroup);
            this.Controls.Add(this.shiftGroup);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.pixelBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "CharDesigner";
            this.Text = "Sharp Memory LCD Char Designer";
            ((System.ComponentModel.ISupportInitialize)(this.pixelBox)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.shiftGroup.ResumeLayout(false);
            this.shiftGroup.PerformLayout();
            this.flipGroup.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencySlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pixelBox;
        private System.Windows.Forms.TextBox byteBox;
        private System.Windows.Forms.Button clearBytesButton;
        private System.Windows.Forms.Button getArrayButton;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.GroupBox shiftGroup;
        private System.Windows.Forms.CheckBox wrapAroundCheckBox;
        private System.Windows.Forms.Button shiftUpButton;
        private System.Windows.Forms.Button shiftRightButton;
        private System.Windows.Forms.Button shiftDownButton;
        private System.Windows.Forms.Button shiftLeftButton;
        private System.Windows.Forms.GroupBox flipGroup;
        private System.Windows.Forms.Button flipHorizontalButton;
        private System.Windows.Forms.Button flipVerticalButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox colBox;
        private System.Windows.Forms.TextBox rowBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripStatusLabel statusBarLabel;
        private System.Windows.Forms.Timer shiftLeftTimer;
        private System.Windows.Forms.Timer shiftRightTimer;
        private System.Windows.Forms.Timer shiftDownTimer;
        private System.Windows.Forms.Timer shiftUpTimer;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button clearLCDButton;
        private System.Windows.Forms.Button fillLCDButton;
        private System.Windows.Forms.Button invertLCDButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar transparencySlider;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ImageList imageList1;
    }
}

