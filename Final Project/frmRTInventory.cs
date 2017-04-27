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
            int updatedCount;
            int upc = (int)dgvInventory.SelectedRows[0].Cells[0].Value;
            int count;

            //This button click event will show the customer/employee what was purchased and also remove the purchased item or items from the database
            //if input is valid, display results to the customer/employee about the purchase
            try
            {
                if (txtQuantity.Text != "")
                {
                    int quantity = int.Parse(txtQuantity.Text);
                    double price = (double)dgvInventory.SelectedRows[0].Cells[2].Value;
                    total = quantity * price;
                    lblStatusStrip.Text = "You purchased: " + dgvInventory.SelectedRows[0].Cells[1].Value + " x" + txtQuantity.Text + " Your total comes to: " + total.ToString("C");

                    //Update the counts by subtracting what was purchased, then updating the new count in the database
                    count = (int)adapter.FindCount(upc);
                    updatedCount = count - quantity;
                    adapter.Update(updatedCount, upc);
                    dgvInventory.DataSource = adapter.GetData();

                    //If the count of the selected item falls below 5, a purchase order will be sent to the supplier
                    if (updatedCount < 5)
                    {
                        if (DateTime.Now.Hour > 17)
                        {
                            MessageBox.Show("A purchase order has been sent to the appropriate vendor to order 50 " + dgvInventory.SelectedRows[0].Cells[1].Value +
                                "\n" + "Since the order was placed after 5PM, the order will be shipped tomorrow morning.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("A purchase order has been sent to the appropriate vendor to order 50 " + dgvInventory.SelectedRows[0].Cells[1].Value +
                                "\n" + "Since the order was placed before 5PM, the order will be shipped today.");
                            updatedCount = count + 50;
                            adapter.Update(updatedCount, upc);
                            dgvInventory.DataSource = adapter.GetData();
                            return;
                        }
                    }
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
