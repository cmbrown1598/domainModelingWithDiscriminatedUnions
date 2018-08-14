(* 

Discriminated unions and domain modeling. 

*)


// F# has objects and types, just like all .NET languages

let k = true;
// big deal, var k = true;


let l = "apple";
// so what, var l = "apple";

let m = 42.0m;

// zzzzz, what? Oh, yeah, that's nothing either
// var m = 42.0m;










/// OK, smarty pants.
/// 
/// Lexically equivalent to C# as :
/// public class ComplexNumber {
///    public ComplexNumber(int r, int i)
///    {
///       this.RealPart = r;
///       this.ImaginaryPart = i;
///    }
///    
///    public int RealPart {
///         get; private set;
///    }
///    
///    public int ImaginaryPart {
///         get; private set;
///    }
/// }
type ComplexNumber = { RealPart : int; ImaginaryPart : int }


/// Lexically Equivalent to:
/// 
/// public static ComplexNumber AddComplexNumbers(ComplexNumber complex1, ComplexNumber complex2) {
///     return new ComplexNumber(complex1.RealPart + complex2.RealPart, complex1.ImaginaryPart + complex1.ImaginaryPart);
/// }   
let addComplexNumbers complex1 complex2 = 
    { 
        RealPart = complex1.RealPart + complex2.RealPart ;
        ImaginaryPart = complex1.ImaginaryPart + complex2.ImaginaryPart
    }



/// A simple enum style type
/// public enum CardType
/// {
///     Clubs,
///     Diamonds,
///     Hearts,
///     Spades
/// }
/// 
type CardType = | Clubs = 0 | Diamonds  = 1 | Hearts = 2 | Spades = 3




//  what F# adds that C# doesn't is the union type

type Shape = 
    | Circle of Radius : float
    | Square of SideLength : float
    | Rectangle of LongSide: float * ShortSide : float
    | Polygon of Sides : float list
    | Line

// looks like an Enum, but acts like so much more / better.
// see lexically_equivalent.cs


// Why should you care?
// Brevity and conciseness.  One is 6 lines, one is 53. 
// Simplicity of setup.

let draw shape = 
    match shape with 
    | Circle r  -> r * System.Math.PI
    | Square s -> s ** 2.0
    | Rectangle (l,s) -> l * s
    | Polygon x -> List.sum x
    | Line -> 0.0

// What happens when you add a shape? 


// Why does this matter? Domain modeling!
// The practice of creating types gives you the ability to 'communicate' the business in code.

// Case study... Security trading.

type Money = {
    Value : decimal
    CurrencyCode : string
}

type Trade = 
    | Buy of Ticker : string * ShareCount : decimal * PurchasePrice : Money * TradeDate : System.DateTime
    | Sell of Ticker : string * ShareCount : decimal * LotShareCount : decimal * SellPrice : Money * CostBasisDate : System.DateTime

// notice, we have some similarities, but a fair amount of variation in the object itself.
// When done in C#, this is either 1) a base class with inheritors (trades1.cs)
// 2) a parent class with has-a style data attributes (trades2.cs)
// 3) or commonly, a single class, with reused data elements and defaulted (or nullable) data points (depending on need.) (trades3.cs)


let executeTrade trade = 
    match trade with 
    | Buy (ticker, shareCount, _, _) -> sprintf "You bought %f3 shares of %s" shareCount ticker
    | Sell (ticker, shareCount, lotShareCount, sellPrice, cbd) -> sprintf "You sold %f3 of your shares of %s that you had originally purchased on %s" shareCount ticker (cbd.ToShortDateString ())





let postTrades trades = 
    let array = trades |> 
                    List.map (fun trade -> 
                        match trade with 
                        | Buy (t, s, p, d) -> 
                            sprintf "%s,%f6,%s,%s,%f" t p.Value p.CurrencyCode (d.ToShortDateString()) s
                        | Sell (t, s, l, p, d) -> 
                            sprintf "%s,%f6,%s,%s,%f,%f2" t p.Value p.CurrencyCode (d.ToShortDateString()) s l
                    ) |> List.toArray 
    String.concat System.Environment.NewLine array











type Security =
    | DomesticEquity of Ticker : string // blahblah
    | ADR of Ticker : string // more blah s
    | InternationalEquity of Cusip : string

// what happens when we add fixed income securities

let getIdentifier = function | DomesticEquity t | ADR t -> t | InternationalEquity c -> c

type TradeV2 = 
    | Buy of Security * ShareCount : decimal * PurchasePrice : Money * TradeDate : System.DateTime
    | Sell of Security * ShareCount : decimal * LotShareCount : decimal * SellPrice : Money * CostBasisDate : System.DateTime

// Or short sells? How do we deal with them?

let buyBuyBuy (shareCount:decimal) = 
    Buy (DomesticEquity "APPL", shareCount, { CurrencyCode = "USD"; Value=104.81m }, (new System.DateTime(2018, 8, 14)))

let executeTradeV2 trade = 
    match trade with 
    | Buy (s, shareCount, _, _) -> sprintf "You bought %f3 shares of %s" shareCount (getIdentifier s)
    | Sell (s, shareCount, lotShareCount, sellPrice, cbd) -> sprintf "You sold %f3 of your shares of %s that you had originally purchased on %s" shareCount (getIdentifier s) (cbd.ToShortDateString ())

let postTradesV2 trades = 
    let array = trades |> List.map (fun trade -> 
                    match trade with 
                    | Buy (t, s, p, d) -> sprintf "%s,%f6,%s,%s,%f" (getIdentifier t) p.Value p.CurrencyCode (d.ToShortDateString()) s
                    | Sell (t, s, l, p, d) -> sprintf "%s,%f6,%s,%s,%f,%f2" (getIdentifier t) p.Value p.CurrencyCode (d.ToShortDateString()) s l
                ) |> List.toArray 
    String.concat System.Environment.NewLine array


(* 

Point to take home... instead of 'enums' to describe types, consider using F# discriminated unions to do so. 


Good book 
https://www.amazon.com/Domain-Modeling-Made-Functional-Domain-Driven/dp/1680502549/ref=sr_1_1?ie=UTF8&qid=1534285508&sr=8-1&keywords=domain+modeling+made+functional


)*


