
namespace Eco.Forms.SourceFuelForms
{
    partial class FormSourceFuel
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
            this.cbSourceFuel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTypeOfFuel = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lblSourceOfEmissionId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbSourceFuel
            // 
            this.cbSourceFuel.Location = new System.Drawing.Point(23, 51);
            this.cbSourceFuel.Name = "cbSourceFuel";
            this.cbSourceFuel.Size = new System.Drawing.Size(246, 21);
            this.cbSourceFuel.TabIndex = 0;
            this.cbSourceFuel.SelectedIndexChanged += new System.EventHandler(this.cbSourceFuel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите тип рассчёта";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выберите топливо";
            // 
            // cbTypeOfFuel
            // 
            this.cbTypeOfFuel.Location = new System.Drawing.Point(23, 133);
            this.cbTypeOfFuel.Name = "cbTypeOfFuel";
            this.cbTypeOfFuel.Size = new System.Drawing.Size(246, 21);
            this.cbTypeOfFuel.TabIndex = 3;
            this.cbTypeOfFuel.SelectedIndexChanged += new System.EventHandler(this.cbTypeOfFuel_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(72, 203);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Добавить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblSourceOfEmissionId
            // 
            this.lblSourceOfEmissionId.AutoSize = true;
            this.lblSourceOfEmissionId.Location = new System.Drawing.Point(209, 182);
            this.lblSourceOfEmissionId.Name = "lblSourceOfEmissionId";
            this.lblSourceOfEmissionId.Size = new System.Drawing.Size(0, 13);
            this.lblSourceOfEmissionId.TabIndex = 5;
            this.lblSourceOfEmissionId.Visible = false;
            // 
            // FormSourceFuel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 238);
            this.Controls.Add(this.lblSourceOfEmissionId);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbTypeOfFuel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSourceFuel);
            this.Name = "FormSourceFuel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Подсчёт выбросов";
            this.Load += new System.EventHandler(this.FormCategoryOfFuel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbSourceFuel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTypeOfFuel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblSourceOfEmissionId;
    }
}