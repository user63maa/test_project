
namespace Eco.Forms.NewSourceForms
{
    partial class FormNewSource
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbNameSource = new System.Windows.Forms.TextBox();
            this.tbCodeSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btAddSource = new System.Windows.Forms.Button();
            this.lblIdProdSite = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите название источника";
            // 
            // tbNameSource
            // 
            this.tbNameSource.Location = new System.Drawing.Point(35, 47);
            this.tbNameSource.Name = "tbNameSource";
            this.tbNameSource.Size = new System.Drawing.Size(194, 20);
            this.tbNameSource.TabIndex = 1;
            // 
            // tbCodeSource
            // 
            this.tbCodeSource.Location = new System.Drawing.Point(35, 109);
            this.tbCodeSource.Name = "tbCodeSource";
            this.tbCodeSource.Size = new System.Drawing.Size(194, 20);
            this.tbCodeSource.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите код источника";
            // 
            // btAddSource
            // 
            this.btAddSource.Location = new System.Drawing.Point(95, 148);
            this.btAddSource.Name = "btAddSource";
            this.btAddSource.Size = new System.Drawing.Size(75, 23);
            this.btAddSource.TabIndex = 4;
            this.btAddSource.Text = "Добавить";
            this.btAddSource.UseVisualStyleBackColor = true;
            this.btAddSource.Click += new System.EventHandler(this.btAddSource_Click);
            // 
            // lblIdProdSite
            // 
            this.lblIdProdSite.AutoSize = true;
            this.lblIdProdSite.Location = new System.Drawing.Point(217, 153);
            this.lblIdProdSite.Name = "lblIdProdSite";
            this.lblIdProdSite.Size = new System.Drawing.Size(0, 13);
            this.lblIdProdSite.TabIndex = 5;
            this.lblIdProdSite.Visible = false;
            // 
            // FormNewSource
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 183);
            this.Controls.Add(this.lblIdProdSite);
            this.Controls.Add(this.btAddSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCodeSource);
            this.Controls.Add(this.tbNameSource);
            this.Controls.Add(this.label1);
            this.Name = "FormNewSource";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Источник";
            this.Load += new System.EventHandler(this.FormNewSource_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNameSource;
        private System.Windows.Forms.TextBox tbCodeSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btAddSource;
        private System.Windows.Forms.Label lblIdProdSite;
    }
}