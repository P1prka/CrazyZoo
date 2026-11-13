using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CrazyZoo.WpfApp;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;

        viewModel.LogMessages.CollectionChanged += (s, e) => ScrollToEnd(LogListBox);
        viewModel.ReportLines.CollectionChanged += (s, e) => ScrollToEnd(ReportListBox);
    }

    private void ScrollToEnd(ListBox listBox)
    {
        Dispatcher.InvokeAsync(() =>
        {
            if (listBox.Items.Count > 0)
            {
                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            }
        }, DispatcherPriority.Background);
    }
}



