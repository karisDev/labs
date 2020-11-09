using System.IO;
using System.Linq;
using System.Windows;

namespace laba3
{
    public partial class MainWindow
    {
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
            if (DequeEnqueueFirstButton.Content == "Save first")
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
    }
}
