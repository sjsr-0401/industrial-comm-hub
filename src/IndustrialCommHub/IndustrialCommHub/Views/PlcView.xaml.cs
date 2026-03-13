using System.Windows;
using System.Windows.Controls;
using IndustrialCommHub.ViewModels;

namespace IndustrialCommHub.Views;

public partial class PlcView : UserControl
{
    public PlcView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is PlcViewModel vm)
        {
            BlockReadCode.Text = vm.MxBlockReadCode;
            BlockWriteCode.Text = vm.MxBlockWriteCode;
            S7Code.Text = vm.S7Code;
        }
    }
}
