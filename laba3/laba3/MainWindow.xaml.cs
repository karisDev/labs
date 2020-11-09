using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba3
{
    public partial class MainWindow : Window
    {
        public static int CurrentDataType = 0;
        static bool StackRunning = false;
        static bool DequeRunning = false;
        static bool QueueRunning = false;
        Stack StackData = new Stack();
        Deque<object> DequeData = new Deque<object>();
        Queue QueueData = new Queue();
        
        private void DataTypeUISwitch(int NewType)
        {
            if(CurrentDataType == NewType)
            {
                return;
            }
            DataTypeBegger.Visibility = Visibility.Hidden;
            switch (CurrentDataType)
            {
                case 0:
                    SaveButton.Visibility = Visibility.Visible;
                    break;
                case 1:
                    StackContainer.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    DequeContainer.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    QueueContainer.Visibility = Visibility.Hidden;
                    break;
            }
            switch (NewType)
            {
                case 0:
                    DataTypeBegger.Visibility = Visibility.Visible;
                    CurrentDataType = 0;
                    SaveButton.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    CurrentDataType = 1;
                    StackContainer.Visibility = Visibility.Visible;
                    break;
                case 2:
                    CurrentDataType = 2;
                    DequeContainer.Visibility = Visibility.Visible;
                    break;
                case 3:
                    CurrentDataType = 3;
                    QueueContainer.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            string[] array;
            switch(CurrentDataType)
            {
                case 1:
                    StackRunning = false;
                    array = Array.ConvertAll(StackData.ToArray(), Convert.ToString);
                    using (StreamWriter sw = new StreamWriter(@"C:\\input\stack.txt", false))
                    {
                        foreach(string obj in array)
                        {
                            sw.WriteLine(obj);
                        }
                        sw.Close();
                    }
                    StackLogs.Text = "logs will appear here";
                    StackData = new Stack();
                    MessageBox.Show(@"C:\\input\stack.txt was saved");
                    break;
                case 2:
                    DequeRunning = false;
                    array = Array.ConvertAll(DequeData.ToArray(), Convert.ToString);
                    using (StreamWriter sw = new StreamWriter(@"C:\\input\deque.txt", false))
                    {
                        foreach (string obj in array)
                        {
                            sw.WriteLine(obj);
                        }
                        sw.Close();
                    }
                    DequeLogs.Text = "logs will appear here";
                    DequeData = new Deque<object>();
                    MessageBox.Show(@"C:\\input\deque.txt was saved");
                    break;
                case 3:
                    QueueRunning = false;
                    array = Array.ConvertAll(QueueData.ToArray(), Convert.ToString);
                    using (StreamWriter sw = new StreamWriter(@"C:\\input\queue.txt", false))
                    {
                        foreach (string obj in array)
                        {
                            sw.WriteLine(obj);
                        }
                        sw.Close();
                    }
                    QueueLogs.Text = "logs will appear here";
                    QueueData = new Queue();
                    MessageBox.Show(@"C:\\input\queue.txt was saved");
                    break;
            }
            DataTypeUISwitch(0);
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
