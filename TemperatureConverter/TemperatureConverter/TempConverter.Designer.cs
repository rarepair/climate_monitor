namespace TemperatureConverter
{
    partial class TempConverter
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
            this.leftByteBox = new System.Windows.Forms.TextBox();
            this.rightByteBox = new System.Windows.Forms.TextBox();
            this.tempBox = new System.Windows.Forms.TextBox();
            this.leftBitBox = new System.Windows.Forms.TextBox();
            this.rightBitBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // leftByteBox
            // 
            this.leftByteBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.leftByteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.leftByteBox.Location = new System.Drawing.Point(81, 12);
            this.leftByteBox.MaxLength = 2;
            this.leftByteBox.Name = "leftByteBox";
            this.leftByteBox.Size = new System.Drawing.Size(41, 31);
            this.leftByteBox.TabIndex = 0;
            this.leftByteBox.Text = "00";          
            this.leftByteBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.leftByteBox_KeyUp);
            // 
            // rightByteBox
            // 
            this.rightByteBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.rightByteBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rightByteBox.Location = new System.Drawing.Point(161, 12);
            this.rightByteBox.MaxLength = 2;
            this.rightByteBox.Name = "rightByteBox";
            this.rightByteBox.Size = new System.Drawing.Size(39, 31);
            this.rightByteBox.TabIndex = 1;
            this.rightByteBox.Text = "00";
            this.rightByteBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rightByteBox_KeyUp);
            // 
            // tempBox
            // 
            this.tempBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tempBox.Location = new System.Drawing.Point(12, 84);
            this.tempBox.Name = "tempBox";
            this.tempBox.Size = new System.Drawing.Size(232, 62);
            this.tempBox.TabIndex = 2;
            this.tempBox.Text = "00.0000";
            this.tempBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // leftBitBox
            // 
            this.leftBitBox.Location = new System.Drawing.Point(50, 49);
            this.leftBitBox.Name = "leftBitBox";
            this.leftBitBox.Size = new System.Drawing.Size(72, 20);
            this.leftBitBox.TabIndex = 3;
            this.leftBitBox.Text = "0000 0000";
            this.leftBitBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.leftBitBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.leftBitBox_KeyUp);
            // 
            // rightBitBox
            // 
            this.rightBitBox.Location = new System.Drawing.Point(128, 49);
            this.rightBitBox.Name = "rightBitBox";
            this.rightBitBox.Size = new System.Drawing.Size(72, 20);
            this.rightBitBox.TabIndex = 4;
            this.rightBitBox.Text = "0000 0000";
            this.rightBitBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rightBitBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rightBitBox_KeyUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "0x";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(132, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 30);
            this.label2.TabIndex = 6;
            this.label2.Text = "0x";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TempConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(256, 158);
            this.Controls.Add(this.rightByteBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.leftByteBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rightBitBox);
            this.Controls.Add(this.leftBitBox);
            this.Controls.Add(this.tempBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TempConverter";
            this.Text = "ADT7310 Temp Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox leftByteBox;
        private System.Windows.Forms.TextBox rightByteBox;
        private System.Windows.Forms.TextBox tempBox;
        private System.Windows.Forms.TextBox leftBitBox;
        private System.Windows.Forms.TextBox rightBitBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

