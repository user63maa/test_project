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
using Eco.Model;
using System.Data.SqlClient;

namespace Eco.Forms.SourceFuelForms
{
    public partial class FormSourceFuel : Form
    {
        public FormSourceFuel()
        {
            InitializeComponent();
            CategoryFuelADO afADO = new CategoryFuelADO();
            List<CategoryOfFuel> listCategories = afADO.getAll();
            cbSourceFuel.DataSource = listCategories;
            cbSourceFuel.DisplayMember = "CategoryName";
            cbSourceFuel.ValueMember = "id";

            //foreach (CategoryOfFuel categ in listCategories)
            //{
            //    cbSourceFuel.Items.Add(categ.CategoryName);
            //}
        }


        private void FormCategoryOfFuel_Load(object sender, EventArgs e)
        {

        }

        private void cbTypeOfFuel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSourceFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSourceFuel.SelectedItem != null)
            {
                int selecteditemId = ((CategoryOfFuel)cbSourceFuel.SelectedItem).id;
                if (selecteditemId == 1 || selecteditemId == 2 || selecteditemId == 3 || selecteditemId == 8 || selecteditemId == 9)
                {
                    ViewTypeOfFuelADO viewTypeOfFuelADO = new ViewTypeOfFuelADO();
                    List<ViewTypeOfFuel> forcb = viewTypeOfFuelADO.getForSourceFromTypeOfFuel(selecteditemId);
                    cbTypeOfFuel.DataSource = forcb;
                    cbTypeOfFuel.DisplayMember = "Name";
                    cbTypeOfFuel.ValueMember = "id";
                }
                else
                {
                    switch (selecteditemId)
                    {
                        case 4:
                            ViewTypeOfFuelADO viewTypeOfFuelADO = new ViewTypeOfFuelADO();
                            List<ViewTypeOfFuel> forcb = viewTypeOfFuelADO.getForSourceFromTypeOfFuelForFlareCombustion();
                            cbTypeOfFuel.DataSource = forcb;
                            cbTypeOfFuel.DisplayMember = "Name";
                            cbTypeOfFuel.ValueMember = "id";
                            break;
                        case 5:
                            ViewTypeOfFuelADO viewTypeOfFuelADO2 = new ViewTypeOfFuelADO();
                            forcb = viewTypeOfFuelADO2.getForSourceFromTypeOfFuelForFugitivEmission();
                            cbTypeOfFuel.DataSource = forcb;
                            cbTypeOfFuel.DisplayMember = "Name";
                            cbTypeOfFuel.ValueMember = "id";
                            break;
                        case 10:
                            ViewTypeOfFuelADO viewTypeOfFuelADO3= new ViewTypeOfFuelADO();
                            forcb = viewTypeOfFuelADO3.getForSourceFromTypeOfFuelForTransport();
                            cbTypeOfFuel.DataSource = forcb;
                            cbTypeOfFuel.DisplayMember = "Name";
                            cbTypeOfFuel.ValueMember = "id";
                            break;
                        case 11:
                            ViewTypeOfFuelADO viewTypeOfFuelADO4 = new ViewTypeOfFuelADO();
                            forcb = viewTypeOfFuelADO4.getForSourceFromEnergySystem();
                            cbTypeOfFuel.DataSource = forcb;
                            cbTypeOfFuel.DisplayMember = "Name";
                            cbTypeOfFuel.ValueMember = "id";
                            break;                        

                    }
                }
            }

           
        }
        public void preAdd(int id)
        {
            lblSourceOfEmissionId.Text = id.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbSourceFuel.SelectedItem != null)
            {
                if (cbTypeOfFuel.SelectedItem != null)
                {
                    SourceOfEmissionFuelADO soefado = new SourceOfEmissionFuelADO();
                    int selectedCategorynId = ((CategoryOfFuel)cbSourceFuel.SelectedItem).id;
                    int selectedCategoryOfFuelnId = ((ViewTypeOfFuel)cbTypeOfFuel.SelectedItem).id;
                    string selectedTypeOfFuelName = ((ViewTypeOfFuel)cbTypeOfFuel.SelectedItem).Name;
                    int sourceEmissionId = int.Parse(lblSourceOfEmissionId.Text);
                    soefado.Add(sourceEmissionId, selectedCategorynId, selectedCategoryOfFuelnId,selectedTypeOfFuelName) ;
                    this.Close();

                }

            }

          
        }
    }
}
