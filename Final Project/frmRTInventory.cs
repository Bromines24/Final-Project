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
        //Create an adapter object to shorten the amount of code that is needed when calling the table adapter
        InventoryDataSetTableAdapters.InventoryTableAdapter adapter =
                    new InventoryDataSetTableAdapters.InventoryTableAdapter();

        public frmRTInventory()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Filling the table adapter with the appropriate data set
            inventoryTableAdapter.Fill(inventoryDataSet.Inventory);
            //Filling and displaying the data to the datagridview on the form
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

            //This button click event will show the customer what was purchased and also remove the purchased item or items from the database
            //if input is valid, display results to the customer/employee about the purchase
            try
            {
                if(txtQuantity.Text != "")
                {
                    int quantity = int.Parse(txtQuantity.Text);

                    double price = (double)dgvInventory.SelectedRows[0].Cells[2].Value;
                    total = quantity * price;

                    MessageBox.Show("You purchased: " + dgvInventory.SelectedRows[0].Cells[1].Value + " x" + txtQuantity.Text + " Your total comes to: " + total.ToString("C"));
                }

                else
                {
                    lblStatusStrip.Text = "You must enter a valid quantity";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
