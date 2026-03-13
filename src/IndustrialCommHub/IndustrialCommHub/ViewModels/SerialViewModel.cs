using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace IndustrialCommHub.ViewModels;

public partial class SerialViewModel : ObservableObject
{
    [ObservableProperty] private string _selectedBaud = "9600";
    [ObservableProperty] private string _selectedParity = "None";
    [ObservableProperty] private string _selectedDataBits = "8";
    [ObservableProperty] private string _selectedStopBits = "1";
    [ObservableProperty] private string _sendText = "Hello PLC";
    [ObservableProperty] private string _terminal = string.Empty;
    [ObservableProperty] private bool _isHexMode = false;
    [ObservableProperty] private string _statusMessage = "Port Closed";

    public List<string> BaudRates { get; } = new() { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" };
    public List<string> Parities { get; } = new() { "None", "Odd", "Even", "Mark", "Space" };
    public List<string> DataBitsList { get; } = new() { "5", "6", "7", "8" };
    public List<string> StopBitsList { get; } = new() { "1", "1.5", "2" };

    [RelayCommand]
    private void SendLoopback()
    {
        if (string.IsNullOrWhiteSpace(SendText)) return;
        var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

        string txDisplay, rxDisplay;
        if (IsHexMode)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(SendText);
            var hex = string.Join(" ", bytes.Select(b => $"{b:X2}"));
            var stxEtx = $"02 {hex} 03";
            txDisplay = $"TX [{timestamp}]  STX {hex} ETX\n         HEX: {stxEtx}\n";
            rxDisplay = $"RX [{timestamp}]  STX {hex} ETX  [Loopback OK]\n";
        }
        else
        {
            txDisplay = $"TX [{timestamp}]  \x02{SendText}\x03  (STX...ETX framing)\n";
            rxDisplay = $"RX [{timestamp}]  \x02{SendText}\x03  [Loopback OK]\n";
        }

        Terminal += txDisplay;
        Terminal += rxDisplay;
        Terminal += "\n";
        StatusMessage = $"Loopback OK  |  {SelectedBaud} baud, {SelectedDataBits}{SelectedParity[0]}{SelectedStopBits}";
    }

    [RelayCommand]
    private void ClearTerminal()
    {
        Terminal = string.Empty;
        StatusMessage = "Terminal cleared";
    }

    [RelayCommand]
    private void ToggleHex() => IsHexMode = !IsHexMode;
}
