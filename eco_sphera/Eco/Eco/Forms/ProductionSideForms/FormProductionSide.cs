using Eco.ADO;
using Eco.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eco.Forms.ProductionSideForms
{
    public partial class FormProductionSide : Form
    {
        bool edit = false;
        public FormProductionSide()
        {
            InitializeComponent();
        }
        public void FormProductionSideAdd(int id)
        {
            edit = false;
            label1.Visible = true;
            lblForNameCompanyDO.Visible = true;
            CompanyDODADO cmpADO = new CompanyDODADO();
            CompanyDO selctdComp = cmpADO.getObject(id);
            lblForNameCompanyDO.Text = selctdComp.Name;
            lblForIdCompany.Text = id.ToString();
        }

        public void FormProductionSideEdit(int id)
        {
            edit = true;
            ProductionSideADO psADO = new ProductionSideADO();
            ProductionSide selctdprodside = psADO.getObject(id);

            CompanyDODADO cmpADO = new CompanyDODADO();
            CompanyDO selctdComp = cmpADO.getObject(selctdprodside.CompanyDO_id);
            lblForNameCompanyDO.Text = selctdComp.Name;

            lblForIdCompany.Text = selctdprodside.id.ToString();
            tbNamePoductionSide.Text= selctdprodside.Name;
            tbAdminArea.Text = selctdprodside.AdministrativeArea;
            tbRegion.Text = selctdprodside.Region;
            buttonAddProductionSide.Text = "Изменить";
        }

        private void FormProductionSide_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddProductionSide_Click(object sender, EventArgs e)
        {
            if (!edit)
            {
                if (tbNamePoductionSide.Text == "" || tbAdminArea.Text == "" || tbRegion.Text == "")
                    MessageBox.Show("Проверьте введённые данные, они не могут быть пустыми");
                else
                {
                    ProductionSideADO psado = new ProductionSideADO();
                    psado.Add(int.Parse(lblForIdCompany.Text), tbNamePoductionSide.Text, tbRegion.Text, tbAdminArea.Text);
                    this.Close();
                }
            }
            else
            {

                if (tbNamePoductionSide.Text == "" || tbAdminArea.Text == "" || tbRegion.Text == "")
                    MessageBox.Show("Проверьте введённые данные, они не могут быть пустыми");
                else
                {
                    ProductionSideADO psado = new ProductionSideADO();
                    psado.Edit(int.Parse(lblForIdCompany.Text), tbNamePoductionSide.Text, tbRegion.Text, tbAdminArea.Text);
                    this.Close();
                }
            }
        }
    }
}
