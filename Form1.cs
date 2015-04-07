using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Name: Daniel Dowd
// Date: 3/3/2015
// Description: Assignment 5
namespace Banking
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Create objects in the form module
        Customer myCustomer = new Customer();

        Object currentAccount = new Object();

        SavingsAccount mySavings = new SavingsAccount();

        CheckingAccount myChecking = new CheckingAccount();

        List<Customer> customerList = new List<Customer>();
          
        private void Form1_Load(object sender, EventArgs e)
        {
            #region Test Subjects
            Customer customer1 = new Customer();
            CheckingAccount checking1 = new CheckingAccount();
            SavingsAccount savings1 = new SavingsAccount();
            customer1._customerNumber = "123ABC";
            customer1._salutation = "Mr.";
            customer1._firstName = "Dan";
            customer1._lastName = "Brown";
            customer1._address = "3939 Test";
            customer1._city = "St. Louis";
            customer1._state = "Mo";
            customer1._zipcode = "63893";
            customer1._homePhone = "3144948321";
            customer1._cellPhone = "3148389333";
            customer1.Accounts.Add(checking1);
            customer1.Accounts.Add(savings1);

            Customer customer2 = new Customer();
            CheckingAccount checking2 = new CheckingAccount();
            customer2._customerNumber = "456DEF";
            customer2._salutation = "Mrs.";
            customer2._firstName = "Susan";
            customer2._lastName = "Green";
            customer2._address = "3940 Somestreet";
            customer2._city = "St. Louis";
            customer2._state = "Mo";
            customer2._zipcode = "63103";
            customer2._homePhone = "3147755555";
            customer2._cellPhone = "314555090";
            customer2.Accounts.Add(checking2);

            customerList.Add(customer1);
            customerList.Add(customer2);
            foreach (Customer c in customerList)
            {
                cboFormCustomer.Items.Add(c._fullName);
            }
            #endregion
            DefaultSettings();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DefaultSettings();
        }
        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // Create customer to store tag from AddCustomer form
            Customer objCustomer;
            AddCustomer addCustomer = new AddCustomer();
            // call AddCustomer form
            addCustomer.ShowDialog();
            if (addCustomer.Tag != null)
            {
                objCustomer = (Customer)addCustomer.Tag;
                customerList.Add(objCustomer);
            }
            // add the new customer to the customer accounts cbo
            LoadCustomers();
        }
   
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            if (currentAccount is SavingsAccount)
            {
                // check if input is a number
                if (IsNumber(txtDepositAmount.Text))
                {
                    try
                    {
                        // deposit
                        mySavings.DepositAmount(Convert.ToDouble(txtDepositAmount.Text));
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                    }
                    finally
                    {
                        MessageBox.Show(String.Format("Successfully Added ${0}", txtDepositAmount.Text));
                        txtDepositAmount.Text = String.Empty;
                    }
                }
            }
            else if (currentAccount is CheckingAccount)
            {
                if (IsNumber(txtDepositAmount.Text))
                {
                    myChecking.DepositAmount(Convert.ToDouble(txtDepositAmount.Text));
                    MessageBox.Show(String.Format("Successfully Added ${0}", txtDepositAmount.Text));
                    txtDepositAmount.Text = String.Empty;
                }
            }
            else
            {
                MessageBox.Show("Please Select an Account");
            }
        }

        private void btnShowBalance_Click(object sender, EventArgs e)
        {
            if (IsInput(cboAccount.Text))
            {
                if (currentAccount is SavingsAccount)
                {
                    MessageBox.Show(String.Format("Customer {0} has ${1} in Savings", myCustomer._firstName, mySavings._balance));
                }
                else if (currentAccount is CheckingAccount)
                {
                    MessageBox.Show(String.Format("Customer {0} has ${1} in Checking", myCustomer._firstName, myChecking._balance));
                }
            }
            else
            {
                MessageBox.Show("Please Select an Account");
            }

        }

        private void btnWithdrawal_Click(object sender, EventArgs e)
        {
            // check to see if input is a number
            if (IsNumber(txtWithdrawAmount.Text))
            {
                double withdrawAmount = Convert.ToDouble(txtWithdrawAmount.Text);
                // check for account type
                if (currentAccount is SavingsAccount)
                {
                    int counter = 0;
                    try
                    {
                        mySavings.WithdrawAmount(Convert.ToDouble(withdrawAmount));

                    }
                    catch (Exception myEx)
                    {
                        counter++;
                        MessageBox.Show(myEx.Message);
                    }
                    finally
                    {
                        if (counter < 0)
                        {
                            MessageBox.Show(String.Format("Successfully Withdrew ${0}", txtWithdrawAmount.Text));
                        }
                        txtWithdrawAmount.Text = String.Empty;
                    }
                }
                else 
                {
                    try
                    {
                        myChecking.WithdrawAmount(Convert.ToDouble(withdrawAmount));
                    }
                    catch (Exception myEx)
                    {
                        MessageBox.Show(myEx.Message);
                    }
                    finally
                    {
                        txtWithdrawAmount.Text = String.Empty;
                    }
                }
            }
         }
        private void btnPrintStatement_Click(object sender, EventArgs e)
        {
            if (IsInput(cboAccount.Text))
            {
                if (currentAccount is SavingsAccount)
                {
                    // create a new statement
                    Statement savingStatement = new Statement();
                    // pass attributes to new instance
                    savingStatement._type = 1;
                    savingStatement._owner = mySavings._owner;
                    savingStatement._balance = mySavings._balance;
                    savingStatement._interest = mySavings.AddInterest();
                    savingStatement.ShowDialog();
                }
                else if (currentAccount is CheckingAccount)
                {
                    Statement checkingStatement = new Statement();
                    checkingStatement._type = 2;
                    checkingStatement._owner = myChecking._owner;
                    checkingStatement._balance = myChecking._balance;
                    checkingStatement._overdraftFee = myChecking.OverdraftFee;
                    checkingStatement._numberOfOverdraft = myChecking._numberOfOverdrafts;
                    checkingStatement.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please Select an Account");
            }
        }

        private void cboFormCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // assign myCustomer to selected customer
            myCustomer = customerList[cboFormCustomer.SelectedIndex];
            // fill in text boxes
            txtFirstName.Text = myCustomer._firstName;
            txtLastName.Text = myCustomer._lastName;
            txtAccountNumber.Text = myCustomer._customerNumber;
            // settings
            LoginSettings();
            LoadAccounts();
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selected account
            currentAccount = myCustomer.Accounts[cboAccount.SelectedIndex];
            // cast account for future and fill in text boxes
            if (currentAccount is SavingsAccount)
            {
                mySavings = (SavingsAccount)currentAccount;
                mySavings._accountNum = myCustomer._customerNumber;
                mySavings._firstName = myCustomer._firstName;
                mySavings._lastName = myCustomer._lastName;
            }
            else
            {
                myChecking = (CheckingAccount)currentAccount;
                myChecking._accountNum = myCustomer._customerNumber;
                myChecking._firstName = myCustomer._firstName;
                myChecking._lastName = myCustomer._lastName;
            }
        }
        #region Settings
        public void LoadCustomers()
        {
            foreach (Customer c in customerList)
            {
                if (cboFormCustomer.Items.Contains(c._fullName) == false)
                {
                    cboFormCustomer.Items.Add(c._fullName);
                }
            }
        }
        public void LoadAccounts()
        {
            cboAccount.Items.Clear();
            foreach (BankAccount c in myCustomer.Accounts)
            {
                cboAccount.Items.Add(c._type);
            }
        }
        public void DefaultSettings()
        {
            btnDeposit.Enabled = false;
            btnWithdrawal.Enabled = false;
            btnShowBalance.Enabled = false;
            btnPrintStatement.Enabled = false;
            txtDepositAmount.Enabled = false;
            txtWithdrawAmount.Enabled = false;
            txtDepositAmount.Enabled = false;
            txtAccountNumber.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            cboAccount.Enabled = true;
            txtAccountNumber.Focus();
            txtAccountNumber.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtWithdrawAmount.Text = "";
            txtDepositAmount.Text = "";
        }
        public void LoginSettings()
        {
            btnDeposit.Enabled = true;
            btnWithdrawal.Enabled = true;
            btnShowBalance.Enabled = true;
            btnPrintStatement.Enabled = true;
            txtDepositAmount.Enabled = true;
            txtWithdrawAmount.Enabled = true;
            txtDepositAmount.Enabled = true;
            txtAccountNumber.Enabled = false;
            cboAccount.Enabled = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            cboAccount.Enabled = true;
            txtWithdrawAmount.Focus();
        }
        #endregion
        #region Validation
        static bool IsNumber(string inputTest)
        {
            int output;
            bool test = int.TryParse(inputTest, out output);
            if (test)
            {
                return true;
            }
            else
                return false;
        }
        static bool IsInput(string inputTest)
        {
            if (inputTest == String.Empty)
                return false;
            else
                return true;
        }
        #endregion
    }
}
