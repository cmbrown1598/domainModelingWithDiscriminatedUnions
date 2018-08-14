 public abstract class Shape()
 {
 }
 
 public class Circle : Shape {
     public Circle(float radius) {
         this.Radius = radius;
     }

     public float Radius {
        get; private set;
 }
 
 public class Square : Shape {
     public Square(float sideLength) {
         this.SideLength = sideLength;
     }
 
     public float SideLength {
        get; private set;
     }
 }
 
 
 public class Rectangle : Shape {
     public Rectangle(float longSideLength, float shortSideLength) {
         this.LongSide = longSideLength;
         this.ShortSide = shortSideLength;
     }
 
     public float LongSide {
        get; private set;
     }
     public float ShortSide {
        get; private set;
     }
 }
 
 public class Polygon : Shape {
     public Polygon(System.Collections.Generic.IReadOnlyList<float> sides) {
         this.Sides = sides;
     }
 
     public System.Collections.Generic.IReadOnlyList<float> Sides {
        get; private set;
     }
 }
 
 public class Line : Shape {
     public Line() {
     }
 }
 








 public static class ShapeMatcher{
     public static float Draw(Shape s)
     {
        if(s as Circle != null)
        {
            return (s as Circle).Radius * System.Math.PI;
        }
        if(s as Square != null)
        {
            return System.Math.Pow((s as Square).SideLength, 2.0); 
        }
        if(s as Rectangle != null)
        {
            return (s as Rectangle).ShortSide * (s as Rectangle).LongSide; 
        }
        if(s as Polygon != null)
        {
            return (s as Polygon).Sides.Sum(); 
        }
        if(s as Line != null)
        {
            return 0.0; 
        }
     }

 }