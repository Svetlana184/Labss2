Mutex mutexObj = new();
Console.Write("Введите название файла:");
string path = Console.ReadLine()!;
string path_2 = "простые_числа.txt";
string path_3 = "простые_числа_c_семёрками.txt";
Thread myThread1 = new(Task1Async);
myThread1.Start();
Thread myThread2 = new(Task2);
myThread2.Start();
Thread myThread3 = new(Task3);
myThread3.Start();


void Task1Async()
{
    mutexObj.WaitOne();
    Console.WriteLine("выполняется запись первого файла...");
    string text = "";
    Random random = new Random();
    for(int i =0; i < random.Next(10, 20); i++)
    {
        text += random.Next(1, 100) + " ";
    }
    text += "67 ";
    using (StreamWriter writer = new StreamWriter(path, false))
    {
        writer.WriteLine(text);
    }
    mutexObj.ReleaseMutex();
}
void Task2()
{
    mutexObj.WaitOne();
    Console.WriteLine("выполняется запись второго файла...");
    string text = "";
    using (StreamReader reader = new StreamReader(path))
    {
        text = reader.ReadToEnd();
    }
    int[] mas = text.Split(' ').SkipLast(1).Select(int.Parse).ToArray();
    string new_text = "";
    for(int i =0; i<mas.Length; i++)
    {
        int x = mas[i];
        bool simple = true;
        for(int j = 2;  j <= x/2; j++)
        {
            if (x % j == 0)
            {
                simple = false;
                break;
            }
        }
        if (simple) new_text += x + " ";
    }
    using (StreamWriter writer = new StreamWriter(path_2, false))
    {
        writer.WriteLine(new_text);
    }
    mutexObj.ReleaseMutex();
}
void Task3()
{
    mutexObj.WaitOne();
    Console.WriteLine("выполняется запись третьего файла...");
    string text = "";
    using (StreamReader reader = new StreamReader(path))
    {
        text = reader.ReadToEnd();
    }
    int[] mas = text.Split(' ').SkipLast(1).Select(int.Parse).ToArray();
    string new_text = "";
    for (int i = 0; i < mas.Length; i++)
    {
        int x = mas[i];
        if (x % 10 == 7) new_text += x + " ";
    }
    using (StreamWriter writer = new StreamWriter(path_3, false))
    {
        writer.WriteLine(new_text);
    }
    mutexObj.ReleaseMutex();
}
