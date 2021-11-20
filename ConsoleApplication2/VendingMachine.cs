using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication2.impl;

namespace ConsoleApplication2
{
    public class VendingMachine
    {
        private int EXIT_OPTION_ID;

        private List<Product> _products;
        private Dictionary<int, Product> _productsCache;

        private List<BankNote> _bankNotes;
        private Dictionary<double, BankNote> _bankNotesCache;
        
        public VendingMachine(List<Product> products, List<BankNote> bankNotes)
        {
            initProducts(products);
            initBankNotes(bankNotes);
            
            // The last option from the products list is the EXIT option - products.lenght+1
            EXIT_OPTION_ID = products.Count;
        }

        public void Start()
        {
            printAvailableCurrencies();
            Console.WriteLine();
            
            printProductsList();
            Console.WriteLine();

            int option;
            do
            {
                option = promptForProductOption();
                Console.WriteLine();
                if (option == EXIT_OPTION_ID) break;

                Console.WriteLine("Enter 0 to interrupt the payment process.");
                PaymentResult payment = doPayment(option);
                if (payment.wasPaymentSuccessful())
                {
                    Console.WriteLine($"Thanks for your purchase!\nCAHSBACK:{payment.getRemainder()}");
                    Console.WriteLine($"Chose an other product or press {EXIT_OPTION_ID} to exit");
                }
                else
                {
                    Console.WriteLine($"Payment interrupted.\nCASHBACK:{payment.getRemainder()}");
                }

            } while (option != EXIT_OPTION_ID);
        }

        private void initProducts(List<Product> products)
        {
            _products = products;

            _productsCache = products
                .Select((product, i) => new {product, i})
                .ToDictionary(prodWIthIndex => prodWIthIndex.i, prodWIthIndex => prodWIthIndex.product);
        }

        private void initBankNotes(List<BankNote> bankNotes)
        {
            _bankNotes = bankNotes;

            _bankNotesCache = bankNotes
                .ToDictionary(bankNote => bankNote.getValue(), bankNote => bankNote);
        }

        private void printProductsList()
        {
            const int ID_COL_LENGTH = 3;
            const int NAME_COL_LENGTH = 15;
            
            Console.WriteLine($"{"ID", -ID_COL_LENGTH}{"NAME", -NAME_COL_LENGTH}PRICE");
            foreach (KeyValuePair<int, Product> product in _productsCache)
            {
                Console.WriteLine($"{product.Key,-ID_COL_LENGTH}{product.Value.getName(),-NAME_COL_LENGTH}${product.Value.getPrice()}");
            }
            Console.WriteLine($"{EXIT_OPTION_ID, -ID_COL_LENGTH}EXIT");
        }

        private void printAvailableCurrencies()
        {
            Console.Write("AVAILABLE BANK NOTES:");
            foreach (var bankNote in _bankNotes)
            {
                Console.Write($" {bankNote.getValue()}");
            }
            Console.WriteLine();
        }

        private int promptForProductOption()
        {
            Console.Write("Choose Product: ");
            return int.Parse(Console.ReadLine());
        }

        private PaymentResult doPayment(int option)
        {
            double remainder;
            double moneyInserted = 0;
            bool enteredEnoughMoney = false;
            Product productChosen = _products[option];
            double input;
            
            do
            {
                input = promptForBankNote();
                if (isValidBankNote(input))
                {
                    moneyInserted += input;
                    enteredEnoughMoney = moneyInserted >= productChosen.getPrice();
                    
                    printPaymentStatus(moneyInserted, productChosen.getPrice());
                }
                else if(input != 0)
                {
                    Console.WriteLine("Invalid Banknote!\nPlease enter a valid one or enter 0 to interrupt the payment process.");
                    printAvailableCurrencies();
                }
                Console.WriteLine();

            } while (input != 0 && !enteredEnoughMoney);

            bool success = input != 0;
            remainder = success ? moneyInserted - productChosen.getPrice() : moneyInserted;

            return new PaymentResultImpl(success, remainder);
        }

        private double promptForBankNote()
        {
            Console.Write("INSERT BANKNOTE: ");
            return double.Parse(Console.ReadLine());
        }

        private bool isValidBankNote(double value)
        {
            return _bankNotesCache.ContainsKey(value);
        }

        private void printPaymentStatus(double totalMoneyInserted, double totalPrice)
        {
            Console.WriteLine($"MONEY INSERTED: {totalMoneyInserted} - TOTAL PRICE: {totalPrice} - DIFFERENCE LEFT: {totalPrice - totalMoneyInserted}");
        }
    }
}