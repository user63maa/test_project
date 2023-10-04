using Eco.ADO;
using Eco.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Eco.Forms.CompanyForms
{
    public partial class FormEditCompany : Form
    {
        public TreeNode selectedNode { get; set; }
        public FormEditCompany()
        {
            InitializeComponent();
        }
        public FormEditCompany(int id)
        {
            InitializeComponent();
            CompanyDODADO cmpADO = new CompanyDODADO();
            CompanyDO selctdComp = cmpADO.getObject(id);
            tbNameCompany.Text = selctdComp.Name;
            tbShortNameCompany.Text = selctdComp.ShortName;
            lblId.Text = id.ToString();
        }


        private void FormEditCompany_Load(object sender, EventArgs e)
        {

        }


        private void buttonEditCompanyDO_Click(object sender, EventArgs e)
        {
            if (tbNameCompany.Text == "")
                MessageBox.Show("Введите название компании");
            else
            {
                if(tbShortNameCompany.Text == "")
                    MessageBox.Show("Введите короткое название компании");
                else
                {
                    CompanyDODADO cmpADO = new CompanyDODADO();
                    cmpADO.Edit(int.Parse(lblId.Text), tbNameCompany.Text, tbShortNameCompany.Text);
                    selectedNode.Text = tbNameCompany.Text;
                    this.Close();                   

                }
            }
        }

        private void tbShortNameCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbNameCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }
    }
}
