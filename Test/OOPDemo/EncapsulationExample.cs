namespace Test.OOPDemo
{
    // Encapsulation example: a BankAccount hides its balance and exposes methods to operate on it.
    public class BankAccount
    {
        private decimal _balance;   // by default, fields are private if we don't specify an access modifier added by me

        public BankAccount(decimal initialBalance)
        {
            _balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0) _balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            return false;
        }

        public decimal GetBalance() => _balance;
    }
}
