class PaymentProcessor
{
    public void ProcessPayment(Product product, IPayment payment)
    {
        Console.WriteLine($"Product total price: {product.TotalPrice}");
        Console.WriteLine($"Paid: {payment.Amount}");
        
        // Your code goes here
        
    }

    public void PrintOutcome(string outcome)
    {
        switch (outcome)
        {
            case "Success": Console.WriteLine($"Payment successful"); return;
            case "Unsupported": Console.WriteLine($"Unsupported payment type"); return;
            case "AlreadyCompleted": Console.WriteLine($"Already processed"); return;
            case "IncorrectAmount": Console.WriteLine($"Incorrect amount paid"); return;
            default: Console.WriteLine("Unknown outcome"); return;
        }
    }
}
