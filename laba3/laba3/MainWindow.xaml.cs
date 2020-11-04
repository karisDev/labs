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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Fill="#FF5100FF" Fill="#FF7400FF"
    public partial class MainWindow : Window
    {
        public static int CurrentDataType = 0;
        static bool StackRunning = false;
        static bool DequeRunning = false;
        static bool QueueRunning = false;
        Stack StackData = new Stack();
        Queue QueueData = new Queue();
        Deque<object> DequeData = new Deque<object>();
        
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

        private void Stack(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"C:\\input\stack.txt") && !StackRunning)
            {
                StackRunning = true;
                int CountLines = File.ReadAllLines(@"C:\\input\stack.txt").Length;
                if (CountLines != 0)
                {
                    string temp;
                    using (StreamReader sr = new StreamReader(@"C:\\input\stack.txt"))
                    {
                        for (int i = 0; i < CountLines; i++)
                        {
                            temp = sr.ReadLine();
                            if (temp != null)
                            {
                                StackData.Push(temp);
                            }
                        }
                    }
                }
            }
            DataTypeUISwitch(1);
        }
        private void StackSearch(object sender, RoutedEventArgs e)
        {
            if(StackPushValue.Text == "")
            {
                StackLogs.Text = "enter what to search for in the box above";
                return;
            }
            
            if(StackData.Contains(StackPushValue.Text))
            {
                StackLogs.Text = "object '" + StackPushValue.Text + "' is inside the stack";
            }
            else
            {
                StackLogs.Text = "there's no object with '" + StackPushValue.Text + "' value";
            }
            StackPushValue.Text = "";
        }
        private void StackPush(object sender, RoutedEventArgs e)
        {
            if(StackPushValue.Text == "")
            {
                StackLogs.Text = "value cant be empty";
                return;
            }
            if(StackPushButton.Content == "Save")
            {
                StackPushButton.Content = "Push";
            }
            StackData.Push(StackPushValue.Text);
            StackLogs.Text = '"' + StackPushValue.Text + '"' + " was written on top";
            StackPushValue.Text = "";
        }
        private void StackPop(object sender, RoutedEventArgs e)
        {
            if(StackData.Count == 0)
            {
                StackLogs.Text = "no any data left to pop from stack";
                return;
            }
            StackLogs.Text = StackData.Pop().ToString() + " was removed from stack";
        }
        private void StackPeek(object sender, RoutedEventArgs e)
        {
            if (StackData.Count == 0)
            {
                StackLogs.Text = "stack is empty";
                return;
            }
            StackLogs.Text = '"' +  StackData.Peek().ToString() + '"' + " is on top of the stack";
        }
        private void StackCount(object sender, RoutedEventArgs e)
        {
            switch (StackData.Count)
            {
                case 0:
                    StackLogs.Text = "queue is empty";
                    break;
                case 1:
                    StackLogs.Text = "there is 1 object in stack";
                    break;
                default:
                    StackLogs.Text = "there are " + StackData.Count + " objects in stack";
                    break;
            }
        }
        private void StackEdit(object sender, RoutedEventArgs e)
        {
            if(StackData.Count == 0)
            {
                StackLogs.Text = "you can't edit an empty stack";
                return;
            }
            if (StackPushButton.Content == "Save")
            {
                return;
            }
            StackPushValue.Text = StackData.Pop().ToString();
            StackLogs.Text = "click 'Save' button to save new value";
            StackPushButton.Content = "Save";
        }
        private void Deque(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"C:\\input\deque.txt") && !DequeRunning)
            {
                DequeRunning = true;
                int CountLines = File.ReadAllLines(@"C:\\input\deque.txt").Length;
                if (CountLines != 0)
                {
                    string temp;
                    using (StreamReader sr = new StreamReader(@"C:\\input\deque.txt"))
                    {
                        for (int i = 0; i < CountLines; i++)
                        {
                            temp = sr.ReadLine();
                            if (temp != null)
                            {
                                DequeData.AddLast(temp);
                            }
                        }
                    }
                }
            }
            DataTypeUISwitch(2);
        }
        private void DequeEnqueueFirst(object sender, RoutedEventArgs e)
        {
            if (DequeEnqueueValue.Text == "")
            {
                DequeLogs.Text = "value cant be empty";
                return;
            }
            if (DequeEnqueueFirstButton.Content == "Save first")
            {
                DequeEnqueueFirstButton.FontSize = 17;
                DequeEnqueueLastButton.FontSize = 17;
                DequeEnqueueFirstButton.Content = "Enqueue first";
                DequeEnqueueLastButton.Content = "Enqueue last";
            }
            DequeData.AddFirst(DequeEnqueueValue.Text);
            DequeLogs.Text = '"' + DequeEnqueueValue.Text + '"' + " was written as a first element";
            DequeEnqueueValue.Text = "";
        }
        private void DequeEnqueueLast(object sender, RoutedEventArgs e)
        {
            if (DequeEnqueueValue.Text == "")
            {
                DequeLogs.Text = "value cant be empty";
                return;
            }
            if (DequeEnqueueLastButton.Content == "Save last")
            {
                DequeEnqueueFirstButton.FontSize = 17;
                DequeEnqueueLastButton.FontSize = 17;
                DequeEnqueueFirstButton.Content = "Enqueue first";
                DequeEnqueueLastButton.Content = "Enqueue last";
            }
            DequeData.AddLast(DequeEnqueueValue.Text);
            DequeLogs.Text = '"' + DequeEnqueueValue.Text + '"' + " was written as a last element";
            DequeEnqueueValue.Text = "";
        }
        private void DequePeekFirst(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "deque is empty";
                return;
            }
            DequeLogs.Text = '"' + DequeData.First().ToString() + '"' + " is in the beginning of the deque";
        }
        private void DequePeekLast(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "deque is empty";
                return;
            }
            DequeLogs.Text = '"' + DequeData.Last().ToString() + '"' + " is in the ending of the deque";
        }
        private void DequeEditFirst(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "you can't edit an empty deque";
                return;
            }
            if (DequeEnqueueFirstButton.Content == "Save first")
            {
                return;
            }
            DequeEnqueueFirstButton.FontSize = 20;
            DequeEnqueueLastButton.FontSize = 20;
            DequeEnqueueValue.Text = DequeData.RemoveFirst().ToString();
            DequeLogs.Text = "click 'Save first' or 'Save last' button to save new value";
            DequeEnqueueFirstButton.Content = "Save first";
            DequeEnqueueLastButton.Content = "Save last";
        }
        private void DequeEditLast(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "you can't edit an empty deque";
                return;
            }
            if(DequeEnqueueFirstButton.Content == "Save first")
            {
                return;
            }
            DequeEnqueueFirstButton.FontSize = 20;
            DequeEnqueueLastButton.FontSize = 20;
            DequeEnqueueValue.Text = DequeData.RemoveLast().ToString();
            DequeLogs.Text = "click 'Save first' or 'Save last' button to save new value";
            DequeEnqueueFirstButton.Content = "Save first";
            DequeEnqueueLastButton.Content = "Save last";
        }
        private void DequeDequeueFirst(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "no any data left to remove from deque";
                return;
            }
            DequeLogs.Text = DequeData.RemoveFirst().ToString() + " was removed from the beginning of deque";
        }
        private void DequeDequeueLast(object sender, RoutedEventArgs e)
        {
            if (DequeData.Count == 0)
            {
                DequeLogs.Text = "no any data left to remove from deque";
                return;
            }
            DequeLogs.Text = DequeData.RemoveLast().ToString() + " was removed from the ending of deque";
        }
        private void DequeSearch(object sender, RoutedEventArgs e)
        {
            if (DequeEnqueueValue.Text == "")
            {
                DequeLogs.Text = "enter what to search for in the box above";
                return;
            }

            if (DequeData.Contains(DequeEnqueueValue.Text))
            {
                DequeLogs.Text = "object '" + DequeEnqueueValue.Text + "' is inside the deque";
            }
            else
            {
                DequeLogs.Text = "there's no object with '" + DequeEnqueueValue.Text + "' value";
            }
            DequeEnqueueValue.Text = "";
        }
        private void DequeCount(object sender, RoutedEventArgs e)
        {
            switch (DequeData.Count)
            {
                case 0:
                    DequeLogs.Text = "deque is empty";
                    break;
                case 1:
                    DequeLogs.Text = "there is 1 object in deque";
                    break;
                default:
                    DequeLogs.Text = "there are " + DequeData.Count + " objects in deque";
                    break;
            }
        }
        private void Queue(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"C:\\input\queue.txt") && !QueueRunning)
            {
                QueueRunning = true;
                int CountLines = File.ReadAllLines(@"C:\\input\queue.txt").Length;
                if (CountLines != 0)
                {
                    string temp;
                    using (StreamReader sr = new StreamReader(@"C:\\input\queue.txt"))
                    {
                        for (int i = 0; i < CountLines; i++)
                        {
                            temp = sr.ReadLine();
                            if (temp != null)
                            {
                                QueueData.Enqueue(temp);
                            }
                        }
                    }
                }
            }
            DataTypeUISwitch(3);
        }
        private void QueueSearch(object sender, RoutedEventArgs e)
        {
            if (QueueEnqueueValue.Text == "")
            {
                QueueLogs.Text = "enter what to search for in the box above";
                return;
            }

            if (QueueData.Contains(QueueEnqueueValue.Text))
            {
                QueueLogs.Text = "object '" + QueueEnqueueValue.Text + "' is inside the queue";
            }
            else
            {
                QueueLogs.Text = "there's no object with '" + QueueEnqueueValue.Text + "' value";
            }
            QueueEnqueueValue.Text = "";
        }
        private void QueueEnqueue(object sender, RoutedEventArgs e)
        {
            if (QueueEnqueueValue.Text == "")
            {
                QueueLogs.Text = "value cant be empty";
                return;
            }
            if (QueueEnqueueButton.Content == "Save")
            {
                QueueEnqueueButton.Content = "Enqueue";
            }
            QueueData.Enqueue(QueueEnqueueValue.Text);
            QueueLogs.Text = '"' + QueueEnqueueValue.Text + '"' + " was written in the end";
            QueueEnqueueValue.Text = "";
        }
        private void QueueDequeue(object sender, RoutedEventArgs e)
        {
            if (QueueData.Count == 0)
            {
                QueueLogs.Text = "no any data left to remove from queue";
                return;
            }
            QueueLogs.Text = QueueData.Dequeue().ToString() + " was removed from the beginning of queue";
        }
        private void QueuePeek(object sender, RoutedEventArgs e)
        {
            if (QueueData.Count == 0)
            {
                QueueLogs.Text = "queue is empty";
                return;
            }
            QueueLogs.Text = '"' + QueueData.Peek().ToString() + '"' + " is in the beginning of the queue";
        }
        private void QueueCount(object sender, RoutedEventArgs e)
        {
            switch (QueueData.Count)
            {
                case 0:
                    QueueLogs.Text = "queue is empty";
                    break;
                case 1:
                    QueueLogs.Text = "there is 1 object in queue";
                    break;
                default:
                    QueueLogs.Text = "there are " + QueueData.Count + " objects in queue";
                    break;
            }
        }
        private void QueueEdit(object sender, RoutedEventArgs e)
        {
            if (QueueData.Count == 0)
            {
                QueueLogs.Text = "you can't edit an empty queue";
                return;
            }
            if (QueueEnqueueButton.Content == "Save")
            {
                return;
            }
            QueueEnqueueValue.Text = QueueData.Dequeue().ToString();
            QueueLogs.Text = "since you can't look at the last element in queue we will edit first\nclick 'Save' button to save new value to the end";
            QueueEnqueueButton.Content = "Save";
        }
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
