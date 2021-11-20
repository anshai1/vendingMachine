using System;

namespace ConsoleApplication2.impl
{
    public class ProductImpl: Product
    {
        private String _name;
        private double _price;

        public ProductImpl(String name, double price)
        {
            this._name = name;
            this._price = price;
        }
        
        public string getName()
        {
            return this._name;
        }

        public double getPrice()
        {
            return this._price;
        }
    }
}