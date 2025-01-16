
try
{
    Console.WriteLine("введите действительную часть первого числа:");
    int a = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите мнимую часть первого числа:");
    int b = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите действительную часть второго числа:");
    int c = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите мнимую часть второго числа:");
    int d = int.Parse(Console.ReadLine()!);
    Complex complex1 = new Complex
    {
        X = a,
        Y = b
    };
    Complex complex2 = new Complex
    {
        X = c,
        Y = d
    };
    Console.WriteLine("операции над комплексными числами:");
    Complex rez1 = complex1 + complex2;
    Console.WriteLine($"({complex1.X},{complex1.Y}) + ({complex2.X},{complex2.Y}) = ({rez1.X},{rez1.Y})");
    Complex rez2 = complex1 - complex2;
    Console.WriteLine($"({complex1.X},{complex1.Y}) - ({complex2.X},{complex2.Y}) = ({rez2.X},{rez2.Y})");
    Complex rez3 = complex1 * complex2;
    Console.WriteLine($"({complex1.X},{complex1.Y}) * ({complex2.X},{complex2.Y}) = ({rez3.X},{rez3.Y})");
    Complex rez4 = complex1 / complex2;
    Console.WriteLine($"({complex1.X},{complex1.Y}) / ({complex2.X},{complex2.Y}) = ({rez4.X},{rez4.Y})");
    Console.WriteLine($"({complex1.X},{complex1.Y}) = ({complex2.X},{complex2.Y}) - {complex1 == complex2}");
    Complex rez5 = complex1.Conj(complex1);
    Console.WriteLine($"сопряженное число для ({complex1.X},{complex1.Y}) - ({rez5.X}, {rez5.Y})");
    Complex rez6 = complex2.Conj(complex2);
    Console.WriteLine($"сопряженное число для ({complex2.X},{complex2.Y}) - ({rez6.X}, {rez6.Y})");
    Console.WriteLine();

    Console.WriteLine("введите числитель первого числа:");
    a = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите знаменатель первого числа:");
    b = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите числитель второго числа:");
    c = int.Parse(Console.ReadLine()!);
    Console.WriteLine("введите знаменатель второго числа:");
    d = int.Parse(Console.ReadLine()!);
    Rational rational1 = new Rational
    {
        X = a,
        Y = b
    };
    Rational rational2 = new Rational
    {
        X = c,
        Y = d
    };

    Console.WriteLine("операции над рациональными дробями:");
    Rational rez7 = rational1 + rational2;
    Console.WriteLine($"({rational1.X}/{rational1.Y}) + ({rational2.X}/{rational2.Y}) = ({rez7.X}/{rez7.Y})");
    Rational rez8 = rational1 - rational2;
    Console.WriteLine($"({rational1.X}/{rational1.Y}) - ({rational2.X}/{rational2.Y}) = ({rez8.X}/{rez8.Y})");
    Rational rez9 = rational1 * rational2;
    Console.WriteLine($"({rational1.X}/{rational1.Y}) * ({rational2.X}/{rational2.Y}) = ({rez9.X}/{rez9.Y})");
    Rational rez10 = rational1 / rational2;
    Console.WriteLine($"({rational1.X}/{rational1.Y}) / ({rational2.X}/{rational2.Y}) = ({rez10.X}/{rez10.Y})");
    Console.WriteLine($"({rational1.X}/{rational1.Y}) = {rational2.X}/{rational2.Y}) - ({rational1 == rational2}");
    Console.WriteLine($"({rational1.X}/{rational1.Y}) > {rational2.X}/{rational2.Y}) - ({rational1 > rational2}");
    Console.WriteLine($"({rational1.X}/{rational1.Y}) < {rational2.X}/{rational2.Y}) - ({rational1 < rational2}");
}
catch
{
    Console.WriteLine("Введите переменные правильно");
}
abstract class Pair
{
    public int X { get; set; }
    public int Y { get; set; }
}

class Complex : Pair
{
    public static Complex operator +(Complex c1, Complex c2)
    {
        return new Complex 
        { 
            X = c1.X + c1.Y,
            Y = c2.X + c2.Y
        };
    }
    public static Complex operator -(Complex c1, Complex c2)
    {
        return new Complex
        {
            X = c1.X - c1.Y,
            Y = c2.X - c2.Y
        };
    }
    public static Complex operator /(Complex c1, Complex c2)
    {
        return new Complex
        {
            X = c1.X * c2.X + c1.Y * c2.Y,
            Y = c1.X * c2.Y + c1.Y * c2.X
        };
    }
    public static Complex operator *(Complex c1, Complex c2)
    {
        return new Complex
        {
            X = (c1.X * c2.X - c1.Y * c2.Y)/(c2.X * c2.X + c2.Y * c2.Y),
            Y = (c1.Y * c2.X - c1.X * c2.Y) / (c2.X * c2.X + c2.Y * c2.Y)
        };
    }
    public static bool operator ==(Complex c1, Complex c2)
    {
        return c1.X == c2.X && c1.Y == c2.Y;
    }
    public static bool operator !=(Complex c1, Complex c2)
    {
        return c1.X != c2.X && c1.Y != c2.Y;
    }
    public Complex Conj(Complex c)
    {
        return new Complex
        {
            X = c.X,
            Y = -c.Y,
        };
    }

}

class Rational : Pair
{
    private static void Reduce(Rational r)
    {
        int c = r.Y;
        while (r.X >= 1 && r.Y >= 1 && c >= 1)
        {
            if (r.X % c == 0 && r.Y % c == 0)
            {
                r.X = r.X / c;
                r.Y = r.Y / c;
            }
            c--;
        }
    }
    public static Rational operator +(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return new Rational
        {
            X = r1.X * r2.Y + r1.Y * r2.X,
            Y = r1.Y * r2.Y
        };
    }
    public static Rational operator -(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return new Rational
        {
            X = r1.X * r2.Y - r1.Y * r2.X,
            Y = r1.Y * r2.Y
        };
    }
    public static Rational operator *(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return new Rational
        {
            X = r1.X * r2.X,
            Y = r1.Y * r2.Y
        };
    }
    public static Rational operator /(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return new Rational
        {
            X = r1.X * r2.Y,
            Y = r1.Y * r2.X
        };
    }
    public static bool operator ==(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return r1.X == r2.X && r1.Y == r2.Y;
    }
    public static bool operator !=(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return r1.X != r2.X && r1.Y != r2.Y;
    }
    public static bool operator >(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return ((double)r1.X / r1.Y) > ((double)r2.X / r2.Y);
    }
    public static bool operator <(Rational r1, Rational r2)
    {
        Reduce(r1);
        Reduce(r2);
        return ((double)r1.X / r1.Y) < ((double)r2.X / r2.Y);
    }
}