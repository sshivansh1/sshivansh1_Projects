
namespace Server
{
    partial class Server
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
            this.tb_RndGuessNum = new System.Windows.Forms.TextBox();
            this.lbl_RndGuess = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_RndGuessNum
            // 
            this.tb_RndGuessNum.Location = new System.Drawing.Point(240, 47);
            this.tb_RndGuessNum.Margin = new System.Windows.Forms.Padding(4);
            this.tb_RndGuessNum.Name = "tb_RndGuessNum";
            this.tb_RndGuessNum.Size = new System.Drawing.Size(168, 24);
            this.tb_RndGuessNum.TabIndex = 0;
            this.tb_RndGuessNum.Text = "Guess Number";
            // 
            // lbl_RndGuess
            // 
            this.lbl_RndGuess.AutoSize = true;
            this.lbl_RndGuess.Location = new System.Drawing.Point(44, 47);
            this.lbl_RndGuess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_RndGuess.Name = "lbl_RndGuess";
            this.lbl_RndGuess.Size = new System.Drawing.Size(174, 18);
            this.lbl_RndGuess.TabIndex = 1;
            this.lbl_RndGuess.Text = "Random Guess Number:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 101);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_RndGuess);
            this.Controls.Add(this.tb_RndGuessNum);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_RndGuessNum;
        private System.Windows.Forms.Label lbl_RndGuess;
        private System.Windows.Forms.Label label1;
    }
}

