using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banking
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _customerNumber { get; set; }

        Customer myCustomer = new Customer();

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Tag = myCustomer;
            this.Close();

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            CheckingAccount myChecking = new CheckingAccount();
            SavingsAccount mySavings = new SavingsAccount();

            myCustomer._customerNumber = txtCustomerNumber.Text.ToUpper();
            myCustomer._firstName = txtFirstName.Text;
            myCustomer._lastName = txtLastName.Text;
            myCustomer._address = txtAddress.Text.ToUpper();
            myCustomer._city = txtCity.Text;
            myCustomer._state = txtState.Text;
            myCustomer._zipcode = txtZipcode.Text;
            myCustomer._homePhone = txtHomePhone.Text;
            myCustomer._cellPhone = txtCellPhone.Text;
            if(chkChecking.Checked == true)
            {
                myCustomer.Accounts.Add(myChecking);
            }
            if(chkSavings.Checked == true)
            {
                myCustomer.Accounts.Add(mySavings);
            }
            if(chkChecking.Checked == false && chkSavings.Checked == false)
            {
                MessageBox.Show("You must select one account type.");
            }
            this.Tag = myCustomer;
            this.Close();

        }

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            txtCustomerNumber.Text = _customerNumber;
            txtFirstName.Text = _firstName;
            txtLastName.Text = _lastName;
            cboSalutation.Items.Add("Mr.");
            cboSalutation.Items.Add("Mrs.");
            cboSalutation.Items.Add("Ms.");
        }

        private void cboSalutation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSalutation.SelectedIndex == 0)
            {
                myCustomer._salutation = "Mr.";
            }
            else if (cboSalutation.SelectedIndex == 1)
            {
                myCustomer._salutation = "Mrs.";
            }
            else
                myCustomer._salutation = "Ms.";
        }
    }
}
