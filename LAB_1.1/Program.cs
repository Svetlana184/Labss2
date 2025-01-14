try
{
    Console.WriteLine("введите действительную часть первого числа:");
    int x = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите мнимую часть первого числа:");
    int x_mn = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите действительную часть первого числа:");
    int y = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите мнимую часть первого числа:");
    int y_mn = int.Parse(Console.ReadLine()!);
    Complex complex = new Complex()
    {
        X = x,
        Y = y,
        X_mn = x_mn,
        Y_mn = y_mn
    };
    Console.WriteLine(complex);

    Console.WriteLine("введите числитель первого числа:");
    int a = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите знаменатель первого числа:");
    int b = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите числитель первого числа:");
    int c = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите знаменатель первого числа:");
    int d = int.Parse(Console.ReadLine()!);
    Rational rational = new Rational()
    {
        X = a,
        Y = b,
        X_zm = c,
        Y_zm = d
    };
    Console.WriteLine(rational);
}
catch
{
    Console.WriteLine("введите переменные правильно");
}
abstract class Pair
{
    public int X { get; set; }
    public int Y { get; set; }
    public abstract (int, int) Add();
    public abstract (int, int) Subtract();
    public abstract (int, int) Multiply();
    public abstract (int, int) Divide();
    public abstract bool Equal();
}

class Complex : Pair
{
    public int X_mn { get; set; }
    public int Y_mn { get; set; }
    public override (int, int) Add() => (X + X_mn, Y + Y_mn);

    public override (int, int) Divide()
    {
        return ((X * Y + X_mn * Y_mn), (X * Y_mn + X_mn * Y));
    }

    public override bool Equal() => X == Y && X_mn == Y_mn;

    public override (int, int) Multiply()
    {
        return ((X * Y - X_mn * Y_mn) / Y * Y + Y_mn * Y_mn, (X_mn * Y - X * Y_mn) / Y * Y + Y_mn * Y_mn);
    }

    public override (int, int) Subtract() => (X - X_mn, Y - Y_mn);
    public (int, int) Conj(int a, int b) => (a, -b);
    public override string? ToString()
    {
        return $"({X}, {X_mn}) + ({Y}, {Y_mn}) = {Add()}\n" +
            $"({X}, {X_mn}) - ({Y}, {Y_mn}) = {Subtract()}\n" +
            $"({X}, {X_mn}) * ({Y}, {Y_mn}) = {Multiply()}\n" +
            $"({X}, {X_mn}) / ({Y}, {Y_mn}) = {Divide()}\n" +
            $"({X}, {X_mn}) = ({Y}, {Y_mn}) - {Equal()}\n" +
            $"Сопряженное число для ({X}, {X_mn}) - {Conj(X, X_mn)}\n" +
            $"Сопряженное число для ({Y}, {Y_mn}) - {Conj(Y, Y_mn)}";
    }
}

class Rational : Pair
{
    public int X_zm { get; set; }
    public int Y_zm { get; set; }
    public override (int, int) Add() => ((X * Y_zm + X_zm + Y), X_zm * Y_zm);

    public override (int, int) Divide() => (X * Y_zm, Y * X_zm);

    public override bool Equal() => X == Y && X_zm == Y_zm;

    public override (int, int) Multiply() => (X * Y, X_zm + Y_zm);

    public override (int, int) Subtract() => ((X * Y_zm - X_zm + Y), X_zm * Y_zm);

    public bool Greate() => ((double)X / X_zm) > ((double)Y / Y_zm);

    public bool Less() => ((double)X / X_zm) < ((double)Y / Y_zm);

    public override string? ToString()
    {
        return $"({X}, {X_zm}) + ({Y}, {Y_zm}) = {Add()}\n" +
            $"({X}, {X_zm}) - ({Y}, {Y_zm}) = {Subtract()}\n" +
            $"({X}, {X_zm}) * ({Y}, {Y_zm}) = {Multiply()}\n" +
            $"({X}, {X_zm}) / ({Y}, {Y_zm}) = {Divide()}\n" +
            $"({X}, {X_zm}) = ({Y}, {Y_zm}) - {Equal()}\n";
    }
}