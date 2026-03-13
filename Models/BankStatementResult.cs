public class BankStatementResult
{
    public string BankName { get; set; }
    public string AccountNumber { get; set; }
    public string AccountName { get; set; }
    public List<Transaction> Transactions { get; set; }
}

public class Transaction
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public decimal Balance { get; set; }
}