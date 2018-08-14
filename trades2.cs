public class BuyData
{
    public Money PurchasePrice {get;set;}
    public DateTime TradeDate {get;set;}
}

public class SellData
{
    public decimal LotShareCount {get;set;}
    public Money SellPrice {get;set;}
    public DateTime CostBasisDate {get;set;}
}

public class Trade
{
    public string Ticker {get;set;}
    public decimal ShareCount {get;set;}
    public BuyData BuyData {get;set;}
    public SellData SellData {get;set;}
}

// this solves the 'only has the information it needs' problem, but breaks 