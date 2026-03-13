public class BankStatementResult
{
    public ResultResponse? result { get; set; }
    public string? userId { get; set; }
    public string? id { get; set; }
}

public class ResultResponse
{
    public string? bankNameEN { get; set; }
    public string? bankNameTH { get; set; }
    //public List<TransactionResponse>? transactions { get; set; }

}

public class TransactionResponse
{
    public string? date { get; set; }
    public string? deposit { get; set; }
    public string? Amount { get; set; }
    public string? balance { get; set; }
    public confidenceResponse? confidence { get; set; }
}

public class confidenceResponse { 
    public string? date { get; set; }
    public string? balance { get; set; }
    public string? transactionDescription { get; set; }
}