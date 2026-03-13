using System;
using System.ComponentModel.DataAnnotations;

public class OcrBankStatement
{
    [Key]
    public Guid Id { get; set; }

    public string? BankName { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountName { get; set; }

    public DateTime CreatedAt { get; set; }
}