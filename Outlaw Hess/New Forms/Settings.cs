using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Outlaw_Hess
{
    public partial class Settings : Form
    {
        SQLiteConnection myConn= new SQLiteConnection();
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            btnPagesBig.Enabled = false;
            btnPagesSmall.Enabled = false;

            tsslWelcome.Text = "Welcome: " + LogInForm.SetValueForName1;
            myConn.ConnectionString = home;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            myConn.Close();



            btnSearchBig.Enabled = false;
            btnSearchSmall.Enabled = false;

            btnPagesBig.Enabled = false;
            btnPagesSmall.Enabled = false;

            btnSearchBig.BackColor = Color.Red;
            btnSearchSmall.BackColor = Color.Red;

            tsslStatus.Text = " ~~ You have been disconnected from the database ~~ ";
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //the button minimized to minimize the application on press
        }

        private void btnRestoreUpNDown_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized) //checks to see if the form is maximized
            {
                this.WindowState = FormWindowState.Normal; //if form is maximized display the form to be normal state

                PnlSideBig.Visible = false; //declares the panel side big to be hidden

                PnlSideSmall.Visible = true; //declares the panel side small to be shown
            }
            else
            {
                this.WindowState = FormWindowState.Maximized; //if form is normal display the form to be maximized state

                PnlSideBig.Visible = true; //declares the panel side big to be shown

                PnlSideSmall.Visible = false; //declares the panel side small to be hidden
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo); //displays a messagebox which allows the user to interact
            if (result == DialogResult.Yes) //if the user presses Yes on the dialog
            {
                Environment.Exit(0); //closes the program 
            }
            else
            {

            }
        }

        private void btnHomeBig_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form 
            Main index = new Main(); //declares what form should be open up
            index.Show();//shows the form home
        }

        private void btnHomeSmall_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form
            Main index = new Main(); //declares what form should be open
            index.Show(); //shows the form home
        }

        private void btnSettingsBig_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form
            Settings set = new Settings(); //declares what form should be open
            set.Show(); //shows the form settings
        }

        private void btnSettingsSmall_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form
            Settings set = new Settings(); //declares what form should be open
            set.Show(); //shows the form settings
        }

        private void btnInfoBig_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form
            Info info = new Info(); //declares what form should be open
            info.Show(); //shows the form information
        }

        private void btnInfoSmall_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the form
            Info info = new Info(); //declares what form should be open
            info.Show(); //shows the form information
        }

        const string home = @"Data Source = D:\Outlaw Hess\Outlaw Hess\bin\Debug\Bandit.db";

        private void search()
        {
            btnSearchBig.BackColor = Color.DarkGreen; // Displasy the colour green on the connection when pressed
            btnSearchSmall.BackColor = Color.DarkGreen;

            btnSearchBig.Enabled = false; // Disabling the buttons once pressed
            btnSearchSmall.Enabled = false;

            tsslStatus.Text = "~~ Database Location: ~~" + home; // Sets the connection to be the location
        }

        private void btnSearchBig_Click(object sender, EventArgs e)
        {
            btnSearchBig.Enabled = false; // Disables the button once pressed
            btnSearchSmall.Enabled = false;

            search(); // Runs the method search

            myConn.Open(); // Opens the connection
            btnPagesBig.Enabled = true; // Sets the pages button to enable
            btnPagesSmall.Enabled = true;
        }

        private void btnSearchSmall_Click(object sender, EventArgs e)
        {
            btnSearchBig.Enabled = false; // Disables the button once pressed
            btnSearchSmall.Enabled = false;

            search(); // Runs the method search

            myConn.Open(); // Opens the connection
            btnPagesBig.Enabled = true; // Sets the pages button to enable
            btnPagesSmall.Enabled = true;
        }

        private void btnPagesBig_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hides the form
            PageForm pages = new PageForm(myConn.ConnectionString); // Creates a connection to the database
            pages.Show(); // Shows the form
        }

        private void btnPagesSmall_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hides the form
            PageForm pages = new PageForm(myConn.ConnectionString); // Creates a connection to the database
            pages.Show(); // Shows the form
        }
    }
}
