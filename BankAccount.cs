using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking
{
    public class BankAccount
    {
        //Private Variables
        public double _balance {get; set;}
        //Properties
        public string _accountNum { get; set; }
        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public virtual string _type
        {
            get { return "Bank Account"; }
        }

        public virtual string _owner
        {
            get { return string.Format("Account #{0} -- {1} {2}", this._accountNum, this._firstName, this._lastName); }
        }

        public double Balance
        {
            get { return _balance; }
        }

        //Constructors
        public BankAccount()
        { }

        public BankAccount(string v1, string v2, string v3)
        {
            this._firstName = v1;
            this._lastName = v2;
            this._accountNum = v3;
        }

        //Methods
        public double DepositAmount(double amount)
        {
            this._balance = this._balance + amount;
            return _balance;
        }

        public virtual void WithdrawAmount(double amount)
        {
                if (amount > _balance)
                {
                    throw new Exception("No negative withdrawal amounts are allowed. Please enter a valid amount.");
                }
                else
                {
                    this._balance = this._balance - amount;
                }
        }
    }

    public class CheckingAccount : BankAccount
    {
        public override string _type
        {
            get
            {
                    return "Checking Account";
            }
        }
        public double _overdraftFee;
        //Properties
        public override string _owner
        {
            get { return string.Format("Checking-" + base._owner); }
        }
        public Enum _accountType { get; set; }
        public int _numberOfOverdrafts { get; set; }
        public double OverdraftFee
        {
            get { return 10 * this._numberOfOverdrafts; }
        }
        public override void WithdrawAmount(double amount)
        {

            if (amount > 300)
            {
                throw new Exception("Your daily withdraw amount is $300. Please enter a smaller amount.");
            }
            else if (amount > this._balance || this._balance < 0)
            {
                _numberOfOverdrafts++;
                throw new Exception(string.Format("Added a ${0} overdraft", this.OverdraftFee.ToString())); 
            }
            this._balance = this._balance - amount;
            throw new Exception(string.Format("Successfully withdrew {0}", amount));
        }
    }
    public class SavingsAccount : BankAccount
    {
        //Properties
        public override string _type
        {
            get
            {
                  return "Savings Account";
            }
        }
        public override string _owner
        {
            get
            {
                return string.Format("Savings -" + base._owner);
            }
        }
        public double AddInterest()
        {
            if (_balance > 100.0)
            {
                return _balance * .02;
            }
            else
                return 0;
        }
    }
}
