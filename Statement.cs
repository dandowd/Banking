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
    public partial class Statement : Form
    {
        public Statement()
        {
            InitializeComponent();
        }
        public int _type;
        public int Type
        {
            set { _type = value; }
        }
        public string _owner;
        public string Owner
        {
            set { _owner = value; }
        }
        public double _balance;
        public double Balance
        {
            set { _balance = value; }
        }
        public double _overdraftFee;
        public double OverdraftFee
        {
            set { _overdraftFee = value; }
        }
        public double _interest;
        public double Interest 
        {
            set {_interest = value;}
        }
        public int _numberOfOverdraft;
        public int NumberOfOverdraft
        {
            set {_numberOfOverdraft = value;}
        }

        private void Statement_Load(object sender, EventArgs e)
        {
            if (_type == 1)
            {
                lblOwner.Text = "Statement as of Todays Date " + _owner;
                lblBalance.Text = "Balance $" + Convert.ToString(_balance);
                lbldraftOrInterest.Text = "Interest added to the account this month is $" + _interest.ToString(); 
            }
            else if (_type == 2)
            {
                lblOwner.Text = "Statement as of Todays Date " + _owner;
                lblBalance.Text = "Balance $" + Convert.ToString(_balance);
                lbldraftOrInterest.Text = "Amount of Overdraft this month $" + _overdraftFee.ToString();
                lblNumberofOverdrafts.Text = "The number of overdrafts for the month is " + _numberOfOverdraft.ToString();
            }

        }
    }
}
