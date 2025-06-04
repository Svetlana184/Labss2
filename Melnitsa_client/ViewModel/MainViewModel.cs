using Melnitsa_client.Infrastructure;
using Melnitsa_client.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Melnitsa_client.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            CancelButton = true;
            Color = "White";
        }
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

        private double angleMelnitsa;

        public double AngleMelnitsa
        {
            get { return angleMelnitsa; }
            set
            {
                angleMelnitsa = value;
                OnPropertyChanged(nameof(AngleMelnitsa));
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

        private bool cancelButton;

        public bool CancelButton
        {
            get { return cancelButton; }
            set
            {
                cancelButton = value;
                OnPropertyChanged(nameof(CancelButton));
            }
        }


        private RelayCommand? speedCommand;

        public RelayCommand SpeedCommand
        {
            get
            {
                return speedCommand ??
                    (speedCommand = new RelayCommand(async (o) =>
                    {

                        CancelButton = false;
                        
                        try
                        {
                            using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            await tcpClient.ConnectAsync("127.0.0.1", 8888);
                            byte[] data = new byte[512];
                            int bytes = await tcpClient.ReceiveAsync(data);
                            string speed_str = Encoding.UTF8.GetString(data, 0, bytes);
                            Speed = int.Parse(speed_str);
                            Clients_info = $"Скорость кручения мельницы: {Speed}";

                            Thread melnitsa_thread = new Thread(RotateMelnitsa);

                            melnitsa_thread.Start();
                            
                            void RotateMelnitsa()
                            {
                                while (true)
                                {
                                    if (AngleMelnitsa == 360) AngleMelnitsa = 0;
                                    AngleMelnitsa += 40;
                                    double time = Speed / 9.0 * 1000.0;
                                    Thread.Sleep((int)(time));
                                }
                               
                            }

                        }
                        catch (Exception ex)
                        {
                            Clients_info = ex.Message;
                            Color = "Pink";
                        }
                    }));
            }
        }
    }
}
