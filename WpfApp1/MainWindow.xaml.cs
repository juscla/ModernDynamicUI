namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Threading;

    using ModernControls.Controls.DynamicUi;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public enum MainMenu
        {
            Keyboard,
            Mouse,
            Packets,
            Haptics
        }

        class ListNode<T>
        {
            public T value { get; set; }
            public ListNode<T> next { get; set; }
        }

        ListNode<int> removeKFromList(ListNode<int> head, int k)
        {
            if (head.value == k)
            {
                head = head.next;
            }

            var current = head;

            while (current.next != null)
            {
                if (current.next.value == k)
                {
                    current.next = current.next.next;
                }
                else
                {
                    current = current.next;
                }
            }


            return head;
        }


        public MainWindow()
        {
            this.InitializeComponent();
            ListNode<int> head = null;

            foreach (var item in new[] { 3, 3 })
            {
                var current = new ListNode<int> { value = item, next = head };
                head = current;
            }

            removeKFromList(head, 3);

            var test = DynamicUserInterfaceFactory.Create(new Test());

            this.Frame.Content = test.BuildViewScrollable();

            new DispatcherTimer(
                TimeSpan.FromSeconds(1),
                DispatcherPriority.Input,
                (s, e) =>
                    {
                        Console.WriteLine(test.Instance.Size);
                    },
                this.Dispatcher).Start();
        }


        public override void OnMenuItemSelected(object s, Page e)
        {
            this.Frame.Content = e;
        }

        private void RunTest(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TestIu : IDynamicUserInterface
    {
        public TestIu(IEnumerable<Person> p)
        {

            this.Source = new ObservableCollection<Person>(p);
        }

        public bool Temp { get; set; }


        public ObservableCollection<Person> Source { get; set; }

        public FrameworkElement BuildView(bool hideReadonly = true)
        {
            var gridView = new GridView { AllowsColumnReorder = true };

            if (this.Source.Any())
            {
                foreach (var p in this.Source.First().GetType().GetProperties())
                {
                    gridView.Columns.Add(new GridViewColumn { Header = p.Name, DisplayMemberBinding = new Binding(p.Name) });
                }
            }

            var view = new ListView { View = gridView, Height = 200 };
            view.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = this.Source });

            return view;
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
