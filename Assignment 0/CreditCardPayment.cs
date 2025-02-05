class CreditCardPayment
{
    public double Amount;
    public string HolderName;
    public bool IsProcessed;
    public string CardNumber;

    public CreditCardPayment(double amount, string holderName, string cardNumber)
    {
        Amount = amount;
        HolderName = holderName;
        CardNumber = cardNumber;
    }


}
