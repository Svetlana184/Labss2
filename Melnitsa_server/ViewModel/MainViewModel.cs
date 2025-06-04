using Melnitsa_server.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics.Eventing.Reader;
using Melnitsa_server.ViewModel;
using System.Windows;


namespace Melnitsa_server.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
       

        

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private int speed;

        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }

        private string on;

        public string On
        {
            get { return on; }
            set
            {
                on = value;
                OnPropertyChanged(nameof(On));
            }
        }

        private string clients_info;

        public string Clients_info
        {
            get { return clients_info; }
            set
            {
                clients_info = value;
                OnPropertyChanged(nameof(Clients_info));
            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        private bool status_Button;

        public bool Status_Button
        {
            get { return status_Button; }
            set
            {
                status_Button = value;
                OnPropertyChanged(nameof(Status_Button));
            }
        }

        private string name_button;

        public string Name_button
        {
            get { return name_button; }
            set
            {
                name_button = value;
                OnPropertyChanged(nameof(Name_button));
            }
        }

        public MainViewModel()
        {
            Name_button = "Запустить сервер";
            Status_Button = false;
        }

        private RelayCommand? turnonCommand;

        public RelayCommand TurnonCommand
        {
            get
            {
                return turnonCommand ??
                    (turnonCommand = new RelayCommand(async (o) =>
                        {
                            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
                            using Socket tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            Random random = new Random();
                            try
                            {   if (Name_button == "Запустить сервер")
                                {
                                    tcpListener.Bind(ipPoint);
                                    tcpListener.Listen();
                                    Name_button = "Остановить сервер";
                                    On = "Сервер запущен. Ожидание подключений... ";
                                    Color = "gray";
                                    Error = "Ошибок не найдено";
                                    while (true)
                                    {

                                        using var tcpClient = await tcpListener.AcceptAsync();

                                        Speed = random.Next(1, 20);

                                        byte[] data = Encoding.UTF8.GetBytes(Speed.ToString());

                                        await tcpClient.SendAsync(data);

                                        Clients_info += $"Клиенту {tcpClient.RemoteEndPoint} отправлены данные - {Speed}\n";

                                    }
                                }
                                else
                                {
                                    tcpListener.Close();
                                    Name_button = "Сервер приостановлен";
                                    On = "";
                                    Error = "Для повторного включения сервера перезапустите приложение";
                                    Clients_info = "";
                                    Status_Button = true;
                                    
                                }
                                
                            }
                            catch (Exception ex)
                            {
                                Color = "red";
                                Error = ex.Message;
           
                            }
                        }));
            }
        }

        private RelayCommand? turnoffCommand;

        public RelayCommand TurnoffCommand
        {
            get
            {
                return turnoffCommand ??
                    (turnoffCommand = new RelayCommand((o) =>
                    {
                        Clients_info = "";
                    }));
            }
        }

    }
}
