using System.IO;
using System.Windows;

namespace laba3
{
    public partial class MainWindow
    {
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
    }
}
