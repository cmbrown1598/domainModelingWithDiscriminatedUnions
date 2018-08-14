public abstract class Trade
{
    public string Ticker {get;set;}
    public decimal ShareCount {get;set;}
}

public class BuyTrade : Trade 
{
    public Money PurchasePrice {get;set;}
    public DateTime TradeDate {get;set;}
}

public class SellTrade : Trade 
{
    public decimal LotShareCount {get;set;}
    public Money SellPrice {get;set;}
    public DateTime CostBasisDate {get;set;}
}

// easy enough to create a new type of trade, but difficult to validate against ()