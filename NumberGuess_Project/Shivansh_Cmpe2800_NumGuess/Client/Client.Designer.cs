﻿
namespace Client
{
    partial class Client
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
            this.btn_Connect = new System.Windows.Forms.Button();
            this.tbar_Guess = new System.Windows.Forms.TrackBar();
            this.tb_Status = new System.Windows.Forms.TextBox();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_Guess = new System.Windows.Forms.Button();
            this.lbl_MinVal = new System.Windows.Forms.Label();
            this.lbl_MaxVal = new System.Windows.Forms.Label();
            this.lbl_GuessVal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbar_Guess)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(23, 21);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(129, 38);
            this.btn_Connect.TabIndex = 0;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // tbar_Guess
            // 
            this.tbar_Guess.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbar_Guess.Location = new System.Drawing.Point(23, 81);
            this.tbar_Guess.Maximum = 1000;
            this.tbar_Guess.Minimum = 1;
            this.tbar_Guess.Name = "tbar_Guess";
            this.tbar_Guess.Size = new System.Drawing.Size(470, 45);
            this.tbar_Guess.TabIndex = 1;
            this.tbar_Guess.Value = 1;
            this.tbar_Guess.Scroll += new System.EventHandler(this.tbar_Guess_Scroll);
            // 
            // tb_Status
            // 
            this.tb_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_Status.Location = new System.Drawing.Point(69, 206);
            this.tb_Status.Name = "tb_Status";
            this.tb_Status.ReadOnly = true;
            this.tb_Status.Size = new System.Drawing.Size(180, 20);
            this.tb_Status.TabIndex = 2;
            this.tb_Status.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(20, 211);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(43, 13);
            this.lbl_Status.TabIndex = 3;
            this.lbl_Status.Text = "Status: ";
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(341, 21);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(129, 38);
            this.btn_Disconnect.TabIndex = 5;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // btn_Guess
            // 
            this.btn_Guess.Location = new System.Drawing.Point(347, 196);
            this.btn_Guess.Name = "btn_Guess";
            this.btn_Guess.Size = new System.Drawing.Size(123, 28);
            this.btn_Guess.TabIndex = 6;
            this.btn_Guess.Text = "Guess";
            this.btn_Guess.UseVisualStyleBackColor = true;
            this.btn_Guess.Click += new System.EventHandler(this.Btn_Guess_Click);
            // 
            // lbl_MinVal
            // 
            this.lbl_MinVal.AutoSize = true;
            this.lbl_MinVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MinVal.Location = new System.Drawing.Point(20, 140);
            this.lbl_MinVal.Name = "lbl_MinVal";
            this.lbl_MinVal.Size = new System.Drawing.Size(15, 15);
            this.lbl_MinVal.TabIndex = 7;
            this.lbl_MinVal.Text = "1";
            // 
            // lbl_MaxVal
            // 
            this.lbl_MaxVal.AutoSize = true;
            this.lbl_MaxVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MaxVal.Location = new System.Drawing.Point(462, 142);
            this.lbl_MaxVal.Name = "lbl_MaxVal";
            this.lbl_MaxVal.Size = new System.Drawing.Size(33, 15);
            this.lbl_MaxVal.TabIndex = 8;
            this.lbl_MaxVal.Text = "1000";
            // 
            // lbl_GuessVal
            // 
            this.lbl_GuessVal.AutoSize = true;
            this.lbl_GuessVal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_GuessVal.Location = new System.Drawing.Point(248, 140);
            this.lbl_GuessVal.Name = "lbl_GuessVal";
            this.lbl_GuessVal.Size = new System.Drawing.Size(15, 15);
            this.lbl_GuessVal.TabIndex = 9;
            this.lbl_GuessVal.Text = "1";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 243);
            this.Controls.Add(this.lbl_GuessVal);
            this.Controls.Add(this.lbl_MaxVal);
            this.Controls.Add(this.lbl_MinVal);
            this.Controls.Add(this.btn_Guess);
            this.Controls.Add(this.btn_Disconnect);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.tb_Status);
            this.Controls.Add(this.tbar_Guess);
            this.Controls.Add(this.btn_Connect);
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbar_Guess)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.TrackBar tbar_Guess;
        private System.Windows.Forms.TextBox tb_Status;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_Guess;
        private System.Windows.Forms.Label lbl_MinVal;
        private System.Windows.Forms.Label lbl_MaxVal;
        private System.Windows.Forms.Label lbl_GuessVal;
    }
}

