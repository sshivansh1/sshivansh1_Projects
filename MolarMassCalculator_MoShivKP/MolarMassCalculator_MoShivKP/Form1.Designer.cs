namespace MolarMassCalculator_MoShivKP
{
    partial class MMC_Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bnt_sortName = new System.Windows.Forms.Button();
            this.btn_singleSym = new System.Windows.Forms.Button();
            this.btn_atomicNo = new System.Windows.Forms.Button();
            this.tb_chemFormula = new System.Windows.Forms.TextBox();
            this.lbl_chemFormula = new System.Windows.Forms.Label();
            this.lbl_molarMass = new System.Windows.Forms.Label();
            this.tb_molarMass = new System.Windows.Forms.TextBox();
            this.UI_DGV_elementsDisplay = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.UI_DGV_elementsDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // bnt_sortName
            // 
            this.bnt_sortName.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnt_sortName.Location = new System.Drawing.Point(718, 11);
            this.bnt_sortName.Margin = new System.Windows.Forms.Padding(2);
            this.bnt_sortName.Name = "bnt_sortName";
            this.bnt_sortName.Size = new System.Drawing.Size(136, 49);
            this.bnt_sortName.TabIndex = 1;
            this.bnt_sortName.Text = "Sort By Name";
            this.bnt_sortName.UseVisualStyleBackColor = true;
            this.bnt_sortName.Click += new System.EventHandler(this.bnt_sortName_Click);
            // 
            // btn_singleSym
            // 
            this.btn_singleSym.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_singleSym.Location = new System.Drawing.Point(718, 73);
            this.btn_singleSym.Margin = new System.Windows.Forms.Padding(2);
            this.btn_singleSym.Name = "btn_singleSym";
            this.btn_singleSym.Size = new System.Drawing.Size(136, 49);
            this.btn_singleSym.TabIndex = 2;
            this.btn_singleSym.Text = "Single Character Symbol";
            this.btn_singleSym.UseVisualStyleBackColor = true;
            this.btn_singleSym.Click += new System.EventHandler(this.btn_singleSym_Click);
            // 
            // btn_atomicNo
            // 
            this.btn_atomicNo.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_atomicNo.Location = new System.Drawing.Point(718, 135);
            this.btn_atomicNo.Margin = new System.Windows.Forms.Padding(2);
            this.btn_atomicNo.Name = "btn_atomicNo";
            this.btn_atomicNo.Size = new System.Drawing.Size(136, 49);
            this.btn_atomicNo.TabIndex = 3;
            this.btn_atomicNo.Text = "Sort By Atomic Number";
            this.btn_atomicNo.UseVisualStyleBackColor = true;
            this.btn_atomicNo.Click += new System.EventHandler(this.btn_atomicNo_Click);
            // 
            // tb_chemFormula
            // 
            this.tb_chemFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_chemFormula.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_chemFormula.Location = new System.Drawing.Point(173, 423);
            this.tb_chemFormula.Margin = new System.Windows.Forms.Padding(2);
            this.tb_chemFormula.Name = "tb_chemFormula";
            this.tb_chemFormula.Size = new System.Drawing.Size(242, 28);
            this.tb_chemFormula.TabIndex = 4;
            this.tb_chemFormula.TextChanged += new System.EventHandler(this.tb_chemFormula_TextChanged);
            // 
            // lbl_chemFormula
            // 
            this.lbl_chemFormula.AutoSize = true;
            this.lbl_chemFormula.Font = new System.Drawing.Font("Comic Sans MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_chemFormula.Location = new System.Drawing.Point(21, 426);
            this.lbl_chemFormula.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_chemFormula.Name = "lbl_chemFormula";
            this.lbl_chemFormula.Size = new System.Drawing.Size(148, 23);
            this.lbl_chemFormula.TabIndex = 5;
            this.lbl_chemFormula.Text = "Chemical Formula:";
            // 
            // lbl_molarMass
            // 
            this.lbl_molarMass.AutoSize = true;
            this.lbl_molarMass.Font = new System.Drawing.Font("Comic Sans MS", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_molarMass.Location = new System.Drawing.Point(460, 426);
            this.lbl_molarMass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_molarMass.Name = "lbl_molarMass";
            this.lbl_molarMass.Size = new System.Drawing.Size(175, 23);
            this.lbl_molarMass.TabIndex = 6;
            this.lbl_molarMass.Text = "Approx. Molar Mass:";
            // 
            // tb_molarMass
            // 
            this.tb_molarMass.BackColor = System.Drawing.SystemColors.Window;
            this.tb_molarMass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_molarMass.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_molarMass.ForeColor = System.Drawing.Color.Green;
            this.tb_molarMass.Location = new System.Drawing.Point(648, 425);
            this.tb_molarMass.Margin = new System.Windows.Forms.Padding(2);
            this.tb_molarMass.Name = "tb_molarMass";
            this.tb_molarMass.ReadOnly = true;
            this.tb_molarMass.Size = new System.Drawing.Size(206, 28);
            this.tb_molarMass.TabIndex = 7;
            this.tb_molarMass.Text = "0";
            // 
            // UI_DGV_elementsDisplay
            // 
            this.UI_DGV_elementsDisplay.AllowUserToAddRows = false;
            this.UI_DGV_elementsDisplay.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UI_DGV_elementsDisplay.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.UI_DGV_elementsDisplay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.UI_DGV_elementsDisplay.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.UI_DGV_elementsDisplay.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.UI_DGV_elementsDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UI_DGV_elementsDisplay.DefaultCellStyle = dataGridViewCellStyle3;
            this.UI_DGV_elementsDisplay.GridColor = System.Drawing.SystemColors.InactiveBorder;
            this.UI_DGV_elementsDisplay.Location = new System.Drawing.Point(11, 9);
            this.UI_DGV_elementsDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.UI_DGV_elementsDisplay.Name = "UI_DGV_elementsDisplay";
            this.UI_DGV_elementsDisplay.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UI_DGV_elementsDisplay.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.UI_DGV_elementsDisplay.RowHeadersWidth = 51;
            this.UI_DGV_elementsDisplay.Size = new System.Drawing.Size(674, 368);
            this.UI_DGV_elementsDisplay.TabIndex = 8;
            // 
            // MMC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(865, 471);
            this.Controls.Add(this.UI_DGV_elementsDisplay);
            this.Controls.Add(this.tb_molarMass);
            this.Controls.Add(this.lbl_molarMass);
            this.Controls.Add(this.lbl_chemFormula);
            this.Controls.Add(this.tb_chemFormula);
            this.Controls.Add(this.btn_atomicNo);
            this.Controls.Add(this.btn_singleSym);
            this.Controls.Add(this.bnt_sortName);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MMC_Form";
            this.Text = "LINQ MMC";
            ((System.ComponentModel.ISupportInitialize)(this.UI_DGV_elementsDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button bnt_sortName;
        private System.Windows.Forms.Button btn_singleSym;
        private System.Windows.Forms.Button btn_atomicNo;
        private System.Windows.Forms.TextBox tb_chemFormula;
        private System.Windows.Forms.Label lbl_chemFormula;
        private System.Windows.Forms.Label lbl_molarMass;
        private System.Windows.Forms.TextBox tb_molarMass;
        private System.Windows.Forms.DataGridView UI_DGV_elementsDisplay;
    }
}

