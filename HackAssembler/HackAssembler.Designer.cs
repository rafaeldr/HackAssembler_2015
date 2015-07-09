namespace HackAssembler
{
    partial class HackAssembler
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
            this.btnASM = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnASM
            // 
            this.btnASM.Location = new System.Drawing.Point(134, 43);
            this.btnASM.Name = "btnASM";
            this.btnASM.Size = new System.Drawing.Size(124, 50);
            this.btnASM.TabIndex = 0;
            this.btnASM.Text = "ASM";
            this.btnASM.UseVisualStyleBackColor = true;
            this.btnASM.Click += new System.EventHandler(this.btnASM_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // HackAssembler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 150);
            this.Controls.Add(this.btnASM);
            this.Name = "HackAssembler";
            this.Text = "HackAssembler";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnASM;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

