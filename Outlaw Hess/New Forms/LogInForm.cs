using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Outlaw_Hess
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbUser.Clear(); //empty's textbox user name
            tbPass.Clear(); //empty's textbox password name
        }
        public static string SetValueForName1 = ""; //declares the string value for the username to be null
        private void btnLogin_Click(object sender, EventArgs e)
        {
            SetValueForName1 = tbUser.Text; //sets the string value for the username to be textbox username

            string user, pass; //declares the string user and pass
            user = tbUser.Text; //declares user to be textbox username
            pass = tbPass.Text; //declares pass to be textbox password
            if (user=="admin" && pass=="123") //if both user and password equals to the value "admin" and "123"
            {
                this.Hide(); //hides form
                Main ss = new Main(); //declares what form to open
                ss.Show(); //open the form
            }
            else
            {
                MessageBox.Show("Username or Password is incorrect"); //if incorrect show messagebox
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); //closes the form
        }

        private void LogInForm_KeyDown(object sender, KeyEventArgs e) //checks if the form is being pressed while on the form
        {
            if (e.KeyCode == Keys.Enter)  //if key enter is pressed
            {
                btnLogin.PerformClick(); //perform the button login method
            }
        }
    }
}
