using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RepairsByCustomer_Lab3Alt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customersBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.customersBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.appRepairsDataSet);

        }

        //load data
        private void Form1_Load(object sender, EventArgs e)
        {
            this.customersTableAdapter.Fill(this.appRepairsDataSet.Customers);
            this.repairsTableAdapter.Fill(this.appRepairsDataSet.Repairs);

        }

        //allow searching by customerID and display repairs related to that customer
        private void fillByCustomerIDToolStripButton_Click(object sender, EventArgs e)
        {
            int customerID;
            try
            {
                customerID = Convert.ToInt32(customerIDToolStripTextBox.Text);

                this.customersTableAdapter.FillByCustomerID(
                    this.appRepairsDataSet.Customers, customerID);

                if (customersBindingSource.Count > 0) //if a customer is found
                {
                    this.repairsTableAdapter.FillByCustomerID(
                        this.appRepairsDataSet.Repairs, customerID);
                }
                else
                {
                    MessageBox.Show("No customer was found with this ID. Please try again.", "Customer Not Found");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Customer ID must be an integer.", "Entry Error");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error # " + ex.Number + ":" + ex.Message,
                    ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            customerIDToolStripTextBox.Focus();

        }

        //enable click-through of all customers by re-filling table adapters
        private void getAllCustomersButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.customersTableAdapter.Fill(this.appRepairsDataSet.Customers);
                this.repairsTableAdapter.Fill(this.appRepairsDataSet.Repairs);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error # " + ex.Number + ":" + ex.Message,
                    ex.GetType().ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

    } //END CLASS
}
