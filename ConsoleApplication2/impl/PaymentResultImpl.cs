namespace ConsoleApplication2.impl
{
    public class PaymentResultImpl: PaymentResult
    {
        private bool _paymentSuccess;
        private double _remainder;

        public PaymentResultImpl(bool success, double reminder)
        {
            this._paymentSuccess = success;
            this._remainder = reminder;
        }
        
        public double getRemainder()
        {
            return _remainder;
        }

        public bool wasPaymentSuccessful()
        {
            return _paymentSuccess;
        }
    }
}