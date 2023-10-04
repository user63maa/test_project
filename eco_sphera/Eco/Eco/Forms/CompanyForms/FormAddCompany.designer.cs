
namespace Eco.Forms.CompanyForms
{
    partial class FormAddCompany
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
            this.buttonAddCompanyDO = new System.Windows.Forms.Button();
            this.tbNameCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbShortNameCompany = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonAddCompanyDO
            // 
            this.buttonAddCompanyDO.Location = new System.Drawing.Point(115, 123);
            this.buttonAddCompanyDO.Name = "buttonAddCompanyDO";
            this.buttonAddCompanyDO.Size = new System.Drawing.Size(70, 23);
            this.buttonAddCompanyDO.TabIndex = 0;
            this.buttonAddCompanyDO.Text = "Добавить";
            this.buttonAddCompanyDO.UseVisualStyleBackColor = true;
            this.buttonAddCompanyDO.Click += new System.EventHandler(this.buttonAddCompanyDO_Click);
            // 
            // tbNameCompany
            // 
            this.tbNameCompany.Location = new System.Drawing.Point(23, 33);
            this.tbNameCompany.Name = "tbNameCompany";
            this.tbNameCompany.Size = new System.Drawing.Size(244, 20);
            this.tbNameCompany.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите название дочерней компании";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите краткое название дочерней компании";
            // 
            // tbShortNameCompany
            // 
            this.tbShortNameCompany.Location = new System.Drawing.Point(23, 85);
            this.tbShortNameCompany.Name = "tbShortNameCompany";
            this.tbShortNameCompany.Size = new System.Drawing.Size(244, 20);
            this.tbShortNameCompany.TabIndex = 4;
            // 
            // FormAddCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.tbShortNameCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNameCompany);
            this.Controls.Add(this.buttonAddCompanyDO);
            this.Name = "FormAddCompany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление дочерней компании";
            this.Load += new System.EventHandler(this.FormAddCompany_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddCompanyDO;
        private System.Windows.Forms.TextBox tbNameCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbShortNameCompany;
    }
}