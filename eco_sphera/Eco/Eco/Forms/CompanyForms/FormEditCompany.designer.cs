
namespace Eco.Forms.CompanyForms
{
    partial class FormEditCompany
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
            this.tbShortNameCompany = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNameCompany = new System.Windows.Forms.TextBox();
            this.buttonEditCompanyDO = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbShortNameCompany
            // 
            this.tbShortNameCompany.Location = new System.Drawing.Point(22, 85);
            this.tbShortNameCompany.Name = "tbShortNameCompany";
            this.tbShortNameCompany.Size = new System.Drawing.Size(244, 20);
            this.tbShortNameCompany.TabIndex = 9;
            this.tbShortNameCompany.TextChanged += new System.EventHandler(this.tbShortNameCompany_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Введите краткое название дочерней компании";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Введите название дочерней компании";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbNameCompany
            // 
            this.tbNameCompany.Location = new System.Drawing.Point(22, 33);
            this.tbNameCompany.Name = "tbNameCompany";
            this.tbNameCompany.Size = new System.Drawing.Size(244, 20);
            this.tbNameCompany.TabIndex = 6;
            this.tbNameCompany.TextChanged += new System.EventHandler(this.tbNameCompany_TextChanged);
            // 
            // buttonEditCompanyDO
            // 
            this.buttonEditCompanyDO.Location = new System.Drawing.Point(107, 126);
            this.buttonEditCompanyDO.Name = "buttonEditCompanyDO";
            this.buttonEditCompanyDO.Size = new System.Drawing.Size(70, 23);
            this.buttonEditCompanyDO.TabIndex = 5;
            this.buttonEditCompanyDO.Text = "Изменить";
            this.buttonEditCompanyDO.UseVisualStyleBackColor = true;
            this.buttonEditCompanyDO.Click += new System.EventHandler(this.buttonEditCompanyDO_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(195, 39);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(0, 13);
            this.lblId.TabIndex = 10;
            this.lblId.Visible = false;
            this.lblId.Click += new System.EventHandler(this.lblId_Click);
            // 
            // FormEditCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 161);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.tbShortNameCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbNameCompany);
            this.Controls.Add(this.buttonEditCompanyDO);
            this.Name = "FormEditCompany";
            this.Text = "Редактирование компании";
            this.Load += new System.EventHandler(this.FormEditCompany_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbShortNameCompany;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNameCompany;
        private System.Windows.Forms.Button buttonEditCompanyDO;
        private System.Windows.Forms.Label lblId;
    }
}