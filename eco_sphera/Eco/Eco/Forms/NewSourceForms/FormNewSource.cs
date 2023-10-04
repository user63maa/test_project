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
        public TreeNode selectNode { get; set; }
        public bool edit = false;
        public FormNewSource()
        {
            InitializeComponent();
        }

        private void FormNewSource_Load(object sender, EventArgs e)
        {

        }

        public void preEdit(int id)
        {
            SourceOfEmissionADO soeADO = new SourceOfEmissionADO();
            SourceOfEmission soe = soeADO.getObject(id);
            tbCodeSource.Text = soe.Code;
            tbNameSource.Text = soe.Name;
            btAddSource.Text = "Изменить";
            lblIdProdSite.Text = id.ToString();
            edit = true;

        }

        public void preAdd(int id)
        {
            lblIdProdSite.Text = id.ToString();
            edit = false;
        }
        private void btAddSource_Click(object sender, EventArgs e)
        {
            if (!edit)
            {
                if (tbCodeSource.Text == "")
                    MessageBox.Show("Заполните код источника");
                else
                {
                    if (tbNameSource.Text == "")
                    {
                        MessageBox.Show("Заполните название источника");
                    }
                    else
                    {
                        SourceOfEmissionADO soeADO = new SourceOfEmissionADO();
                        int newId = soeADO.Add(int.Parse(lblIdProdSite.Text), tbNameSource.Text, tbCodeSource.Text);
                        TreeNode newNode = new TreeNode(tbNameSource.Text);
                        newNode.Tag = newId;
                        selectNode.Nodes.Add(newNode);
                        this.Close();
                    }
                }
            }
            else
            {

                if (tbCodeSource.Text == "")
                    MessageBox.Show("Заполните код источника");
                else
                {
                    if (tbNameSource.Text == "")
                    {
                        MessageBox.Show("Заполните название источника");
                    }
                    else
                    {
                        SourceOfEmissionADO soeADO = new SourceOfEmissionADO();                       
                        soeADO.Edit(int.Parse(lblIdProdSite.Text), tbNameSource.Text, tbCodeSource.Text);
                        selectNode.Text = tbNameSource.Text;
                        this.Close();
                    }
                }
                
            }
            
        }
    }
}
