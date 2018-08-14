public class Trade
{
    public string Ticker {get;set;}
    public decimal ShareCount {get;set;}
    // for a buy, this is purchase price, for a sell, this is the sell price
    public Money Price {get;set;}
    // for a sell this is null
    public DateTime? TradeDate {get;set;}
    // for a buy these are null
    public decimal? LotShareCount {get;set;}
    public DateTime? CostBasisDate {get;set;}
}