using System;

namespace CSharp.LabExercise3
{
    class Account
    {
        public decimal Balance
        {
            get;
            set;
        }

        public Account()
        {
        }
    }

    class WithdrawValidator
    {
        Account account;

        public WithdrawValidator(Account account)
        {
            this.account = account;
        }

        public bool IsValidToWithdraw(decimal amount)
        {
            if (account.Balance == 0)
            {
                Console.WriteLine("Your account is empty. Please deposit first.");
                return false;
            }
            if (amount == null)
            {
                Console.WriteLine("Please enter a valid number.");
                return false;
            }
            if (amount < 1)
            {
                Console.WriteLine("Please enter an amount greater than 0");
                return false;
            }
            if (amount % 100 != 0)
            {
                Console.WriteLine("This ATM machine only accepts withdrawal by P100");
                return false;
            }
            if (amount > account.Balance)
            {
                Console.WriteLine("You have insufficient funds in your account.");
                return false;
            }
            return true;
        }

    }

    class Withdraw
    {
        Account account;
        public Withdraw(Account account)
        {
            this.account = account;
        }
        public void WithdrawCash(decimal amount)
        {
            account.Balance -= amount;
            Console.WriteLine("Withdraw successful! Please take your cash in the cash dispenser.");
        }
    }

    class DepositValidator
    {
        public DepositValidator()
        {
        }

        public bool IsValidToDeposit(decimal amount)
        {
            if (amount == null)
            {
                Console.WriteLine("Please enter a valid number.");
                return false;
            }
            if (amount < 1)
            {
                Console.WriteLine("Please enter an amount greater than 0");
                return false;
            }
            return true;
        }
    }

    class Deposit
    {
        Account account;

        public Deposit(Account account)
        {
            this.account = account;
        }

        public void DepositCash(decimal amount)
        {
            account.Balance += amount;
            Console.WriteLine("Deposit successful!");
        }
    }

    class BalanceRenderer
    {
        Account account;

        public BalanceRenderer(Account account)
        {
            this.account = account;
        }
        public void DisplayBalance()
        {
            Console.WriteLine("Your current balance is P{0}", account.Balance);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            bool toContinue = true;

            Account account = new Account();
            WithdrawValidator withdrawValidator = new WithdrawValidator(account);
            Withdraw withdraw = new Withdraw(account);
            DepositValidator depositValidator = new DepositValidator();
            Deposit deposit = new Deposit(account);
            BalanceRenderer balanceRenderer = new BalanceRenderer(account);

            do
            {
                Console.WriteLine("==============ATM==============\n");
                Console.WriteLine("[1] Check Balance");
                Console.WriteLine("[2] Withdraw Cash");
                Console.WriteLine("[3] Deposit Cash");
                Console.WriteLine("[4] Exit");
                Console.WriteLine("\n*******************************");

                Console.Write("Enter your choice of transaction: ");
                string choice = Console.ReadLine();

                if (int.TryParse(choice, out int choiceTransaction))
                {
                    switch (choiceTransaction)
                    {
                        case 1:
                            balanceRenderer.DisplayBalance();
                            break;

                        case 2:
                            Console.Write("Enter amount to withdraw: ");
                            string amount = Console.ReadLine();

                            if (!decimal.TryParse(amount, out decimal amountTransaction))
                            {
                                Console.WriteLine("Please enter a number.");
                                break;
                            }

                            if (withdrawValidator.IsValidToWithdraw(amountTransaction))
                            {
                                withdraw.WithdrawCash(amountTransaction);
                                balanceRenderer.DisplayBalance();
                            }
                            break;

                        case 3:
                            Console.Write("Enter amount to deposit: ");
                            amount = Console.ReadLine();

                            if (!decimal.TryParse(amount, out amountTransaction))
                            {
                                Console.WriteLine("Please enter a number.");
                                break;
                            }
                            if (depositValidator.IsValidToDeposit(amountTransaction))
                            {
                                deposit.DepositCash(amountTransaction);
                                balanceRenderer.DisplayBalance();
                            }
                            break;

                        case 4:
                            Console.WriteLine("Thank you for banking with us!");
                            toContinue = false;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number.");
                }
            } while (toContinue);


        }
    }
}
