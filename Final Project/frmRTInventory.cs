using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class frmRTInventory : Form
    {
        InventoryDataSetTableAdapters.InventoryTableAdapter adapter =
                    new InventoryDataSetTableAdapters.InventoryTableAdapter();

        public frmRTInventory()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            inventoryTableAdapter.Fill(inventoryDataSet.Inventory);
            cboItems.SelectedIndex = -1;
            dgvInventory.DataSource = adapter.GetData();
        }

        private void inventoryBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.inventoryBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inventoryDataSet);

        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            double total;

            //This button click event will show the customer what was purchased and also remove the purchased item or items
            //from the database
            if (txtQuantity.Text != "")
            {
                int quantity = int.Parse(txtQuantity.Text);
                total = quantity * 
                
            }
            else
            {
                lblStatusStrip.Text = "You must enter a valid quantity";
            }

            MessageBox.Show("You purchased: " + cboItems.SelectedItem + " x" + txtQuantity.Text + " Your total comes to: " + total.ToString("C"));
        }
    }
}
