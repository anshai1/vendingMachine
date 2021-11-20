namespace ConsoleApplication2.impl
{
    public class BankNoteImpl: BankNote
    {
        private double _value;

        public BankNoteImpl(double value)
        {
            this._value = value;
        }

        public double getValue()
        {
            return this._value;
        }
    }
}