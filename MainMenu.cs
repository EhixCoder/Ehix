using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CS_BLUEPRINTE
{
    public partial class frmMainMenu : Form
    {
        string cs = "Data Source=localhost;Initial Catalog=BluePrinteGTextile;Integrated Security=True";
        SqlConnection conn = new SqlConnection("Data Source=EHIS-PC;Initial Catalog=BluePrinteGTextile;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;

        public frmMainMenu()
        {
            InitializeComponent();
            DisplayData();
        }

        // registration of materials + amount per yard + discount per yard + stock('quantities')
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(cs))
                if (txtMaterial.Text != "" && txtAmountperyard.Text != "" && txtStock.Text != "")
                {
                    string query = "INSERT INTO MaterialSetup(Material,AmountPerYard,Stock) VALUES (@Material,@AmountPerYard,@Stock)";
                    conn.Open();
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Material", txtMaterial.Text);
                    cmd.Parameters.AddWithValue("@AmountPerYard", txtAmountperyard.Text);
                    cmd.Parameters.AddWithValue("@Stock", txtStock.Text);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Material is successfully recorded");
                    conn.Close();
                    DisplayData();
                }
                else
                    MessageBox.Show("Please provide details");
        }

        // display successfully inputed materials
        private void DisplayData()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                DataTable dt = new DataTable();
                adapt = new SqlDataAdapter("SELECT * from MaterialSetup", conn);
                adapt.Fill(dt);

                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView1.DataSource = dt;
                conn.Close();
            }
        }

        // clear materials details
        private void clearData()
        {
            txtMaterialId.Text = "";
            txtMaterial.Text = "";
            txtAmountperyard.Text = "";
            txtStock.Text = "";
        }

        // loads the inputed mateials for editing
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtMat.Text = selectedRow.Cells[0].Value.ToString();
            txtMaterials.Text = selectedRow.Cells[1].Value.ToString();
            txtAmountPeryards.Text = selectedRow.Cells[2].Value.ToString();
        }

        private void txtAmountperyard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // for entering number only

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter only Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Handled = true;
            }
        }

        private void txtDiscountperyard_KeyPress(object sender, KeyPressEventArgs e)
        {
            // for entering number only

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter only Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Handled = true;
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // for entering number only

            if ((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("Please Enter only Number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                e.Handled = true;
            }
        }

        private void txtNameofMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // for entering char only
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar == (char)Keys.Back && e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;

            iExit = MessageBox.Show("Are you sure you want to close the application", "Blue Printe G Textile, Management System",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (iExit == DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtOrderId.Clear();
            txtMaterials.Clear();
            txtQuantities.Clear();
            txtAmountPeryards.Clear();
            txtDiscount.Clear();
            txtTotalAmount.Clear();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double amountPerYard, totalAmount, discount;
            int quantity = 0;

            amountPerYard = txtAmountPeryards.Text == "" ? 0 : Convert.ToDouble(txtAmountPeryards.Text);
            discount = txtDiscount.Text == "" ? 0 : Convert.ToDouble(txtDiscount.Text);
            quantity = txtQuantities.Text == "" ? 0 : Convert.ToInt32(txtQuantities.Text);

            totalAmount = amountPerYard * quantity;
            totalAmount -= discount;

            txtTotalAmount.Text = totalAmount.ToString();

        }

        private void btnAddon_Click(object sender, EventArgs e)
        {
            txtMaterial.Clear();
            txtAmountperyard.Clear();
            txtStock.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(cs))
                if (txtMaterial.Text != "" && txtAmountperyard.Text != "" && txtStock.Text != "")
                {

                    //// check if the registered item has been sold
                    //var ccmd = new SqlCommand("select count(*) from sales where materiaid=@id");
                    //ccmd.Parameters.AddWithValue("@id", txtMaterialId.Text);
                    //ccmd.Connection = conn;
                    // int sale_count = (int)ccmd.ExecuteScalar();

                    //if(sale_count >= 1)
                    //{
                    //   var result = MessageBox.Show("This item has sales record\nAre you sure you want to delete this record", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    if(result == DialogResult.Yes)
                    //    {

                    //    }
                    //}

                    //DELETE from MaterialSetup where Id = @id
                    cmd = new SqlCommand("DELETE from MaterialSetup where Id=@id", conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", txtMaterialId.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Record was deleted successfully");
                    dataGridView1.DataSource = null;

                    dataGridView1.DataSource = new DataTable();

                    DisplayData();
                    clearData();
                }
                else
                {
                    MessageBox.Show("Please select record for deleting");
                }
        }

        //load table
        public void refreshSales()
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "select sales.Id, MaterialName = MaterialSetup.Material, sales.Quantity, sales.AmountPerYard, sales.Discount,sales.TotalAmount, OrderId from sales inner join" +
                                    " MaterialSetup on sales.MateriaId = MaterialSetup.Id and dateSold >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and dateSold <= '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59'";
                cmd.Connection = new SqlConnection(cs);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable("Sales");

                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //check for selected 
            if (txtMat.Text == "")
            {
                MessageBox.Show("Please select a material to sell", "Blue Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtAmountPeryards.Text == "")
            {
                MessageBox.Show("Please enter amount for a yard", "Blue Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtQuantities.Text == "")
            {
                MessageBox.Show("Please enter quantity", "Blue Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (txtTotalAmount.Text == "")
            {
                return;
            }

            var cmd = new SqlCommand();
            var conn = new SqlConnection(cs);
            cmd.Connection = conn;
            conn.Open();

            int q = txtQuantities.Text == "" ? 0 : Convert.ToInt32(txtQuantities.Text);
            int stock_quantity = 0;

            if (q == 0)
            {
                MessageBox.Show("Please enter a valid quantity\nQuantity cannot be 0", "Blue Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                // get item quantity
                cmd.CommandText = "select Stock from materialsetup where id = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", txtMat.Text);

                stock_quantity = (int)cmd.ExecuteScalar();

                if (Convert.ToInt32(txtQuantities.Text) > stock_quantity)
                {
                    MessageBox.Show("You do have enough items in stock");
                    return;
                }

            }

            int new_orderId = 0;
            if (txtOrderId.Text == "")
            {
                cmd.CommandText = "select isnull(max(orderId),0) as N from sales";
                cmd.Connection = conn;

                new_orderId = ((int)cmd.ExecuteScalar()) + 1;
                txtOrderId.Text = new_orderId.ToString();
            }

            string insert_query = "insert into sales (MateriaId, Quantity, AmountPerYard, Discount, TotalAmount, OrderId) " +
                "values (@materiaid, @quantity, @amountperyard, @discount, @amount, @orderid);";

            cmd.CommandText = insert_query;
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("materiaid", txtMat.Text);
            cmd.Parameters.AddWithValue("quantity", txtQuantities.Text);
            cmd.Parameters.AddWithValue("amountperyard", txtAmountPeryards.Text);
            cmd.Parameters.AddWithValue("amount", txtTotalAmount.Text);
            cmd.Parameters.AddWithValue("orderid", txtOrderId.Text);
            cmd.Parameters.AddWithValue("discount", txtDiscount.Text);

            if (cmd.Connection == null) cmd.Connection = conn;

            int result = cmd.ExecuteNonQuery();
            if (result == 1) MessageBox.Show("Record Saved", "Blue Print", MessageBoxButtons.OK, MessageBoxIcon.Information);

            refreshSales();

            // update item stock
            cmd.CommandText = "update materialsetup set stock = @new_stock where id = @id";
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("new_stock", Math.Abs(stock_quantity - Convert.ToInt32(txtQuantities.Text)));
            cmd.Parameters.AddWithValue("id", txtMat.Text);
            cmd.ExecuteNonQuery();

            DisplayData();

            txtMat.Text = "";
            txtQuantities.Text = "";
            txtAmountPeryards.Text = "";
            txtTotalAmount.Text = "";
            txtDiscount.Text = "";
            txtMaterials.Text = "";

        }

        private void Print_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedRows.Count == 0) return;
            int p_id = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["orderid"].Value);

            // get data for print
            var dt = new DataTable("report");
            using (var _cmd = new SqlCommand())
            {
                _cmd.CommandText = "select sales.Id, MaterialName = MaterialSetup.Material, sales.Quantity, sales.AmountPerYard, sales.Discount, sales.TotalAmount, GrandTotal = (select sum(TotalAmount) from sales where OrderId = @order_id), OrderId from sales inner join  " +
                                    "MaterialSetup on sales.MateriaId = MaterialSetup.Id where orderid = @order_id";

                _cmd.Parameters.AddWithValue("order_id", p_id);

                _cmd.Connection = new SqlConnection(cs);
                _cmd.Connection.Open();

                var dr = _cmd.ExecuteReader();
                dt.Load(dr);
            }

            var receipt_form = new frmPrint()
            {
                ReportFile = "receipt.rdlc",
                ReportData = dt,
                DataSetName = "SalesReceipt",
                ZoomLevel = 75
            };

            receipt_form.Show();
        }

        private void grbCalculation_Enter(object sender, EventArgs e)
        {

        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            refreshSales();
        }


        private void frmMainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.ExitThread();
            Application.Exit();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView2.Rows[index];
            txtMat.Text = selectedRow.Cells[0].Value.ToString();
            txtMaterials.Text = selectedRow.Cells[1].Value.ToString();
            txtAmountPeryards.Text = selectedRow.Cells[2].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // update records
            using (SqlConnection conn = new SqlConnection(cs))
                if (txtMaterialId.Text != "" && txtMaterial.Text != "" && txtAmountperyard.Text != "" && txtStock.Text != "")
                {
                    conn.Open();
                    string cmdtext = "update MaterialSetup set Material = @materialName, AmountPerYard = @amountPerYard, Stock = @stock where Id =@id;";
                    SqlCommand cmd = new SqlCommand(cmdtext, conn);
                    cmd.Parameters.AddWithValue("@id", txtMaterialId.Text.Trim());
                    cmd.Parameters.AddWithValue("@materialName", txtMaterial.Text.Trim());
                    cmd.Parameters.AddWithValue("@amountPerYard", txtAmountperyard.Text.Trim());
                    cmd.Parameters.AddWithValue("@stock", txtStock.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record updated successfully");

                    refreshSales1();
                }
                else
                {
                    MessageBox.Show("select a record that you want to update");
                }

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];
            txtMaterialId.Text = selectedRow.Cells[0].Value.ToString();
            txtMaterial.Text = selectedRow.Cells[1].Value.ToString();
            txtAmountperyard.Text = selectedRow.Cells[2].Value.ToString();
            txtStock.Text = selectedRow.Cells[3].Value.ToString();
        }

        private void btnPreviewSalesReport_Click(object sender, EventArgs e)
        {
            string query = "select materialname = c.material, d.quantity, d.totalamount, d.discount from sales as d left join " +
                "materialsetup as c on d.materiaid = c.id where d.datesold >= '" + dteStartDate.Value.ToString("yyyy-MM-dd") + "' and " +
                "d.datesold <= '" + dteEndDate.Value.ToString("yyy-MM-dd") + " 23:59'";

            DataTable dt = null;
            using (var cmd = new SqlCommand(query, new SqlConnection(cs)))
            {
                dt = new DataTable("salesReport");
                cmd.Connection.Open();

                dt.Load(cmd.ExecuteReader());
            }

            if (dt != null && dt.Rows.Count >= 1)
            {
                var reportform = new frmPrint()
                {
                    ReportData = dt,
                    ReportFile = "salesreport.rdlc",
                    DataSetName = "salesReport",
                    ZoomLevel = 100
                };

                reportform.Show();
            }

        }

        public void refreshSales1()
        {
            using (var cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from MaterialSetup";
                cmd.Connection = new SqlConnection(cs);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable("MaterialSetup");

                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        //private void btnLogout_Click(object sender, EventArgs e)
        //{
        //    var form = new frmLogin();
        //    form.Show();

        //    var MainMenu = new MainMenu();
        //    form.Hide();
        //}
    }
}
