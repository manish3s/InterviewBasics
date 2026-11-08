namespace Test.OOPDemo
{
    // Interface example
    public interface IVehicle
    {
        string Start();
        string Stop();
    }

    public class Car : IVehicle
    {
        public string Start() => "Car started";
        public string Stop() => "Car stopped";
    }

    // Abstraction example: abstract Shape
    public abstract class Shape
    {
        public abstract double Area();
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double Area() => System.Math.PI * Radius * Radius;
    }
}
