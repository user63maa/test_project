using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eco.ADO;

namespace Eco.Forms.CompanyForms
{
    public partial class FormAddCompany : Form
    {
        public FormAddCompany()
        {
            InitializeComponent();
        }

        private void buttonAddCompanyDO_Click(object sender, EventArgs e)
        {
            string name= tbNameCompany.Text;
            string shortname = tbShortNameCompany.Text;
            if (name == "")
                MessageBox.Show("Введите название компании!");
            else
            {
                if (shortname == "")
                    MessageBox.Show("Введите короткое название компании!");
                else
                {
                    CompanyDODADO cmdDO = new CompanyDODADO();
                    cmdDO.Add(name,shortname);
                    this.Close();
                }
            }


        }

        private void FormAddCompany_Load(object sender, EventArgs e)
        {

        }
    }
}
