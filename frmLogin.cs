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
    public partial class frmLogin : Form
    {
        string cs = "Data Source=localhost;Initial Catalog=BluePrinteGTextile;Integrated Security=True";
        SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=BluePrinteGTextile;Integrated Security=True");

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var frmRegister = new frmRegister();
            frmRegister.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter adapt = new SqlDataAdapter("SELECT Count(*) from Registration where UserName='" + txtUserName.Text + "' and Password='" + txtPassword.Text + "'", conn);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();

                var frmMainMenu = new frmMainMenu();
                frmMainMenu.Show();
            }
            else
            {
                MessageBox.Show("Please Check your UserName or Password");
            }
        }
    }
}