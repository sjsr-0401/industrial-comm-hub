using System.Windows;
using System.Windows.Controls;
using IndustrialCommHub.ViewModels;

namespace IndustrialCommHub.Views;

public partial class SecsGemView : UserControl
{
    public SecsGemView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is SecsGemViewModel vm)
        {
            StateModelText.Text = vm.EquipmentStateModel;
            PmcCtcText.Text = vm.PmcCtcHostDiagram;
        }
    }
}
