using System.Windows;
using System.Windows.Controls;

namespace CrazyZoo.WpfApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MainViewModel viewModel = new MainViewModel();
        DataContext = viewModel;

        viewModel.LogMessages.CollectionChanged += (s, e) => ScrollToEnd(LogListBox);
        viewModel.ReportLines.CollectionChanged += (s, e) => ScrollToEnd(ReportListBox);
    }

    private static void ScrollToEnd(ListBox listBox)
    {
        if (listBox.Items.Count > 0)
        {
            listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
        }
    }
}