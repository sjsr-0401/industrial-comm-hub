using CommunityToolkit.Mvvm.ComponentModel;

namespace IndustrialCommHub.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public ModbusViewModel Modbus { get; } = new();
    public SerialViewModel Serial { get; } = new();
    public MotionViewModel Motion { get; } = new();
    public PlcViewModel Plc { get; } = new();
    public SecsGemViewModel SecsGem { get; } = new();
    public ProtocolCompareViewModel ProtocolCompare { get; } = new();
}
