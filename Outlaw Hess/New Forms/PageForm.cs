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
    public partial class PageForm : Form
    {
        SQLiteCommand dbcmd; //declares the command dbcmd to SQLiteCommand
        SQLiteCommand dbcmdDelete;

        string conString; //declares conString to be a string.
        SQLiteConnection dbCon = new SQLiteConnection(); //declares a new connection
        SQLiteDataAdapter daProduct; //declares a new data adapter
        SQLiteDataAdapter daCustomer; //declares a adapter
        SQLiteDataAdapter daProduct2;

        DataTable dtCustomer = new DataTable(); //creates a new table for Customer
        DataTable dtAccount = new DataTable(); //create a new table for Account
        DataTable dtProduct = new DataTable(); //create a new table for Product
        int rowAt; //declares rowAt to be a integer

        public PageForm(string con) //gatheres a connection from the form 
        {
            InitializeComponent();
            conString = con; //declares conString to be the connection
            dbCon.ConnectionString = con; //declares dbCon to be a connection
        }

        private void PageForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; //loads the form to be in window state maximized

            tsslWelcome.Text = "Welcome: " + LogInForm.SetValueForName1; //gatheres the string value from form login and place it in the label.

            btnSearchBig.Enabled = false; //declares the button Search big to be disabled
            btnSearchSmall.Enabled = false; //declares the button search small to be disabled
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

        /// <summary>
        ///                                                 Form Accounts
        /// </summary>

        private void accountnames() //method is being used to gather names to be filled for form ISA name account Overnight accured int
        {
            try
            {
                using (SQLiteConnection dbCon = new SQLiteConnection(conString)) //using a new connection string 
                {
                    string sql1 = @"SELECT title || "" "" || firstname || "" "" || lastname as fullname,"
                    + " lastname, custid from customer order by lastname"; //declares the SQL code where you will be able to gather information

                    dtCustomer.Clear(); //clears the date table
                    daCustomer = new SQLiteDataAdapter(sql1, dbCon); //gathers the SQL and the connection for the datetable
                    daCustomer.Fill(dtCustomer); //fills the date table with the SQL command

                    cmbCust.DataSource = dtCustomer; //fills the comboboxcust with the data from the datatable
                    cmbCust.DisplayMember = "fullname"; //displays the title, firstname and lastname in the combobox
                    cmbCust.ValueMember = "custid"; //declaing the fullname by customer id
                    cmbCust.SelectedIndex = -1; //select nothing when start
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //if this doesnt work display error message.
            }
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            PnlOAI.Visible = true; //shows the panel overnight annual itnrest 

            PnlNewISA.Visible = false; //Panel for new ISA
            PnlISAAccount.Visible = false; //Panel for view ISA accounts

            PnlViewCustomers.Visible = false; //Panel to view customers
            PnlNewCustomer.Visible = false; //Panel new customer

            cbCustomer.Visible = false; //combobox customer
            CbISAAccounts.Visible = false; //combobox isa accounts

            PnlTran.Visible = false; //Panel to view transaction

            accountnames(); //use the method accountnames 
        }
        private void isanamesforaccInt() //displays the isa names
        {
            try
            {
                SQLiteConnection dbCon = new SQLiteConnection(conString); //creates a new SQL connection 
                string sql2 = @"SELECT product.isaname as isa, account.accid as acc"
                + " from account inner join product"
                + " on account.prodid = product.prodid"
                + " where account.custid = @cust"; //SQL code to receive the information

                SQLiteDataAdapter daAccount = new SQLiteDataAdapter(sql2, dbCon); //creates a new SQL data adapter 

                daAccount.SelectCommand.Parameters.AddWithValue("@cust", cmbCust.SelectedValue); //gatheres the values from the combo box customer

                dtAccount.Clear(); //clears the table 
                daAccount.Fill(dtAccount); //Fills the table with the data from the SQL

                //Bind datatable dtAccount to the Account ComboBox
                cmbAcc.DataSource = dtAccount;
                cmbAcc.DisplayMember = "isa"; 
                cmbAcc.ValueMember = "acc";
                cmbAcc.SelectedIndex = -1;

            }
            catch (Exception ex) //shows a error if the code doesnt work
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                using (dbCon = new SQLiteConnection(conString))
                {
                    string sql = @"SELECT product.prodid, product.isaname, product.intrate, account.balance, account.accrued, account.accid"
                    + " FROM account INNER JOIN product on account.prodid = product.prodid"
                    + " WHERE account.custid =@cust"
                    + " AND account.accid =@isa"; //declares the SQL string

                    daProduct = new SQLiteDataAdapter(sql, dbCon);

                    daProduct.SelectCommand.Parameters.AddWithValue("cust", cmbCust.SelectedValue);   //fill placeholders with data from UI
                    daProduct.SelectCommand.Parameters.AddWithValue("isa", cmbAcc.SelectedValue); 


                    dtProduct.Clear(); //clears the table product
                    daProduct.Fill(dtProduct); //fills the table product with the data from product

                    rowAt = 0; //declares the row to look at row 0

                    DataRow row = dtProduct.Rows[rowAt]; //looks at the table product and the row

                    //fill the labels with the coordinate columns 
                    lblvalue1.Text = row["accrued"].ToString();
                    lblvalue2.Text = row["balance"].ToString();
                    lblvalue3.Text = row["intrate"].ToString();
                    lblvalue4.Text = lblvalue1.Text + (Convert.ToDouble(lblvalue2.Text) * Convert.ToDouble(lblvalue3.Text) / 365);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); //show error message if their is a problem 
            }
        }

        private void cmbCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            isanamesforaccInt(); //run method
        }

        private void cmbAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAcc.SelectedValue != null)
            {
                try
                {
                    using (dbCon = new SQLiteConnection(conString))
                    {
                        string sql2 = $@"SELECT * FROM account WHERE accid=@acc"; //sets the SQL string using info from combobox

                        string sql = @"SELECT product.prodid, product.isaname, product.intrate, account.balance, account.accrued, account.accid"
                        + " FROM account INNER JOIN product on account.prodid = product.prodid"
                        + " WHERE account.custid =@cust"
                        + " AND account.accid =@isa"; //SQL code

                        daProduct = new SQLiteDataAdapter(sql, dbCon); //declaring the data adapter for sql 
                        daProduct2 = new SQLiteDataAdapter(sql2, dbCon); //declaring the data adapter for sql2

                        //fill placeholders with data from UI
                        daProduct.SelectCommand.Parameters.AddWithValue("cust", cmbCust.SelectedValue);
                        daProduct.SelectCommand.Parameters.AddWithValue("isa", cmbAcc.SelectedValue);

                        daProduct2.SelectCommand.Parameters.AddWithValue("acc", cmbAcc.SelectedValue);

                        DataTable dtProduct2 = new DataTable(); //creating a new data table

                        dataGridView1.DataSource = new BindingSource(dtProduct2, null); //declaring the datagrid data

                        dtProduct2.Clear(); //clear the data table 
                        daProduct2.Fill(dtProduct2); //fill the data table

                        dtProduct.Clear(); //clear the data table
                        daProduct.Fill(dtProduct); //fill the data table

                        rowAt = 0; //declaring the start row = 0

                        DataRow row = dtProduct.Rows[rowAt]; //declaring the row to be looking at table rows 

                        lblvalue1.Text = row["accrued"].ToString(); //declaring each label to be the value
                        lblvalue2.Text = row["balance"].ToString();
                        lblvalue3.Text = row["intrate"].ToString();
                        lblvalue4.Text = lblvalue1.Text + (Convert.ToDouble(lblvalue2.Text) * Convert.ToDouble(lblvalue3.Text) / 365);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }
            }
        }

        /// <summary>
        ///                                 FORM CUSTOMER NEW CUSTOMER
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnCustomer_Click(object sender, EventArgs e)
        {
            customer();

            PnlOAI.Visible = false; //Panel for Accounts

            PnlNewISA.Visible = false; //Panel for new ISA
            PnlISAAccount.Visible = false; //Panel for view ISA products

            PnlViewCustomers.Visible = false; //Panel to view customers
            PnlNewCustomer.Visible = false; //Panel new customer

            CbISAAccounts.Visible = false; //combobox isa accounts

            PnlTran.Visible = false; //Panel to view transaction

            cbCustomer.Visible = true; //shows the combobox customer
        }


        private void customer() //method customer
        {
            dbCon = new SQLiteConnection(conString); //new sql connection 
            string sql = @"SELECT * FROM CUSTOMER"; //declares the SQL 

            daCustomer = new SQLiteDataAdapter(sql, dbCon); //new data adapter
            daCustomer.Fill(dtCustomer); //fills the data table
        }
        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCustomer.Text == "New Customer") //if the combobox equals to the text "New Customer" 
            {
                PnlViewCustomers.Visible = false; //hide the panel view customer
                PnlNewCustomer.Visible = true; //show the panel new customer

                rowAt = 0; //declares the row to look at row 0 of the table
                newcustomer(); //run method newcustomer 
            }
            else //if the combobox equal to anything else
            {
                PnlNewCustomer.Visible = false; //hide panel new customer
                PnlViewCustomers.Visible = true; //show panel view customer

                viewcustomers(); //run method view customer
                rowAt = 0; //declares to look at row 0 at the table
            }
        }

        private void newcustomer() //method new customer
        {
            DataRow row = dtCustomer.Rows[rowAt]; //declares row to be data from the table table customer

            tbNum.Text = (dtCustomer.Rows.Count + 1).ToString(); //adds one to the row number count
            cbTit.Text = ""; //set the textboxes to be blank
            tbFirst.Text = "";
            tbLast.Text = "";
            tbDoB.Text = "";
            tbNI.Text = "";
            tbEmail.Text = "";
            tbPassword.Text = "";
            tbAllow.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbTit.Text == "" || tbFirst.Text == "" || tbLast.Text == "" || tbDoB.Text == "" || tbNI.Text == "" || tbEmail.Text == "" || tbPassword.Text == "" || tbAllow.Text == "")
            {
                MessageBox.Show("Please make sure each textbox is filled with the correct information and no boxes are empty.");
            }
            else
            {
                using (SQLiteCommand dbcmd = dbCon.CreateCommand())
                {
                    dbCon.ConnectionString = conString; //declaring connection to conString

                    dbcmd.CommandText = @"Insert into Customer(custid, title, firstname, lastname, dob, nicode, email, password, allowance)
                Values (@custid, @title, @firstname, @lastname, @dob, @nicode, @email, @password, @allowance)"; //SQL 

                    //fill placeholders with data from UI
                    dbcmd.Parameters.AddWithValue("custid", tbNum.Text);
                    dbcmd.Parameters.AddWithValue("title", cbTit.Text);
                    dbcmd.Parameters.AddWithValue("firstname", tbFirst.Text);
                    dbcmd.Parameters.AddWithValue("lastname", tbLast.Text);
                    dbcmd.Parameters.AddWithValue("dob", tbDoB.Text);
                    dbcmd.Parameters.AddWithValue("nicode", tbNI.Text);
                    dbcmd.Parameters.AddWithValue("email", tbEmail.Text);
                    dbcmd.Parameters.AddWithValue("password", tbPassword.Text);
                    dbcmd.Parameters.AddWithValue("allowance", tbAllow.Text);

                    dbCon.Open(); //open the connection
                    int recordsChanged = dbcmd.ExecuteNonQuery(); //adds the data to the table
                    MessageBox.Show(recordsChanged.ToString() + " records added"); //show messagebox
                    dbCon.Close(); //closes the connection

                    newcustomer(); //run method new customer
                }
                try
                {
                    dbCon.ConnectionString = conString; //connection to conString
                    dbcmd = dbCon.CreateCommand(); //Creates the command
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); //show error if doesnt work
                }
            }
        }

        /// <summary>
        ///                             FORM CUSTOMER VIEW CUSTOMER
        /// </summary>

        private void viewcustomers()
        {
            DataRow row = dtCustomer.Rows[rowAt]; //sets the value row to be looking at table customer

            tb1.Text = row["custid"].ToString(); //declaring what label to look at what column
            cmb1.Text = row["title"].ToString();
            tb2.Text = row["firstname"].ToString();
            tb3.Text = row["lastname"].ToString();
            tb4.Text = row["dob"].ToString();
            tb5.Text = row["nicode"].ToString();
            tb6.Text = row["email"].ToString();
            tb7.Text = row["password"].ToString();
            tb8.Text = row["allowance"].ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (rowAt < dtCustomer.Rows.Count - 1) //if row is less than the number of rows within the table allow it to keep cycling through
            {
                rowAt++; //cycle to the next one
                btnPrevious.Enabled = true; //allow to go back
            }
            else if (rowAt > dtCustomer.Rows.Count) //if row is greater than dt customer
            {
                btnNext.Enabled = false; //disable the button next
                rowAt = 0; //set row to 0
            }
            viewcustomers(); //run method view customer
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (rowAt > 0) //if row is greater than 0
            {
                rowAt--; //allow it to go back
            }
            if (rowAt == 0) //if row = to 0
            {
                btnPrevious.Enabled = false; //disblae the button
            }
            viewcustomers(); //run method view customer
        }

        /// <summary>
        ///                             ISA ACCOUNT FORM VIEW ISA ACCOUNT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnIsaAccounts_Click(object sender, EventArgs e)
        {
            IsaAccountInfo();

            PnlOAI.Visible = false; //Panel for Accounts

            PnlNewISA.Visible = false; //Panel for new ISA
            PnlISAAccount.Visible = false; //Panel for view ISA accounts

            PnlViewCustomers.Visible = false; //Panel to view customers
            PnlNewCustomer.Visible = false; //Panel new customer

            cbCustomer.Visible = false; //combobox customer

            PnlTran.Visible = false; //Panel to view transaction

            CbISAAccounts.Visible = true; //ComboBox Accounts
        }

        private void IsaAccountInfo()
        {
            string sqlcommand = @"SELECT * FROM product"; //declares the SQL to look at all the data within products

            daProduct = new SQLiteDataAdapter(sqlcommand, dbCon); //creates a new data adapter using the connection string and the SQL
            daProduct.Fill(dtProduct); //fills the data table using the data adapter
        }
        private void ShowISAAccountRecords()
        {
            DataRow row = dtProduct.Rows[rowAt]; //declares row to be the row of the rowAt

            lblAccID.Text = row["prodid"].ToString(); //delcaring what column each label will look at
            lblProductName.Text = row["isaname"].ToString();
            lblstatus1.Text = row["status"].ToString();
            lblTranIn.Text = "Number of Transactions In :" + row["transin"].ToString();
            lblIntrestRate.Text = "Intrest rate: " + row["intrate"].ToString();
        }

        private void CbISAAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbISAAccounts.Text == "ISA Accounts") //checks if the combobox has the text ISA Accounts
            {
                PnlISAAccount.Visible = true; //shows panel ISA Accounts
                PnlNewISA.Visible = false; //hides the panel new isa
                rowAt = 0; //declares the row which it starts
                ShowISAAccountRecords(); //run method ShowISAAccountsRecords
            }
            else //if there is something else in the form
            {
                PnlNewISA.Visible = true; //show panel NewISA 
                PnlISAAccount.Visible = false; //hides panel ISAAccounts
                rowAt = 0; //declaring the row which it starts
            }
        }

        private void btnAccNext_Click(object sender, EventArgs e)
        {
            if (rowAt < dtProduct.Rows.Count - 1) //if rowAt is less than number of rows within data table product
            {
                rowAt++; //allow it to move up the table
            }
            else
            {
                rowAt = 0; //leave it as 0
            }
            ShowISAAccountRecords(); //run the method
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (rowAt > 0) //rowAt is greater than 0
            {
                rowAt--;// allow it to move down the table
            }
            if (rowAt == 0)
            {
                btnPrev.Enabled = false; //disable the button previous
            }
            ShowISAAccountRecords(); //run the method
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (lblstatus1.Text == "open") //if the label equals to open
            {
                DataRow row = dtProduct.Rows[rowAt]; //check what row within the table the user is at
                row["status"] = "close"; //change the status to close

                ShowISAAccountRecords(); //run method
            }
            else if (lblstatus1.Text == "close") //if its close
            {
                DataRow row = dtProduct.Rows[rowAt]; //check what row within the table the user is at
                row["status"] = "open"; //change the status to open

                ShowISAAccountRecords(); //run method
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to Delete this?", "Moving record", MessageBoxButtons.YesNo);
            if (lblAccID.Text == "11")
            {
                MessageBox.Show("You cannot delete the holding account!!"); //messagebox to stop the user deleting the holding account
            }
            else
            {
                if (result == DialogResult.Yes) //if the user chooses yes
                {
                    using (SQLiteCommand dbcmd = dbCon.CreateCommand())
                    {
                        SQLiteCommand dbcmdDelete = dbCon.CreateCommand();
                        dbCon.ConnectionString = conString; //connect the connection to string conString
                        dbcmd.CommandText =
                            @"UPDATE account SET prodID = '11' WHERE prodid =@ID"; //declare the SQL 

                        dbcmdDelete.CommandText = @"DELETE FROM product WHERE prodid=@IDDELETE"; //declare the SQL 


                        dbcmd.Parameters.AddWithValue("ID", lblAccID.Text);  //fill placeholders with data from UI

                        dbcmdDelete.Parameters.AddWithValue("IDDELETE", lblAccID.Text);

                        dbCon.Open(); //open the connection
                        int recordsChanged = dbcmd.ExecuteNonQuery(); //opens the database and execute the SQL
                        int recordsChangedDelete = dbcmdDelete.ExecuteNonQuery();
                        MessageBox.Show(recordsChanged.ToString() + " Data has been moved and " + recordsChangedDelete.ToString() + "data has been deleted"); //show messagebox
                        dbCon.Close(); //close the database
                    }
                    try
                    {
                        dbCon.ConnectionString = conString; // Declaring the connection
                        dbcmd = dbCon.CreateCommand(); // Creates a command using the connection
                        dbcmdDelete = dbCon.CreateCommand(); // Creates a seperate command for deleting
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message); // Execute a error message if occurs
                    }
                    ShowISAAccountRecords(); // Updates the page.
                }
            }
        }
        private void btnIntrest_Click(object sender, EventArgs e)
        {
            DataRow row = dtProduct.Rows[rowAt]; //declaring the row from the table products
            row["intrate"] = tbInt.Text; //implmenting the data from the textbox to the row

            ShowISAAccountRecords(); //run method ShowISAAccountRecords
        }

        /// <summary>
        ///                                 ISA ACCOUNT FORM NEW ISA ACCOUNT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //checks if the corresponding values are correct
            if (textBox2.Text.Length > 2 && comboBox1.Text == "open" || comboBox1.Text == "close" && textBox1.Text.Length > 1)
            {
                using (SQLiteCommand dbcmd = dbCon.CreateCommand())
                {
                    dbCon.ConnectionString = conString;
                    dbcmd.CommandText = @"Insert into Product(prodid, isaname, status, transin, intrate)
            Values (@prodid, @isaname, @status, @transin, @intrate)"; //SQL string

                    //fill placeholders with data from UI
                    dbcmd.Parameters.AddWithValue("prodid", dtProduct.Rows.Count + 1);
                    dbcmd.Parameters.AddWithValue("isaname", textBox1.Text);
                    dbcmd.Parameters.AddWithValue("status", comboBox1.Text);
                    dbcmd.Parameters.AddWithValue("transin", 0);
                    dbcmd.Parameters.AddWithValue("intrate", textBox2.Text);

                    dbCon.Open(); //open connection
                    int recordsChanged = dbcmd.ExecuteNonQuery(); //adds the value to the data
                    MessageBox.Show(recordsChanged.ToString() + " records added"); //messagebox to interact 
                    dbCon.Close(); //closes connection
                }
                try
                {
                    dbCon.ConnectionString = conString; // Declaring a connection
                    dbcmd = dbCon.CreateCommand(); // Creates a new connection
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message); // Displays a error message
                }
            }
            else if (textBox1.Text == "")
            {
                // Displays a text message if their is nothing present within the textbox
                MessageBox.Show("Check if the intrest rate is more than 3% or if the product name is blank or if the combo box is blank");
            }
            else
                MessageBox.Show("Check if the intrest rate is more than 3% or if the product name is blank or if the combo box is blank");
        }

           /// <summary>
           ///                                  FORM TRANSACTION
           /// </summary>

        private void accountnameTran()
        {
            try
            {
                using (SQLiteConnection dbCon = new SQLiteConnection(conString))
                {
                    string sql1 = @"SELECT title || "" "" || firstname || "" "" || lastname as fullname,"
                    + " lastname, custid from customer order by lastname"; //declaring the SQL 

                    dtCustomer.Clear(); // Clears the data table
                    daCustomer = new SQLiteDataAdapter(sql1, dbCon); //creates a connection to data table
                    daCustomer.Fill(dtCustomer); // Fills the data table
                    
                    comboBox2.DataSource = dtCustomer; // Connect the combobox to the data table
                    comboBox2.DisplayMember = "fullname"; // Displaying the fullname of the customer to the combobox
                    comboBox2.ValueMember = "custid"; // Sets the value of the combobox with the value of custID
                    comboBox2.SelectedIndex = -1; // Deselects the combobox (leaves blank when load)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message); // Executes a error if their is a error within the code
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection dbCon = new SQLiteConnection(conString);
                string sql2 = @"SELECT product.isaname as isa, account.accid as acc"
                + " from account inner join product"
                + " on account.prodid = product.prodid"
                + " where account.custid = @customer"; // Declaring the SQL code

                SQLiteDataAdapter daAccount = new SQLiteDataAdapter(sql2, dbCon); // Creates a connection with the connection and SQL

                daAccount.SelectCommand.Parameters.AddWithValue("@customer", comboBox2.SelectedValue); //fill placeholders with data from UI

                dtAccount.Clear(); // Clear the data table
                daAccount.Fill(dtAccount); // Fill the data table with the connection

                cmbAccountName.DataSource = dtAccount; // Declare the combobox source to coordinate with the data from SQL table
                cmbAccountName.DisplayMember = "isa"; // Display the ISA name
                cmbAccountName.ValueMember = "acc"; // Declare the value of the combobox to be the value of account ID
                cmbAccountName.SelectedIndex = -1; // Deselect the combobox to display nothing
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Executes a error if their is a problem with the code above
            }
        }

        private void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteCommand dbcmd = dbCon.CreateCommand()) // Declares the connection which will be used within this code
            {
                try
                {
                    // Declaring the select with the information value from the comboxbox
                    dbcmd.CommandText = $@"SELECT action as TransactionType,"
                    + " amnt as Amount, event as DateAndTime FROM TRANX WHERE accid= " + (cmbAccountName.SelectedIndex);
                    SQLiteDataAdapter daTrans = new SQLiteDataAdapter(dbcmd);

                    listBox1.Items.Clear(); // Clear the listbox1 = Transaction Type
                    listBox2.Items.Clear(); // Clear the listbox2 = Amount
                    listBox3.Items.Clear(); // Clear the listbox3 = Data and Time

                    DataTable dtTrans = new DataTable(); // Create a new data table which will be set with information later

                    using (SQLiteConnection conn = new SQLiteConnection(conString)) // using a different connection 
                    {
                        dtTrans.Clear(); // Clearing the data table
                        daTrans = new SQLiteDataAdapter(dbcmd.CommandText, conString); // Creates a new data adapter which connects to the connection and the SQL
                        daTrans.Fill(dtTrans); // Fills the data table with information from the SQL table
                    }
                    foreach (DataRow dat in dtTrans.Rows) // foreach loop to fill in the listboxes
                    {
                        listBox1.Items.Add(dat["TransactionType"]); // Declaring what infromation column should go in which listbox
                        listBox2.Items.Add(dat["Amount"]);
                        listBox3.Items.Add(dat["DateAndTime"]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex); // Executes a error message if their is a problem with the code above
                }
            }
        }

        private void btnTran_Click(object sender, EventArgs e)
        {
            accountnameTran(); // Displays the information within the form
            PnlTran.Visible = true; // Declaring the the panel to be visible

            PnlOAI.Visible = false; //Panel for Accounts

            PnlNewISA.Visible = false; //Panel for new ISA
            PnlISAAccount.Visible = false; //Panel for view ISA accounts

            PnlViewCustomers.Visible = false; //Panel to view customers
            PnlNewCustomer.Visible = false; //Panel new customer

            cbCustomer.Visible = false; //combobox customer

            CbISAAccounts.Visible = false; //ComboBox Accounts
        }
    }
}
