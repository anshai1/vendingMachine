using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using ConsoleApplication2.impl;

namespace ConsoleApplication2 {
    internal class Program {
        public static void Main(string[] args)
        {
            // Products
            Product Tedi = new ProductImpl("Tedi", 15);
            Product Strongbow = new ProductImpl("Strongbow", 20);
            Product Sprite = new ProductImpl("Sprite", 25);
            Product Cola = new ProductImpl("Cola", 35);
            List<Product> products = new List<Product> {Tedi, Strongbow, Sprite, Cola};

            // Currencies
            BankNote NICKEL = new BankNoteImpl(5);
            BankNote DIME = new BankNoteImpl(10);
            BankNote QUARTER = new BankNoteImpl(25);
            List<BankNote> bankNotes = new List<BankNote> {NICKEL, DIME, QUARTER};

            VendingMachine vendingMachine = new VendingMachine(products: products, bankNotes: bankNotes);

            vendingMachine.Start();
        }
    }
}