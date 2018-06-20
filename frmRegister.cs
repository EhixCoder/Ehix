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
using System.Security.Cryptography;

namespace CS_BLUEPRINTE
{
    public partial class frmRegister : Form
    {
        string cs = "Data Source=localhost;Initial Catalog=BluePrinteGTextile;Integrated Security=True";
        SqlConnection conn = new SqlConnection("Data Source=EHIS-PC;Initial Catalog=BluePrinteGTextile;Integrated Security=True");

        public frmRegister()
        {
            InitializeComponent();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("please enter your first name");
            }

            if (txtLastName.Text == "")
            {
                MessageBox.Show("please enter your lastname");
            }

            if (txtUserName.Text == "")
            {
                MessageBox.Show("please enter your username");
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("please enter your password");
            }

            if (txtPosition.Text == "")
            {
                MessageBox.Show("please enter your position");
            }

            using (SqlConnection conn = new SqlConnection(cs))
                if (txtFirstName.Text != "" && txtLastName.Text != "" && txtUserName.Text != "" && txtPassword.Text != "" && txtPosition.Text != "")
                {
                    string query = "INSERT INTO Registration(FirstName,LastName,UserName,Password,Position) VALUES (@FirstName,@LastName,@UserName,@Password,@Position)";
                    conn.Open();
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@UserName", txtUserName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    cmd.Parameters.AddWithValue("@Position", txtPosition.Text.Trim());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("user is register successfully");
                    Clear();
                }
        }

        private void Clear()
        {
            txtFirstName.Text = txtLastName.Text = txtUserName.Text = txtPassword.Text = txtPosition.Text = "";
        }

        //public class Security
        //{
        //    public static string HashSHA1(string value)
        //    {
        //        var sha1 = System.Security.Cryptography.SHA1.Create();
        //        var inputBytes = Encoding.ASCII.GetBytes(value);
        //        var hash = sha1.ComputeHash(inputBytes);

        //        var sb = new StringBuilder();
        //        for (var i = 0; i < hash.Length; i++)
        //        {
        //            sb.Append(hash[i].ToString("X2"));
        //        }
        //        return sb.ToString();
        //    }
        //}

        //public static int GetUserIdByUserNameAndPassword(string UserName, string Password)
        //{
        //    // this is the value we will return
        //    int UserId = 0;

        //    SqlConnection conn = new SqlConnection(connectionstring);
        //    var cmd = new SqlCommand( "SELECT UserId, Password, FROM [Registration] WHERE UserName=@UserName", conn))
        //    {
        //        cmd.Parameters.AddWithValue("@UserName", UserName);
        //        conn.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            // dr.Read() = we found user(s) with matching username!

        //            int dbUserId = Convert.ToInt32(dr["UserId"]);
        //            string dbPassword = Convert.ToString(dr["Password"]);

        //            // Now we hash the UserGuid from the database with the password we want to check
        //            // In the same way as when we saved it to the database in the first place.

        //            string hashedPassword = Security.HashSHA1(Password);

        //            // If its correct password the result of the hash is the same as in the database
        //            if(dbPassword == hashedPassword)
        //            {
        //                // The password is correct
        //                UserId = dbUserId;
        //            }
        //        }
        //        conn.Close();
        //    }

        //    // Return the user id which is 0 if we did not found a user.
        //    return UserId;
    }
}