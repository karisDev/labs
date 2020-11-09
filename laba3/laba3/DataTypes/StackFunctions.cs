using System.IO;
using System.Windows;

namespace laba3
{
    public partial class MainWindow
    {
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
            if (StackPushValue.Text == "")
            {
                StackLogs.Text = "enter what to search for in the box above";
                return;
            }

            if (StackData.Contains(StackPushValue.Text))
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
            if (StackPushValue.Text == "")
            {
                StackLogs.Text = "value cant be empty";
                return;
            }
            if (StackPushButton.Content == "Save")
            {
                StackPushButton.Content = "Push";
            }
            StackData.Push(StackPushValue.Text);
            StackLogs.Text = '"' + StackPushValue.Text + '"' + " was written on top";
            StackPushValue.Text = "";
        }
        private void StackPop(object sender, RoutedEventArgs e)
        {
            if (StackData.Count == 0)
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
            StackLogs.Text = '"' + StackData.Peek().ToString() + '"' + " is on top of the stack";
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
            if (StackData.Count == 0)
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
    }
}
