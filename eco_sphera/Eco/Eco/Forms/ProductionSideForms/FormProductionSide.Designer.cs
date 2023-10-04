
namespace Eco.Forms.ProductionSideForms
{
    partial class FormProductionSide
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
            this.buttonAddProductionSide = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblForNameCompanyDO = new System.Windows.Forms.Label();
            this.lblForIdCompany = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNamePoductionSide = new System.Windows.Forms.TextBox();
            this.tbAdminArea = new System.Windows.Forms.TextBox();
            this.tbRegion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAddProductionSide
            // 
            this.buttonAddProductionSide.Location = new System.Drawing.Point(94, 289);
            this.buttonAddProductionSide.Name = "buttonAddProductionSide";
            this.buttonAddProductionSide.Size = new System.Drawing.Size(75, 23);
            this.buttonAddProductionSide.TabIndex = 0;
            this.buttonAddProductionSide.Text = "Добавить";
            this.buttonAddProductionSide.UseVisualStyleBackColor = true;
            this.buttonAddProductionSide.Click += new System.EventHandler(this.buttonAddProductionSide_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название предприятия";
            // 
            // lblForNameCompanyDO
            // 
            this.lblForNameCompanyDO.AutoSize = true;
            this.lblForNameCompanyDO.Location = new System.Drawing.Point(29, 56);
            this.lblForNameCompanyDO.Name = "lblForNameCompanyDO";
            this.lblForNameCompanyDO.Size = new System.Drawing.Size(0, 13);
            this.lblForNameCompanyDO.TabIndex = 2;
            // 
            // lblForIdCompany
            // 
            this.lblForIdCompany.AutoSize = true;
            this.lblForIdCompany.Location = new System.Drawing.Point(158, 56);
            this.lblForIdCompany.Name = "lblForIdCompany";
            this.lblForIdCompany.Size = new System.Drawing.Size(0, 13);
            this.lblForIdCompany.TabIndex = 3;
            this.lblForIdCompany.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите название площадки";
            // 
            // tbNamePoductionSide
            // 
            this.tbNamePoductionSide.Location = new System.Drawing.Point(32, 121);
            this.tbNamePoductionSide.Name = "tbNamePoductionSide";
            this.tbNamePoductionSide.Size = new System.Drawing.Size(218, 20);
            this.tbNamePoductionSide.TabIndex = 5;
            // 
            // tbAdminArea
            // 
            this.tbAdminArea.Location = new System.Drawing.Point(29, 184);
            this.tbAdminArea.Name = "tbAdminArea";
            this.tbAdminArea.Size = new System.Drawing.Size(218, 20);
            this.tbAdminArea.TabIndex = 6;
            // 
            // tbRegion
            // 
            this.tbRegion.Location = new System.Drawing.Point(29, 250);
            this.tbRegion.Name = "tbRegion";
            this.tbRegion.Size = new System.Drawing.Size(218, 20);
            this.tbRegion.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Введите район";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Введите месторасположение";
            // 
            // FormProductionSide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 356);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRegion);
            this.Controls.Add(this.tbAdminArea);
            this.Controls.Add(this.tbNamePoductionSide);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblForIdCompany);
            this.Controls.Add(this.lblForNameCompanyDO);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddProductionSide);
            this.Name = "FormProductionSide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Площадка";
            this.Load += new System.EventHandler(this.FormProductionSide_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddProductionSide;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblForNameCompanyDO;
        private System.Windows.Forms.Label lblForIdCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNamePoductionSide;
        private System.Windows.Forms.TextBox tbAdminArea;
        private System.Windows.Forms.TextBox tbRegion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}