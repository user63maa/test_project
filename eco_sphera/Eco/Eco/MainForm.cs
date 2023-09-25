using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eco.Model;
using Eco.Forms.CompanyForms;
using Eco.Forms.ProductionSideForms;
using Eco.ADO;
using Eco.Forms.NewSourceForms;
using Eco.Forms.SourceFuelForms;

namespace Eco
{
    public partial class MainForm : Form
    {
        Calculations calculator = new Calculations();
        CalculationResultADO calcADO = new CalculationResultADO();

        public SqlConnection connection;
        public MainForm()
        {
            InitializeComponent();           
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
            ViewTreeNodes();
            fillDefaults();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        public void ViewTreeNodes()
        {
            CompanyDODADO cmpADO = new CompanyDODADO();
            List<CompanyDO> listCompamyDO = cmpADO.getAll();

            ProductionSideADO psADO = new ProductionSideADO();
            List<ProductionSide> listProdSites =psADO.getAll();

            SourceOfEmissionADO somADO = new SourceOfEmissionADO();
            List<SourceOfEmission> listSource =somADO.getAll();

            connection.Open();

            string sourceFueQuery = @"SELECT sef.id as id, sef.SourceOfEmission_id as SourceOfEmission_id,CategoryName as categoryName, TypeOfFuelName
            FROM SourceOfEmission se JOIN SourceOfEmissionFuel sef ON se.id = sef.SourceOfEmission_id
            JOIN CategoryOfFuel cf ON cf.id = sef.CategoryOfFuel_id
            WHERE isDeleted = 0
            ";
            SqlCommand cmdSourceFuel = new SqlCommand(sourceFueQuery, connection);
            List<SourceOfEmissionFuel> listSourceFuel = new List<SourceOfEmissionFuel>();
            SqlDataReader SourceFuelReader = cmdSourceFuel.ExecuteReader();

            while (SourceFuelReader.Read())
            {
                SourceOfEmissionFuel ps = new SourceOfEmissionFuel();
                ps.id = (int)SourceFuelReader["id"];
                ps.SourceOfEmission_id = (int)SourceFuelReader["SourceOfEmission_id"];
                ps.CategoryName = (string)SourceFuelReader["categoryName"];
                ps.TypeOfFuelName = SourceFuelReader["TypeOfFuelName"] == DBNull.Value ? null: (string)SourceFuelReader["TypeOfFuelName"];
                listSourceFuel.Add(ps);
            }
            SourceFuelReader.Close();
            connection.Close();

            foreach (CompanyDO company in listCompamyDO)
            {
                TreeNode companyDOnode = new TreeNode(company.Name);
                companyDOnode.Tag = company.id;
                foreach (ProductionSide production in listProdSites)
                {
                    if (production.CompanyDO_id == company.id)
                    {
                        TreeNode poductionSideNode = new TreeNode(production.Name);
                        poductionSideNode.Tag = production.id;

                        foreach (SourceOfEmission source in listSource)
                        {
                            if (source.ProductionSite_id == production.id)
                            {
                                TreeNode sourceSideNode = new TreeNode(source.Name);
                                sourceSideNode.Tag = source.id;
                                foreach (SourceOfEmissionFuel sourcefuel in listSourceFuel)
                                {
                                    if (sourcefuel.SourceOfEmission_id == source.id)
                                    {
                                        TreeNode sourceFuelNode = new TreeNode(sourcefuel.CategoryName+" ("+ sourcefuel.TypeOfFuelName + ")");
                                        sourceFuelNode.Tag = sourcefuel.id;

                                        sourceSideNode.Nodes.Add(sourceFuelNode);
                                    }
                                }
                                poductionSideNode.Nodes.Add(sourceSideNode);
                            }
                        }
                        companyDOnode.Nodes.Add(poductionSideNode);
                    }
                }
                treeView1.Nodes.Add(companyDOnode);

            }
            
        }

        public void fillDefaults()
        {
            var coefficientDB = new TypeOfFuelWithCoefADO();
            var conditions = coefficientDB.getMeasurementConditions(3);
            CO2DensityGasTB.Text = conditions.CO2Density.ToString();
            //FluidCO2DensityTB.Text = conditions.CO2Density.ToString();
            FlareCO2DensityTB.Text = conditions.CO2Density.ToString();
            FugitiveCO2DensityTB.Text = conditions.CO2Density.ToString();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonAddCompanyDO, "Создание дочерней компании");
            t.SetToolTip(btRefresh, "Обновить дерево");
        }

        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = treeView1.GetNodeAt(e.X, e.Y);

            if (e.Button == MouseButtons.Right)
            {
                if (clickedNode != null && clickedNode.Level == 1)
                {
                    cmsForProductiondSide.Show(treeView1, e.Location);
                }
            //}
            //if (e.Button == MouseButtons.Right)
            //{
                if (clickedNode != null && clickedNode.Level == 0)
                {
                    cmsForCompanyDO.Show(treeView1, e.Location);
                }
            //}
            //if (e.Button == MouseButtons.Right)
            //{
                if (clickedNode != null && clickedNode.Level == 2)
                {
                    cmsForSourceOFEmission.Show(treeView1, e.Location);
                }
            }
        }

        private void buttonAddCompanyDO_MouseClick(object sender, MouseEventArgs e)
        {
            FormAddCompany frmaddcmp = new FormAddCompany();
            frmaddcmp.Show();
        }

        private void tsmDeleteCompany_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            CompanyDODADO cmADO = new CompanyDODADO();
            cmADO.Delete((int)selNode.Tag);
        }

        private void tsmEditCompany_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                FormEditCompany frmeditcmp = new FormEditCompany((int)selectedNode.Tag);
                frmeditcmp.Show();
            }

        }

        private void cmsForCompanyDO_Opening(object sender, CancelEventArgs e)
        {

        }

        private void editProductionSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                FormProductionSide frmps = new FormProductionSide();
                frmps.FormProductionSideEdit((int)selectedNode.Tag);
                frmps.Show();
            }
        }

        private void delProductionSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                ProductionSideADO psADO = new ProductionSideADO();
                psADO.Delete((int)selectedNode.Tag);
            }
        }

        private void tsmAddProsuctionSide_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                FormProductionSide frmps = new FormProductionSide();
                frmps.FormProductionSideAdd((int)selectedNode.Tag);
                frmps.Show();
            }
        }

        private void cmsForProductiondSide_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addProductionSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            FormNewSource fns = new FormNewSource();
            fns.preAdd((int)selNode.Tag);
            fns.Show();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            ViewTreeNodes();
        }

        private void buttonAddCompanyDO_MouseClick(object sender, EventArgs e)
        {

        }

        private void addSourceFuelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TreeNode selNode = treeView1.SelectedNode;
            FormSourceFuel fsf = new FormSourceFuel();
            fsf.preAdd((int)selNode.Tag);
            fsf.Show();
        }

        private void editSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void delSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            var coefficientDB = new TypeOfFuelWithCoefADO();
            CalculationResultADO calresADO = new CalculationResultADO();
            SourceOfEmissionADO soeADO = new SourceOfEmissionADO();

            if (selectedNode.Level == 3)
            {
                
                switch (soeADO.getType((int)selectedNode.Tag))
                {
                    case 1:
                        tabControl1.SelectedIndex = 0;
                       
                            gasesUsageTB.Text = "";
                            mixPercent1.Text = "";
                            mixPercent2.Text = "";
                            mixPercent3.Text = "";
                            mixPercent4.Text = "";
                            mixPercent5.Text = "";
                            mixPercent6.Text = "";
                            mixPercent7.Text = "";
                            mixPercent8.Text = "";
                            mixPercent9.Text = "";
                            mixPercent10.Text = "";
                            gasesEditCB.Checked = false;
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        break;
                    case 2:
                        tabControl1.SelectedIndex = 1;
                        
                            fluidUsageTB.Text = "";
                            fluidLowerHeatTB.Text = "";
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                        dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
                        fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text) * Double.Parse(dictLowerHeatTB.Text) * 0.001 : 0).ToString();


                        break;
                    case 4:
                        tabControl1.SelectedIndex = 2;
                        
                            flareGasesEditCB.Checked = false;
                            fuelUsageFlareTB.Text = "";
                            combustionFlareCB.SelectedIndex = 0;
                            flareTB1.Text = "";
                            flareTB2.Text = "";
                            flareTB3.Text = "";
                            flareTB4.Text = "";
                            flareTB5.Text = "";
                            flareTB6.Text = "";
                            flareTB7.Text = "";
                            flareTB8.Text = "";
                            flareTB9.Text = "";
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        break;
                    case 5:
                        tabControl1.SelectedIndex = 4;
                        
                            gasUsageFugitiveTB.Text = "";
                            CH4ShareTB.Text = "";
                            CO2ShareTB.Text = "";
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        break;
                    case 10:
                        tabControl1.SelectedIndex = 4;
                        
                            lUsageTransportTB.Text = "";
                            tUsageTransportTB.Text = "";
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        break;
                    case 11:
                        
                            electroUsageTB.Text = "";
                            heatUsageTB.Text = "";
                        if (!calresADO.HaveResult((int)selectedNode.Tag))
                        {
                        }
                        tabControl1.SelectedIndex = 5;
                        break;
                }

                var calcResId = soeADO.HaveCalculationResult((int)selectedNode.Tag);
                if (calcResId == 0)
                {
                    //MessageBox.Show("Нет сохранённых вычислений(отобразить форму рассчёта)");
                }
                else
                {
                    MessageBox.Show("Отобразить форму результата рассчёта)");
                }
            }
            else
            {
                tabControl1.SelectedIndex = 6;
                if (selectedNode.Level == 2 || selectedNode.Level == 1 || selectedNode.Level == 0)
                {
                    createStatistics();
                }
            }
            }

        private void co2densitylbl1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            int idSelectedSourceEmissionFuel = (int)selectedNode.Tag;
            Gases gases;
            if (!gasesEditCB.Checked)
            {
                gases = new Gases();
            }
            else
            {
                gases = new Gases(
                    Double.Parse(mixPercent1.Text.Replace('.', ',')),
                    Double.Parse(mixPercent2.Text.Replace('.', ',')),
                    Double.Parse(mixPercent3.Text.Replace('.', ',')),
                    Double.Parse(mixPercent4.Text.Replace('.', ',')),
                    Double.Parse(mixPercent5.Text.Replace('.', ',')),
                    Double.Parse(mixPercent6.Text.Replace('.', ',')),
                    Double.Parse(mixPercent7.Text.Replace('.', ',')),
                    Double.Parse(mixPercent8.Text.Replace('.', ',')),
                    Double.Parse(mixPercent9.Text.Replace('.', ',')),
                    Double.Parse(mixPercent10.Text.Replace('.', ',')));
            }
            var emissions = calculator.GasFuel(getFuelId(), gases, gasesUsageTB.Text.Length > 0 ? Int32.Parse(gasesUsageTB.Text) : 0);
            emissionsResult1.Text = emissions.ToString();
            if (checkBox1.Checked)
            {
                CalculationResultADO calcADO = new CalculationResultADO();
                int addCalcID = addResult(emissionsResult1.Text);
                if (addCalcID != 0)
                {
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, gasesUsageTB.Name, gasesUsageTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, CO2DensityGasTB.Name, CO2DensityGasTB.Text);
                    

                    if (gasesEditCB.Checked)
                    {
                        calcControls = calcADO.addToListForResForm(calcControls, gasesEditCB.Name, "1");

                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent1.Name, mixPercent1.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent2.Name, mixPercent2.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent3.Name, mixPercent3.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent4.Name, mixPercent4.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent5.Name, mixPercent5.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent6.Name, mixPercent6.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent7.Name, mixPercent7.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent8.Name, mixPercent8.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent9.Name, mixPercent9.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, mixPercent10.Name, mixPercent10.Text);
                    }
                    else
                        calcControls = calcADO.addToListForResForm(calcControls, gasesEditCB.Name, "0");
                    calcADO.CalculationCompositionForm(calcControls, addCalcID);


                    List<CalculationCompositionForExport> calcForReport = new List<CalculationCompositionForExport>();                    
                     
                    SourceOfEmissionFuelADO soef = new SourceOfEmissionFuelADO();
                    int SourceOfEmissionId = (soef.getObject(idSelectedSourceEmissionFuel)).SourceOfEmission_id;
                    int FuelifFromTable = (soef.getObject(idSelectedSourceEmissionFuel)).TypeOfFuelTable_id;

                    CategoryFuelADO caf = new CategoryFuelADO();
                    string fuelName = (soef.getObject(idSelectedSourceEmissionFuel)).TypeOfFuelName;
                    SourceOfEmissionADO soeADO = new SourceOfEmissionADO();
                    string SourceName = (soeADO.getObject(SourceOfEmissionId)).Name;
                    int SiteId = soeADO.getObject(SourceOfEmissionId).ProductionSite_id;
                    ProductionSideADO psADO = new ProductionSideADO();
                    string siteAdminArea = psADO.getObject(SiteId).AdministrativeArea;
                    string siteRegion = psADO.getObject(SiteId).Region;
                    string siteName = psADO.getObject(SiteId).Name;

                    CalculationResultADO csADO = new CalculationResultADO();
                    calcForReport= csADO.addToListForResForExport(calcForReport,siteRegion,9,4);
                    calcForReport = csADO.addToListForResForExport(calcForReport, siteAdminArea, 9, 5);
                    calcForReport = csADO.addToListForResForExport(calcForReport, siteName, 9, 6);
                    calcForReport = csADO.addToListForResForExport(calcForReport, SourceName, 15, 7);
                    calcForReport = csADO.addToListForResForExport(calcForReport, fuelName, 9, 8);

                    calcForReport = csADO.addToListForResForExport(calcForReport, gasesUsageTB.Text, 22, 10);
                    double koefVibros = double.Parse(emissionsResult1.Text)/double.Parse(gasesUsageTB.Text);
                    calcForReport = csADO.addToListForResForExport(calcForReport, koefVibros.ToString(), 20, 12);
                    calcForReport = csADO.addToListForResForExport(calcForReport, emissionsResult1.Text, 21, 12);
                    calcForReport = csADO.addToListForResForExport(calcForReport, emissionsResult1.Text, 22, 12);
                    

                    if (gasesEditCB.Checked)
                    {
                        calcForReport = csADO.addToListForResForExport(calcForReport, "Edit",1 , 1);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent1.Text, 9, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent2.Text, 10, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent3.Text, 11, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent4.Text, 12, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent5.Text, 13, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent6.Text, 14, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent7.Text, 15, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent8.Text, 16, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent9.Text, 17, 10);
                        calcForReport = csADO.addToListForResForExport(calcForReport, mixPercent10.Text, 18, 10);
                    }
                    else
                    {
                        calcForReport = csADO.addToListForResForExport(calcForReport, "NotEdit", 1, 1);
                    }
                        csADO.CalculationCompositionToReport(calcForReport,addCalcID);

                }
            }
        }

        private void calculateFluidBTN_Click(object sender, EventArgs e)
        {
            double usage = fluidUsageTB.Text.Length != 0 ? Double.Parse(fluidUsageTB.Text) : 0;
            double lowerHeat = fluidLowerHeatTB.Text.Length != 0 ? Double.Parse(fluidLowerHeatTB.Text) : 0;

            var emissions = calculator.FluidFuel(getFuelId(), usage, lowerHeat);
            emissionsResult2.Text = emissions.ToString();
            if (checkBox2.Checked) addResult(emissionsResult2.Text);
        }

        private void calculateFlareBTN_Click(object sender, EventArgs e)
        {
            double usage = fuelUsageFlareTB.Text.Length != 0 ? Double.Parse(fuelUsageFlareTB.Text) : 0;

            FlareGases gases;
            if (!flareGasesEditCB.Checked)
            {
                gases = new FlareGases();
            }
            else
            {
                gases = new FlareGases(
                    Double.Parse(flareTB1.Text.Replace('.', ',')),
                    Double.Parse(flareTB2.Text.Replace('.', ',')),
                    Double.Parse(flareTB3.Text.Replace('.', ',')),
                    Double.Parse(flareTB4.Text.Replace('.', ',')),
                    Double.Parse(flareTB5.Text.Replace('.', ',')),
                    Double.Parse(flareTB6.Text.Replace('.', ',')),
                    Double.Parse(flareTB7.Text.Replace('.', ',')),
                    Double.Parse(flareTB8.Text.Replace('.', ',')),
                    Double.Parse(flareTB9.Text.Replace('.', ',')));
            }

            var emissions = calculator.FlareCombustion(getFuelId(), combustionFlareCB.SelectedIndex+1, gases, usage);
            emissionsResult3.Text = emissions.ToString();
            if (checkBox3.Checked) { 
                addResult(emissionsResult3.Text); 

            }
        }

        private void calculateFugitiveBTN_Click(object sender, EventArgs e)
        {
            double usage = gasUsageFugitiveTB.Text.Length != 0 ? Double.Parse(gasUsageFugitiveTB.Text) : 0;
            double CH4Share = CH4ShareTB.Text.Length != 0 ? Double.Parse(CH4ShareTB.Text) : 0;
            double CO2Share = CO2ShareTB.Text.Length != 0 ? Double.Parse(CO2ShareTB.Text) : 0;
            var emissions = calculator.FugitiveEmissions(getFuelId(), usage, CH4Share, CO2Share);
            emissionsResult4.Text = emissions.ToString();
            if (cbSaveFugitive.Checked) addResult(emissionsResult4.Text);
        }

        private void calculateTransportBTN_Click(object sender, EventArgs e)
        {
            double tonns = tUsageTransportTB.Text.Length != 0 ? Double.Parse(tUsageTransportTB.Text) : 0;
            double liters = lUsageTransportTB.Text.Length != 0 ? Double.Parse(lUsageTransportTB.Text) : 0;
            var emissions = calculator.Transport(getFuelId(), tonns, liters);
            emissionsResult5.Text = emissions.ToString();
            if (cbSaveResultTransport.Checked) addResult(emissionsResult5.Text);
        }

        private void calculateIndirectBTN_Click(object sender, EventArgs e)
        {
            double electro = electroUsageTB.Text.Length != 0 ? Double.Parse(electroUsageTB.Text) : 0;
            double heat = heatUsageTB.Text.Length != 0 ? Double.Parse(heatUsageTB.Text) : 0;
            var emissions = calculator.IndirectWorks(getFuelId(), getFuelId(), electro, heat);
            emissionsResult6.Text = emissions.ToString();
            if (cbSaveResultKocVibros.Checked) addResult(emissionsResult6.Text);
        }

        public int getFuelId()
        {
            int res = 0;
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode.Level == 3)
            {
                CategoryFuelADO cofADO = new CategoryFuelADO();
                res = cofADO.getFuel((int)selectedNode.Tag);
            }
            return res;
        }

        private void flareGasesEditCB_CheckedChanged(object sender, EventArgs e)
        {
            flareGasesPanel.Enabled = flareGasesEditCB.Checked;
        }

        private void gasesEditCB_CheckedChanged(object sender, EventArgs e)
        {
            gasesPanel.Enabled = gasesEditCB.Checked;
        }

        private void gasesChanged(object sender, EventArgs e)
        {
            totalGases.BackColor = Color.White;
            var sum =
                    (mixPercent1.Text.Length > 0 ? Double.Parse(mixPercent1.Text.Replace('.', ',')) : 0) +
                    (mixPercent2.Text.Length > 0 ? Double.Parse(mixPercent2.Text.Replace('.', ',')) : 0) +
                    (mixPercent3.Text.Length > 0 ? Double.Parse(mixPercent3.Text.Replace('.', ',')) : 0) +
                    (mixPercent4.Text.Length > 0 ? Double.Parse(mixPercent4.Text.Replace('.', ',')) : 0) +
                    (mixPercent5.Text.Length > 0 ? Double.Parse(mixPercent5.Text.Replace('.', ',')) : 0) +
                    (mixPercent6.Text.Length > 0 ? Double.Parse(mixPercent6.Text.Replace('.', ',')) : 0) +
                    (mixPercent7.Text.Length > 0 ? Double.Parse(mixPercent7.Text.Replace('.', ',')) : 0) +
                    (mixPercent8.Text.Length > 0 ? Double.Parse(mixPercent8.Text.Replace('.', ',')) : 0) +
                    (mixPercent9.Text.Length > 0 ? Double.Parse(mixPercent9.Text.Replace('.', ',')) : 0) +
                    (mixPercent10.Text.Length > 0 ? Double.Parse(mixPercent10.Text.Replace('.', ',')) : 0);
            totalGases.Text = sum.ToString();
            if (sum > 100) totalGases.BackColor = Color.Red;
        }

        private void fluidLowerHeatTB_TextChanged(object sender, EventArgs e)
        {
            if (fluidLowerHeatTB.Text.Length == 0)
            {
                TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
            } else
            {
                dictLowerHeatTB.Text = (Double.Parse(fluidLowerHeatTB.Text) * 0.0041868).ToString();
            }
            if (fluidUsageTB.Text.Length == 0)
            {
                TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text) * Double.Parse(dictLowerHeatTB.Text) * 0.001 : 0).ToString();
            }
            else
            {
                fluidUsageTJTB.Text = (Double.Parse(fluidUsageTB.Text) * Double.Parse(dictLowerHeatTB.Text) * 0.001).ToString();
            }
        }

        private void fluidUsageTB_TextChanged(object sender, EventArgs e)
        {
            if (fluidLowerHeatTB.Text.Length == 0)
            {
                TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
            }
            else
            {
                dictLowerHeatTB.Text = (Double.Parse(fluidLowerHeatTB.Text) * 0.0041868).ToString();
            }
            if (fluidUsageTB.Text.Length == 0)
            {
                TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text) * Double.Parse(dictLowerHeatTB.Text) * 0.001 : 0).ToString();
            }
            else
            {
                fluidUsageTJTB.Text = (Double.Parse(fluidUsageTB.Text) * Double.Parse(dictLowerHeatTB.Text) * 0.001).ToString();
            }
        }

        public void createStatistics()
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            var calcresDB = new CalculationResultADO();
            if(selectedNode.Level == 2)
            {
                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 2);
                foreach(var item in results)
                {
                    item.percentage = item.Result / results.Select(d => d.Result).Sum() * 100;
                }
                var datasource = results.OrderByDescending(d => d.percentage).ToList();
                datasource.Add(new CalcResult { Category = "Всего:", Result = results.Select(d => d.Result).Sum(), percentage = results.Select(d => d.percentage).Sum() });
                statsDatagrid.DataSource = datasource;
                statsDatagrid.Columns[0].Visible = false;
                statsDatagrid.Columns[1].Visible = false;
                statsDatagrid.Columns[5].DefaultCellStyle.Format = "N2";
                statsDatagrid.Columns[6].DefaultCellStyle.Format = "N2";
            }
            if (selectedNode.Level == 1)
            {
                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 1);
                foreach (var item in results)
                {
                    item.percentage = item.Result / results.Select(d => d.Result).Sum() * 100;
                }
                var datasource = results.OrderByDescending(d => d.percentage).ToList();
                datasource.Add(new CalcResult { Category = "Всего:", Result = results.Select(d => d.Result).Sum(), percentage = results.Select(d => d.percentage).Sum() });
                statsDatagrid.DataSource = datasource;
                statsDatagrid.Columns[0].Visible = false;
                statsDatagrid.Columns[1].Visible = true;
                statsDatagrid.Columns[5].DefaultCellStyle.Format = "N2";
                statsDatagrid.Columns[6].DefaultCellStyle.Format = "N2";
            }
            if (selectedNode.Level == 0)
            {
                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 0);
                foreach (var item in results)
                {
                    item.percentage = item.Result / results.Select(d => d.Result).Sum() * 100;
                }
                var datasource = results.OrderByDescending(d => d.percentage).ToList();
                datasource.Add(new CalcResult { Category = "Всего:", Result = results.Select(d => d.Result).Sum(), percentage = results.Select(d => d.percentage).Sum() });
                statsDatagrid.DataSource = datasource;
                statsDatagrid.Columns[0].Visible = true;
                statsDatagrid.Columns[1].Visible = true;
                statsDatagrid.Columns[5].DefaultCellStyle.Format = "N2";
                statsDatagrid.Columns[6].DefaultCellStyle.Format = "N2";
            }
        }

        public int addResult(string text)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            System.Security.Principal.WindowsIdentity user = System.Security.Principal.WindowsIdentity.GetCurrent();
            string userLogin = user.Name;
            int addCalcID = calcADO.Add((int)selectedNode.Tag, float.Parse(text), userLogin);
            return addCalcID;
        }

        private void btToExcel_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            int selectTypeOfSourceFuelId = (int)selectedNode.Tag;
            if (selectTypeOfSourceFuelId != 0)
            {
                ExcelWork ew = new ExcelWork();
                ew.getFileFromResult(selectTypeOfSourceFuelId);
            }
        }
    }
}
