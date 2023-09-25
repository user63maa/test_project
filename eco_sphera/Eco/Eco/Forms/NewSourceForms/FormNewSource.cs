using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Eco.ADO;
using Eco.Model;

namespace Eco.Forms.NewSourceForms
{
    public partial class FormNewSource : Form
    {
        public FormNewSource()
        {
            InitializeComponent();
            //CategoryFuelADO afADO = new CategoryFuelADO();
            //List<CategoryOfFuel> listCategories = afADO.getAll();

            //cbCategoryOfFuel.Items.Add("Выбор типа топлива");
            //foreach (CategoryOfFuel categ in listCategories )
            //{
            //    cbCategoryOfFuel.Items.Add(categ.CategoryName);
            //}
        }

        private void FormNewSource_Load(object sender, EventArgs e)
        {

        }

        private void cbCategoryOfFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*MessageBox.Show(cbCategoryOfFuel.SelectedItem.ToString());*/
        }

        public void preAdd(int id)
        {
            lblIdProdSite.Text = id.ToString();

        }
        private void btAddSource_Click(object sender, EventArgs e)
        {            
            if (tbCodeSource.Text == "")
                MessageBox.Show("Заполните код источника");
            else {
                if (tbNameSource.Text == "") {
                    MessageBox.Show("Заполните название источника");
                }
                else
                {
                    SourceOfEmissionADO soeADO = new SourceOfEmissionADO();
                    soeADO.Add(int.Parse(lblIdProdSite.Text), tbNameSource.Text, tbCodeSource.Text);
                    
                    this.Close();
                }
            }
            CategoryFuelADO afADO = new CategoryFuelADO();
        }
    }
}
