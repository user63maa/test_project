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
        int selectIndexTabPage = 0;

        public SqlConnection connection;
        public MainForm()
        {
            InitializeComponent();           
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["EcologyConnectionString"].ConnectionString);
            ViewTreeNodes();
            fillDefaults();
            CalculationResultADO c = new CalculationResultADO();
            DOObjectForReport xev =  c.GetDataForReport(11, 0);
           
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
            WHERE se.isDeleted = 0 AND sef.isDeleted = 0";
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

        public void fillWithData(int id)
        {
            var dbw = new CalculationResultADO();
            var values = dbw.getDataForFields(id);

            foreach(var item in values)
            {
                var control = tabControl1.Controls.Find(item.Name, true).FirstOrDefault();
                control.Text = item.Value;
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(buttonAddCompanyDO, "Создание дочерней компании");
            t.SetToolTip(btToExcel, "Выгрузить в Excel");

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

                if (clickedNode != null && clickedNode.Level == 3)
                {
                    cmsForSourceFuel.Show(treeView1, e.Location);
                }
            }
        }

       


        private void tsmDeleteCompany_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            CompanyDODADO cmADO = new CompanyDODADO();
            if (cmADO.Delete((int)selNode.Tag))
                treeView1.Nodes.Remove(selNode);
        }

        private void tsmEditCompany_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                using (FormEditCompany frmeditcmp = new FormEditCompany((int)selectedNode.Tag))
                {
                    frmeditcmp.selectedNode = selectedNode;
                    frmeditcmp.ShowDialog();
                }

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
                using (FormProductionSide frmps = new FormProductionSide())
                {
                    frmps.FormProductionSideEdit((int)selectedNode.Tag);
                    frmps.selectnode = selectedNode;
                    frmps.ShowDialog();
                    
                }
                   
            }
        }

        private void delProductionSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
               ProductionSideADO psADO = new ProductionSideADO();
               if(psADO.Delete((int)selectedNode.Tag))
                selectedNode.Parent.Nodes.Remove(selectedNode);
            }
        }

        private void tsmAddProsuctionSide_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                using (FormProductionSide frmps = new FormProductionSide())
                {
                    frmps.selectnode = selectedNode;
                    frmps.FormProductionSideAdd((int)selectedNode.Tag);
                    frmps.ShowDialog();
                }
                selectedNode.Expand();
            }
        }

        private void cmsForProductiondSide_Opening(object sender, CancelEventArgs e)
        {

        }

        private void addProductionSideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            using (FormNewSource fns = new FormNewSource())
            {
                fns.selectNode = selNode;
                fns.preAdd((int)selNode.Tag);
                fns.ShowDialog();
            }

            selNode.Expand();
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            ViewTreeNodes();
        }



        private void addSourceFuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selNode = treeView1.SelectedNode;
            using (FormSourceFuel fsf = new FormSourceFuel())
            {
                fsf.selectNode=selNode;
                fsf.preAdd((int)selNode.Tag);
                fsf.ShowDialog();
            }
            selNode.Expand();
        }

        private void editSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                using (FormNewSource frmsrc = new FormNewSource())
                {
                    frmsrc.preEdit((int)selectedNode.Tag);
                    frmsrc.selectNode = selectedNode;
                    frmsrc.ShowDialog();
                }

            }

        }

        private void delSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                SourceOfEmissionADO soeADO = new SourceOfEmissionADO();
                if (soeADO.Delete((int)selectedNode.Tag))
                    selectedNode.Parent.Nodes.Remove(selectedNode);
            }

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;

            var coefficientDB = new TypeOfFuelWithCoefADO();
            CalculationResultADO calresADO = new CalculationResultADO();
            SourceOfEmissionADO soeADO = new SourceOfEmissionADO();

            lblLastSave.Visible = false;
            lblWhoLastSave.Visible = false;

            if (selectedNode.Level == 3)
            {
                btToExcel.Visible = false;
                disableAllPages();
                lblFullPath.Text = GetNodeFullPath(selectedNode);
                switch (soeADO.getType((int)selectedNode.Tag))
                {
                    case 1:
                        selectIndexTabPage = 0;
                        tabControl1.SelectedIndex = 0;
                        
                        co2densitylbl1.Enabled = true;
                        checkBox1.Checked = false;
                        emissionsResult1.Text = "";

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
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);

                            if (gasesEditCB.Text=="0")
                            {
                                gasesEditCB.Text = " Редактировать состав газов";
                                gasesEditCB.Checked = false;
                            }
                            else
                            {
                                gasesEditCB.Text = " Редактировать состав газов";
                                gasesEditCB.Checked = true;
                            }
                        }
                        break;
                    case 2:                        
                        tabPage2.Enabled = true;
                        selectIndexTabPage = 1;
                        tabControl1.SelectedIndex = 1;
                        
                        checkBox2.Checked = false;
                        emissionsResult2.Text = "";

                        fluidUsageTB.Text = "";
                            fluidLowerHeatTB.Text = "";
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);
                        }
                        var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                        dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
                        fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text.Replace('.', ',')) * Double.Parse(dictLowerHeatTB.Text.Replace('.', ',')) * 0.001 : 0).ToString();
                        tbCoefEmisson.Text = coeff.EmissionFactor2.ToString(); 

                        break;
                    case 4:
                        selectIndexTabPage = 2;
                        tabControl1.SelectedIndex = 2;
                        tabPage3.Enabled = true;
                        checkBox3.Checked = false;
                        emissionsResult3.Text = "";

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
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);
                            combustionFlareCB.SelectedIndex = int.Parse(combustionFlareCB.Text);
                            if (flareGasesEditCB.Text == "0")
                            {
                                flareGasesEditCB.Text = " Редактировать состав газов";
                                flareGasesEditCB.Checked = false;
                            }
                            else
                            {
                                flareGasesEditCB.Text = " Редактировать состав газов";
                                flareGasesEditCB.Checked = true;
                            }                                       
                        }

                        var coefficients2 = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuelForFlareCombustion");
                        var measurementconditions1 = coefficientDB.getMeasurementConditions(3);
                        var combustion = coefficientDB.getCombustionType(combustionFlareCB.SelectedIndex + 1);

                        FlareCO2DensityTB.Text = measurementconditions1.CO2Density.ToString();
                        tbKoefVibrosCH4.Text = coefficients2.EmissionFactorCH4.ToString();
                        tbKoefVibrosCO2.Text = coefficients2.EmissionFactorCO2.ToString();
                        tbDensityMetan.Text = measurementconditions1.CH4Density.ToString();
                        tbVibrosCO2.Text = fuelUsageFlareTB.Text.Length!=0 ? (double.Parse(fuelUsageFlareTB.Text.Replace('.', ',')) * coefficients2.EmissionFactorCO2).ToString():"";
                        tbVibrosCH4.Text = fuelUsageFlareTB.Text.Length != 0 ? (double.Parse(fuelUsageFlareTB.Text.Replace('.', ',')) * coefficients2.EmissionFactorCH4).ToString():"";
                        tbKoefNedoj.Text = combustion.Coefficient.ToString();
                        break;
                    case 5:
                        selectIndexTabPage = 3;
                        tabControl1.SelectedIndex = 3;
                        tabPage4.Enabled = true;
                        cbSaveFugitive.Checked = false;
                        emissionsResult4.Text = "";

                        gasUsageFugitiveTB.Text = "";
                            CH4ShareTB.Text = "";
                            CO2ShareTB.Text = "";
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);
                        }
                        
                        var coefficients = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuelForFugitivEmission");
                        var measurementconditions = coefficientDB.getMeasurementConditions(3);

                        FugitiveCO2DensityTB.Text = measurementconditions.CH4Density.ToString();
                        tbCO2Density.Text = measurementconditions.CO2Density.ToString();
                        tbValueCH4Default.Text = coefficients.CH4Content.ToString(); ;
                        tbValueCO2Default.Text = coefficients.CO2Content.ToString(); ;
                        tbFigusiveCH4.Text = CH4ShareTB.Text.Length != 0 ? (double.Parse(CH4ShareTB.Text.Replace('.', ',')) * measurementconditions.CH4Density * Math.Pow(10, -2)).ToString():"";
                        tbFigusiveCO2.Text= CO2ShareTB.Text.Length != 0 ? (double.Parse(CO2ShareTB.Text.Replace('.', ',')) * measurementconditions.CO2Density * Math.Pow(10, -2)).ToString() : "";


                        break;

                    case 10:
                        selectIndexTabPage = 4;
                        tabControl1.SelectedIndex = 4;
                        tabPage5.Enabled = true;
                        cbSaveResultTransport.Checked = false;
                        emissionsResult5.Text = "";

                            lUsageTransportTB.Text = "";
                            tUsageTransportTB.Text = "";
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);
                        }
                        var coefTransport = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuelForTransport");

                        tbRasxodToplivaRaschet.Text = lUsageTransportTB.Text.Length != 0 ? (double.Parse(lUsageTransportTB.Text.Replace('.', ',')) *coefTransport.Density).ToString() : "";
                        tbPlotnostTopliva.Text = coefTransport.Density.ToString();
                        tbKoefVibrosov.Text = coefTransport.EmissionFactor1.ToString();

                        break;
                    case 11:
                        selectIndexTabPage = 5;
                        tabControl1.SelectedIndex = 5;
                        tabPage6.Enabled = true;
                        cbSaveResultKocVibros.Checked = false;
                        emissionsResult6.Text = "";

                        electroUsageTB.Text = "";
                            heatUsageTB.Text = "";
                        if (calresADO.HaveResult((int)selectedNode.Tag))
                        {
                            fillWithData((int)selectedNode.Tag);
                            SetWhoLastSave((int)selectedNode.Tag);
                        }

                        var coefficients1 = coefficientDB.getObject(getFuelId(), "dbo.EnergySystem");
                        tbVibrosElectro.Text = electroUsageTB.Text.Length !=0 ? (double.Parse(electroUsageTB.Text.Replace('.', ',')) * coefficients1.EnergySystemCoeff1 * 0.001).ToString() : "" ;
                        tbVibrosTeplo.Text = heatUsageTB.Text.Length != 0 ? (double.Parse(heatUsageTB.Text.Replace('.', ',')) * coefficients1.EnergySystemCoeff2 * 0.001).ToString() : "";
                        break;
                }

                //var calcResId = soeADO.HaveCalculationResult((int)selectedNode.Tag);
                //if (calcResId == 0)
                //{
                //    //MessageBox.Show("Нет сохранённых вычислений(отобразить форму рассчёта)");
                //}
                //else
                //{
                //    MessageBox.Show("Отобразить форму результата рассчёта");
                //}
            }
            else
            {
                selectIndexTabPage = 6;
                tabControl1.SelectedIndex = 6;
                btToExcel.Visible = true;
                if (selectedNode.Level == 2 || selectedNode.Level == 1 || selectedNode.Level == 0)
                {
                    lblFullPath.Text = GetNodeFullPath(selectedNode);
                    primaryEmissionsCb.Checked = true;
                    indirectEmissionsCb.Checked = true;
                    createStatistics();
                }
            }
        }

        public void SetWhoLastSave(int sourcefuelId)
        {
            ToolTip tooltip = new ToolTip();
            CalculationResultADO calresADO = new CalculationResultADO();
            int idcalc = calresADO.getIdResCalc(sourcefuelId);
            lblLastSave.Visible = true;
            CalculationResult cr = calresADO.getObject(idcalc);
            lblWhoLastSave.Text = cr.PersonnelLogin + "  " + cr.ShortDate;
            lblWhoLastSave.Visible = true;
            tooltip.SetToolTip(lblWhoLastSave, cr.SaveData); 
        }
        public void SetWhoSaveNow(int calclId)
        {
            ToolTip tooltip = new ToolTip();
            CalculationResultADO calresADO = new CalculationResultADO();            
            lblLastSave.Visible = true;
            CalculationResult cr = calresADO.getObject(calclId);
            lblWhoLastSave.Text = cr.PersonnelLogin + "  " + cr.ShortDate;
            lblWhoLastSave.Visible = true;
            tooltip.SetToolTip(lblWhoLastSave, cr.SaveData);
        }

        private void co2densitylbl1_Click(object sender, EventArgs e)
        {

        }

        private string GetNodeFullPath(TreeNode node)
        {
            string fullPath = node.Text;
            TreeNode parentNode = node.Parent;

            while (parentNode != null)
            {
                fullPath = parentNode.Text + "//" + fullPath;
                parentNode = parentNode.Parent;
            }

            return fullPath;
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
            var emissions = calculator.GasFuel(getFuelId(), gases, gasesUsageTB.Text.Length > 0 ? Double.Parse(gasesUsageTB.Text.Replace('.', ',')) : 0);
            emissionsResult1.Text = emissions.ToString();
            if (checkBox1.Checked)
            {
                CalculationResultADO calcADO = new CalculationResultADO();
                int addCalcID = addResult(emissionsResult1.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID);
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, gasesUsageTB.Name, gasesUsageTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, CO2DensityGasTB.Name, CO2DensityGasTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, emissionsResult1.Name, emissionsResult1.Text);

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
                    double koefVibros = double.Parse(emissionsResult1.Text.Replace('.', ',')) /double.Parse(gasesUsageTB.Text.Replace('.', ','));
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
            double usage = fluidUsageTB.Text.Length != 0 ? Double.Parse(fluidUsageTB.Text.Replace('.', ',')) : 0;
            double lowerHeat = fluidLowerHeatTB.Text.Length != 0 ? Double.Parse(fluidLowerHeatTB.Text.Replace('.', ',')) : 0;

            CalculationResultADO calcADO = new CalculationResultADO();
            var emissions = calculator.FluidFuel(getFuelId(), usage, lowerHeat);
            emissionsResult2.Text = emissions.ToString();
            if (checkBox2.Checked)
            {
                int addCalcID = addResult(emissionsResult2.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID);
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, fluidUsageTB.Name, fluidUsageTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, fluidLowerHeatTB.Name, fluidLowerHeatTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, dictLowerHeatTB.Name, dictLowerHeatTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, fluidUsageTJTB.Name, fluidUsageTJTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbCoefEmisson.Name, tbCoefEmisson.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, emissionsResult2.Name, emissionsResult2.Text);
                    calcADO.CalculationCompositionForm(calcControls, addCalcID);                   

                }
            }

        }

        private void calculateFlareBTN_Click(object sender, EventArgs e)
        {
            double usage = fuelUsageFlareTB.Text.Length != 0 ? Double.Parse(fuelUsageFlareTB.Text.Replace('.', ',')) : 0;

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

            var emissions = calculator.FlareCombustion(getFuelId(), combustionFlareCB.SelectedIndex + 1, gases, usage);
            emissionsResult3.Text = emissions.ToString();
            if (checkBox3.Checked)
            {
                int addCalcID = addResult(emissionsResult3.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID);
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, fuelUsageFlareTB.Name, fuelUsageFlareTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, FlareCO2DensityTB.Name, FlareCO2DensityTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, combustionFlareCB.Name, (combustionFlareCB.SelectedIndex).ToString());
                    calcControls = calcADO.addToListForResForm(calcControls, emissionsResult3.Name, emissionsResult3.Text);

                    calcControls = calcADO.addToListForResForm(calcControls, tbKoefVibrosCO2.Name, tbKoefVibrosCO2.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbVibrosCO2.Name, tbVibrosCO2.Text); 
                    calcControls = calcADO.addToListForResForm(calcControls, tbDensityMetan.Name, tbDensityMetan.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbKoefVibrosCH4.Name, tbKoefVibrosCH4.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbVibrosCH4.Name, tbVibrosCH4.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbKoefNedoj.Name, tbKoefNedoj.Text);


                    if (flareGasesEditCB.Checked)
                    {
                        calcControls = calcADO.addToListForResForm(calcControls, flareGasesEditCB.Name, "1");

                        calcControls = calcADO.addToListForResForm(calcControls, flareTB1.Name, flareTB1.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB2.Name, flareTB2.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB3.Name, flareTB3.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB4.Name, flareTB4.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB5.Name, flareTB5.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB6.Name, flareTB6.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB7.Name, flareTB7.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB8.Name, flareTB8.Text);
                        calcControls = calcADO.addToListForResForm(calcControls, flareTB9.Name, flareTB9.Text);
                    }
                    else
                        calcControls = calcADO.addToListForResForm(calcControls, flareGasesEditCB.Name, "0");
                    calcADO.CalculationCompositionForm(calcControls, addCalcID);

                }

            }
        }

        private void calculateFugitiveBTN_Click(object sender, EventArgs e)
        {
            double usage = gasUsageFugitiveTB.Text.Length != 0 ? Double.Parse(gasUsageFugitiveTB.Text.Replace('.', ',')) : 0;
            double CH4Share = CH4ShareTB.Text.Length != 0 ? Double.Parse(CH4ShareTB.Text.Replace('.', ',')) : 0;
            double CO2Share = CO2ShareTB.Text.Length != 0 ? Double.Parse(CO2ShareTB.Text.Replace('.', ',')) : 0;
            var emissions = calculator.FugitiveEmissions(getFuelId(), usage, CH4Share, CO2Share);
            emissionsResult4.Text = emissions.ToString();
            if (cbSaveFugitive.Checked)
            {
                CalculationResultADO calcADO = new CalculationResultADO();
                int addCalcID = addResult(emissionsResult4.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID);
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, gasUsageFugitiveTB.Name, gasUsageFugitiveTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, CH4ShareTB.Name, CH4ShareTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, CO2ShareTB.Name, CO2ShareTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbCO2Density.Name, tbCO2Density.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, FugitiveCO2DensityTB.Name, FugitiveCO2DensityTB.Text);
                     calcControls = calcADO.addToListForResForm(calcControls, emissionsResult4.Name, emissionsResult4.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbValueCH4Default.Name, tbValueCH4Default.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbValueCO2Default.Name, tbValueCO2Default.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbFigusiveCH4.Name, tbFigusiveCH4.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbFigusiveCO2.Name, tbFigusiveCO2.Text);

                    calcADO.CalculationCompositionForm(calcControls, addCalcID);

                }
            }
        }

        private void calculateTransportBTN_Click(object sender, EventArgs e)
        {
            double tonns = tUsageTransportTB.Text.Length != 0 ? Double.Parse(tUsageTransportTB.Text.Replace('.', ',')) : 0;
            double liters = lUsageTransportTB.Text.Length != 0 ? Double.Parse(lUsageTransportTB.Text.Replace('.', ',')) : 0;
            var emissions = calculator.Transport(getFuelId(), tonns, liters);
            emissionsResult5.Text = emissions.ToString();
            if (cbSaveResultTransport.Checked)
            {
                CalculationResultADO calcADO = new CalculationResultADO();
                int addCalcID = addResult(emissionsResult5.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID);
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, tUsageTransportTB.Name, tUsageTransportTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, lUsageTransportTB.Name, lUsageTransportTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbRasxodToplivaRaschet.Name, tbRasxodToplivaRaschet.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbPlotnostTopliva.Name, tbPlotnostTopliva.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbKoefVibrosov.Name, tbKoefVibrosov.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, emissionsResult5.Name, emissionsResult5.Text);
                    calcADO.CalculationCompositionForm(calcControls, addCalcID);

                }
            }

        }

        private void calculateIndirectBTN_Click(object sender, EventArgs e)
        {
            double electro = electroUsageTB.Text.Length != 0 ? Double.Parse(electroUsageTB.Text.Replace('.', ',')) : 0;
            double heat = heatUsageTB.Text.Length != 0 ? Double.Parse(heatUsageTB.Text.Replace('.', ',')) : 0;
            var emissions = calculator.IndirectWorks(getFuelId(), getFuelId(), electro, heat);
            emissionsResult6.Text = emissions.ToString();
            if (cbSaveResultKocVibros.Checked)
            {
                int addCalcID = addResult(emissionsResult6.Text);
                if (addCalcID != 0)
                {
                    SetWhoSaveNow(addCalcID); 
                    List<CalculationCompositionForm> calcControls = new List<CalculationCompositionForm>();
                    calcControls = calcADO.addToListForResForm(calcControls, electroUsageTB.Name, electroUsageTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, heatUsageTB.Name, heatUsageTB.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, emissionsResult6.Name, emissionsResult6.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbVibrosElectro.Name, tbVibrosElectro.Text);
                    calcControls = calcADO.addToListForResForm(calcControls, tbVibrosTeplo.Name, tbVibrosTeplo.Text);
                    calcADO.CalculationCompositionForm(calcControls, addCalcID);

                }
            }

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
            if (GasTBChecked())
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
        }
        public bool GasTBChecked()
        {
            double check;
            if ((!double.TryParse(gasesUsageTB.Text, out check) && gasesUsageTB.Text.Length != 0) || (!double.TryParse(mixPercent1.Text, out check) && mixPercent1.Text.Length != 0) ||
                (!double.TryParse(mixPercent2.Text, out check) && mixPercent2.Text.Length != 0) || (!double.TryParse(mixPercent3.Text, out check) && mixPercent3.Text.Length != 0) ||
                (!double.TryParse(mixPercent4.Text, out check) && mixPercent4.Text.Length != 0) || (!double.TryParse(mixPercent5.Text, out check) && mixPercent5.Text.Length != 0) ||
                (!double.TryParse(mixPercent6.Text, out check) && mixPercent6.Text.Length != 0) || (!double.TryParse(mixPercent7.Text, out check) && mixPercent7.Text.Length != 0) ||
                (!double.TryParse(mixPercent8.Text, out check) && mixPercent8.Text.Length != 0) || (!double.TryParse(mixPercent9.Text, out check) && mixPercent9.Text.Length != 0) 
                || (!double.TryParse(mixPercent10.Text, out check) && mixPercent10.Text.Length != 0) )
                {
                lblErrorGas.Visible = true;
                button1.Enabled = false;
                return false;
            }
            else
            {
                lblErrorGas.Visible = false;
                button1.Enabled = true;
                return true;
            }
        }

        private void fluidLowerHeatTB_TextChanged(object sender, EventArgs e)
        {
            if (FluidTBChecked())
            {
                if (fluidLowerHeatTB.Text.Length == 0)
                {
                    TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                    var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                    dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
                }
                else
                {
                    dictLowerHeatTB.Text = (Double.Parse(fluidLowerHeatTB.Text.Replace('.', ',')) * 0.0041868).ToString();
                }
                if (fluidUsageTB.Text.Length == 0)
                {
                    TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                    var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                    fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text.Replace('.', ',')) * Double.Parse(dictLowerHeatTB.Text.Replace('.', ',')) * 0.001 : 0).ToString();
                }
                else
                {
                    fluidUsageTJTB.Text = (Double.Parse(fluidUsageTB.Text.Replace('.', ',')) * Double.Parse(dictLowerHeatTB.Text.Replace('.', ',')) * 0.001).ToString();
                }
            }
        }
        public bool FluidTBChecked()
        {
            double check;
            if ((!double.TryParse(fluidLowerHeatTB.Text, out check) && fluidLowerHeatTB.Text.Length != 0) || (!double.TryParse(fluidUsageTB.Text, out check) && fluidUsageTB.Text.Length != 0))
            {
                lblErrorFluid.Visible = true;
                calculateFluidBTN.Enabled = false;
                return false;
            }
            else
            {
                lblErrorFluid.Visible = false;
                calculateFluidBTN.Enabled = true;
                return true;
            }
        }

        private void fluidUsageTB_TextChanged(object sender, EventArgs e)
        {
            if (FluidTBChecked())
            {
                if (fluidLowerHeatTB.Text.Length == 0)
                {
                    TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                    var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                    dictLowerHeatTB.Text = coeff.ConversionFactor2.ToString();
                }
                else
                {
                    dictLowerHeatTB.Text = (Double.Parse(fluidLowerHeatTB.Text.Replace('.', ',')) * 0.0041868).ToString();
                }
                if (fluidUsageTB.Text.Length == 0)
                {
                    TypeOfFuelWithCoefADO coefficientDB = new TypeOfFuelWithCoefADO();
                    var coeff = coefficientDB.getObject(getFuelId(), "dbo.TypeOfFuel");
                    fluidUsageTJTB.Text = (fluidUsageTB.Text.Length > 0 ? Double.Parse(fluidUsageTB.Text.Replace('.', ',')) * Double.Parse(dictLowerHeatTB.Text.Replace('.', ',')) * 0.001 : 0).ToString();
                }
                else
                {
                    fluidUsageTJTB.Text = (Double.Parse(fluidUsageTB.Text.Replace('.', ',')) * Double.Parse(dictLowerHeatTB.Text.Replace('.', ',')) * 0.001).ToString();
                }
            }

        }

        public void createStatistics()
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            var calcresDB = new CalculationResultADO();
            disableAllPages();
            tabPage7.Enabled = true;
            if (selectedNode.Level == 2 && (primaryEmissionsCb.Checked || indirectEmissionsCb.Checked))
            {

                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 2, primaryEmissionsCb.Checked, indirectEmissionsCb.Checked);
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
            if (selectedNode.Level == 1 && (primaryEmissionsCb.Checked || indirectEmissionsCb.Checked))
            {
                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 1, primaryEmissionsCb.Checked, indirectEmissionsCb.Checked);
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
            if (selectedNode.Level == 0 && (primaryEmissionsCb.Checked || indirectEmissionsCb.Checked))
            {
                var results = calcresDB.GetDataForStatistics((int)selectedNode.Tag, 0, primaryEmissionsCb.Checked, indirectEmissionsCb.Checked);
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
        public void disableAllPages()
        {
            co2densitylbl1.Enabled = false;
            tabPage2.Enabled = false;
            tabPage3.Enabled = false;
            tabPage4.Enabled = false;
            tabPage5.Enabled = false;
            tabPage6.Enabled = false;
            tabPage7.Enabled = false;
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
            var excel = new ExcelWork();
            if(selectedNode.Level!=3)
                excel.getFullReport((int)selectedNode.Tag, selectedNode.Level);

        }


        private void buttonAddCompanyDO_Click(object sender, EventArgs e)
        {
            using (FormAddCompany frmaddcmp = new FormAddCompany())
            {
                frmaddcmp.tree = treeView1;
                frmaddcmp.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = 0;
            if(mixPercent1.Text != "")
              x = int.Parse(mixPercent1.Text.Replace('.', ','));
            x = x + 10;
            mixPercent1.Text = x.ToString();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode != null)
            {
                SourceOfEmissionFuelADO soefADO = new SourceOfEmissionFuelADO();
                if (soefADO.Delete((int)selectedNode.Tag))
                    selectedNode.Parent.Nodes.Remove(selectedNode);
            }


        }

        private void primaryEmissionsCb_CheckedChanged(object sender, EventArgs e)
        {
            createStatistics();
            if (!indirectEmissionsCb.Checked && !primaryEmissionsCb.Checked)
            {
                statsDatagrid.DataSource = null;
            }

        }

        private void indirectEmissionsCb_CheckedChanged(object sender, EventArgs e)
        {
            createStatistics();
            if (!indirectEmissionsCb.Checked && !primaryEmissionsCb.Checked)
            {
                statsDatagrid.DataSource = null;
            }
        }

        private void lUsageTransportTB_TextChanged(object sender, EventArgs e)
        {

           if(ForTransportTBChanged())
            tbRasxodToplivaRaschet.Text = (lUsageTransportTB.Text.Length != 0 && tbKoefVibrosov.Text.Length != 0) ? (double.Parse(lUsageTransportTB.Text.Replace('.', ',')) * 0.001 * double.Parse(tbPlotnostTopliva.Text.Replace('.', ','))).ToString() : "";
            

           
        }

        private void electroUsageTB_TextChanged(object sender, EventArgs e)
        {
            if (UsageTBChecked())
            {
                var coefficientDB = new TypeOfFuelWithCoefADO();
                var coefficients1 = coefficientDB.getObject(getFuelId(), "dbo.EnergySystem");
                tbVibrosElectro.Text = electroUsageTB.Text.Length != 0 ? (double.Parse(electroUsageTB.Text.Replace('.', ',')) * coefficients1.EnergySystemCoeff1 * 0.001).ToString() : "";
            }
            else
                tbVibrosElectro.Text = "";


        }

        private void heatUsageTB_TextChanged(object sender, EventArgs e)
        {
            if (UsageTBChecked())
            {
                var coefficientDB = new TypeOfFuelWithCoefADO();
                var coefficients1 = coefficientDB.getObject(getFuelId(), "dbo.EnergySystem");
                tbVibrosTeplo.Text = heatUsageTB.Text.Length != 0 ? (double.Parse(heatUsageTB.Text.Replace('.', ',')) * coefficients1.EnergySystemCoeff2 * 0.001).ToString() : "";
            }
            else
                tbVibrosTeplo.Text = "";


        }
        public bool UsageTBChecked()
        {
            double check;
            if ((!double.TryParse(heatUsageTB.Text, out check) && heatUsageTB.Text.Length != 0) || (!double.TryParse(electroUsageTB.Text, out check) && electroUsageTB.Text.Length != 0))
            {
                lblErrorKosVibros.Visible = true;
                calculateIndirectBTN.Enabled = false;
                return false;
            }
            else
            {
                lblErrorKosVibros.Visible = false;
                calculateIndirectBTN.Enabled = true;
                return true;
            }
        }

        private void CH4ShareTB_TextChanged(object sender, EventArgs e)
        {
            if (ShareTBChecked())
            {
                if (FugitiveCO2DensityTB.Text.Length != 0)
                    tbFigusiveCH4.Text = CH4ShareTB.Text.Length != 0 ? (double.Parse(CH4ShareTB.Text.Replace('.', ',')) * double.Parse(FugitiveCO2DensityTB.Text.Replace('.', ',')) * Math.Pow(10, -2)).ToString() : "";
            }
            else
                tbFigusiveCH4.Text = "";
        }

        private void CO2ShareTB_TextChanged(object sender, EventArgs e)
        {
            if (ShareTBChecked())
            {
                if (tbCO2Density.Text.Length != 0)
                    tbFigusiveCO2.Text = CO2ShareTB.Text.Length != 0 ? (double.Parse(CO2ShareTB.Text.Replace('.', ',')) * double.Parse(tbCO2Density.Text.Replace('.', ',')) * Math.Pow(10, -2)).ToString() : "";
            }
            else
                tbFigusiveCO2.Text = "";
        }
        public bool ShareTBChecked()
        {
            double check;
            if ((!double.TryParse(CH4ShareTB.Text, out check) && CH4ShareTB.Text.Length != 0) || (!double.TryParse(CO2ShareTB.Text, out check) && CO2ShareTB.Text.Length != 0))
            {
                lblErrorFigusive.Visible = true;
                calculateFugitiveBTN.Enabled = false;
                return false;
            }
            else
            {
                lblErrorFigusive.Visible = false;
                calculateFugitiveBTN.Enabled = true;
                return true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var excel = new ExcelWork();
            excel.getFullReport(11, 0);
        }

        private void fuelUsageFlareTB_TextChanged(object sender, EventArgs e)
        {
            if (ForFlareTBChanged())
            {
                if (tbKoefVibrosCH4.Text.Length != 0)
                    tbVibrosCH4.Text = fuelUsageFlareTB.Text.Length != 0 ? (double.Parse(fuelUsageFlareTB.Text.Replace('.', ',')) * double.Parse(tbKoefVibrosCH4.Text.Replace('.', ','))).ToString() : "";
                if (tbKoefVibrosCO2.Text.Length != 0)
                    tbVibrosCO2.Text = fuelUsageFlareTB.Text.Length != 0 ? (double.Parse(fuelUsageFlareTB.Text.Replace('.', ',')) * double.Parse(tbKoefVibrosCO2.Text.Replace('.', ','))).ToString() : "";
            }
            else
            {
                tbVibrosCH4.Text = "";
                tbVibrosCO2.Text = "";
            }
        }

        private void gasesUsageTB_KeyPress(object sender, KeyPressEventArgs e)
        {       
            
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex != selectIndexTabPage)
            {
                e.Cancel = true;
            }
        }

        private bool ForTransportTBChanged()
        {
            double check;
            if ((!double.TryParse(tUsageTransportTB.Text, out check) && tUsageTransportTB.Text.Length != 0) || (!double.TryParse(lUsageTransportTB.Text, out check) && lUsageTransportTB.Text.Length != 0))
            {
                lblTextErrortranstort.Visible = true;
                calculateTransportBTN.Enabled = false;
                return false;
            }
            else
            {
                lblTextErrortranstort.Visible = false;
                calculateTransportBTN.Enabled = true;
                return true;
            }
        }
        private void TransportChanged(object sender, EventArgs e)
        {
            ForTransportTBChanged();        
        }

        private bool ForFlareTBChanged()
        {
            double check;
            if ((!double.TryParse(fuelUsageFlareTB.Text, out check) && fuelUsageFlareTB.Text.Length != 0) || (!double.TryParse(flareTB1.Text, out check) && flareTB1.Text.Length != 0) ||
                (!double.TryParse(flareTB2.Text, out check) && flareTB2.Text.Length != 0) || (!double.TryParse(flareTB3.Text, out check) && flareTB3.Text.Length != 0) || 
                (!double.TryParse(flareTB4.Text, out check) && flareTB4.Text.Length != 0) || (!double.TryParse(flareTB5.Text, out check) && flareTB5.Text.Length != 0) || 
                (!double.TryParse(flareTB6.Text, out check) && flareTB6.Text.Length != 0) || (!double.TryParse(flareTB7.Text, out check) && flareTB7.Text.Length != 0) ||
                (!double.TryParse(flareTB8.Text, out check) && flareTB8.Text.Length != 0) || (!double.TryParse(flareTB9.Text, out check) && flareTB9.Text.Length != 0) )
            {
                lblErrorFlare.Visible = true;
                calculateFlareBTN.Enabled = false;
                return false;
            }
            else
            {
                lblErrorFlare.Visible = false;
                calculateFlareBTN.Enabled = true;
                return true;
            }
        }
        private void flareTB1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gasesUsageTB_TextChanged(object sender, EventArgs e)
        {
            GasTBChecked();
        }
    }
}
