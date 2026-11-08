namespace Test.OOPDemo
{
    // Inheritance example: Animal base class and derived types
    public class Animal
    {
        public virtual string Speak() => "Some sound";
    }

    public class Dog : Animal
    {
        public override string Speak() => "Woof";
    }

    public class Cat : Animal
    {
        public override string Speak() => "Meow";
    }
}
