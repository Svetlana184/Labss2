using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB_OS_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
            string text = "";
            //Mutex mutexObj = new();
            string path_1 = "fileone.txt";
            // запускаем пять потоков
            //for (int i = 1; i < 3; i++)
            //{

            //}

            Create_FileAsync();
            Easy_File();

            async Task Create_FileAsync()
            {
                //mutexObj.WaitOne();     // приостанавливаем поток до получения мьютекса
                using (StreamWriter writer = new StreamWriter(path_1, false))
                {
                    Random random = new Random();
                    for(int i = 0;  i < random.Next(10, 50); i++)
                    {
                        text += random.Next(1, 50) + " ";
                    }
                    await writer.WriteLineAsync(text);
                }
                //mutexObj.ReleaseMutex();    // освобождаем мьютекс
            }

            async Task Easy_File()
            {
                string text_fileone;
                using (StreamReader reader = new StreamReader(path_1))
                {
                    text_fileone = await reader.ReadToEndAsync();
                }
                int[] mas = text_fileone.Split(' ').Select(x => int.Parse(x)).ToArray();
                Text_output.Text = mas.ToString();
            }
        }
    }
}