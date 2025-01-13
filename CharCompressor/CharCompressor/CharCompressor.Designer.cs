namespace CharCompressor
{
    partial class charCompressorForm
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
            this.outputBox = new System.Windows.Forms.TextBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.compressionTableBox = new System.Windows.Forms.TextBox();
            this.compressedArrayBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(12, 48);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(194, 318);
            this.outputBox.TabIndex = 0;
            // 
            // analyzeButton
            // 
            this.analyzeButton.Location = new System.Drawing.Point(417, 6);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(79, 36);
            this.analyzeButton.TabIndex = 1;
            this.analyzeButton.Text = "Analyze";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // compressionTableBox
            // 
            this.compressionTableBox.Location = new System.Drawing.Point(212, 48);
            this.compressionTableBox.Multiline = true;
            this.compressionTableBox.Name = "compressionTableBox";
            this.compressionTableBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.compressionTableBox.Size = new System.Drawing.Size(224, 318);
            this.compressionTableBox.TabIndex = 2;
            // 
            // compressedArrayBox
            // 
            this.compressedArrayBox.Location = new System.Drawing.Point(442, 48);
            this.compressedArrayBox.Multiline = true;
            this.compressedArrayBox.Name = "compressedArrayBox";
            this.compressedArrayBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.compressedArrayBox.Size = new System.Drawing.Size(447, 318);
            this.compressedArrayBox.TabIndex = 4;
            // 
            // charCompressorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 378);
            this.Controls.Add(this.compressedArrayBox);
            this.Controls.Add(this.compressionTableBox);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.outputBox);
            this.Name = "charCompressorForm";
            this.Text = "Char Compressor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.TextBox compressionTableBox;
        private System.Windows.Forms.TextBox compressedArrayBox;
    }
}

