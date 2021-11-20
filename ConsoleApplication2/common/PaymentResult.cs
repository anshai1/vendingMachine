namespace ConsoleApplication2
{
    public interface PaymentResult
    {
        bool wasPaymentSuccessful();
        double getRemainder();
    }
}