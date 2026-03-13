using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IndustrialCommHub.Models;

namespace IndustrialCommHub.ViewModels;

public partial class ModbusViewModel : ObservableObject
{
    [ObservableProperty] private string _statusMessage = "Ready";
    [ObservableProperty] private string _txId = "0001";
    [ObservableProperty] private string _protocolId = "0000";
    [ObservableProperty] private string _length = "0006";
    [ObservableProperty] private string _unitId = "01";
    [ObservableProperty] private string _selectedRegisterType = "Holding Register";

    public ObservableCollection<ModbusRegister> Registers { get; } = new();
    public ObservableCollection<FunctionCodeInfo> FunctionCodes { get; } = new();

    public List<string> RegisterTypes { get; } = new()
    {
        "Holding Register", "Input Register", "Coil", "Discrete Input"
    };

    public ModbusViewModel()
    {
        InitializeFunctionCodes();
        SimulateRead();
    }

    private void InitializeFunctionCodes()
    {
        var codes = new[]
        {
            new FunctionCodeInfo { Code = "FC01", Name = "Read Coils", Description = "1~2000개 코일 읽기", DataType = "Bit (1-bit)" },
            new FunctionCodeInfo { Code = "FC02", Name = "Read Discrete Inputs", Description = "1~2000개 디스크릿 입력 읽기", DataType = "Bit (1-bit)" },
            new FunctionCodeInfo { Code = "FC03", Name = "Read Holding Registers", Description = "1~125개 홀딩 레지스터 읽기", DataType = "Word (16-bit)" },
            new FunctionCodeInfo { Code = "FC04", Name = "Read Input Registers", Description = "1~125개 입력 레지스터 읽기", DataType = "Word (16-bit)" },
            new FunctionCodeInfo { Code = "FC05", Name = "Write Single Coil", Description = "단일 코일 쓰기 (0x0000/0xFF00)", DataType = "Bit (1-bit)" },
            new FunctionCodeInfo { Code = "FC06", Name = "Write Single Register", Description = "단일 홀딩 레지스터 쓰기", DataType = "Word (16-bit)" },
            new FunctionCodeInfo { Code = "FC07", Name = "Read Exception Status", Description = "예외 상태 8개 읽기", DataType = "Byte (8-bit)" },
            new FunctionCodeInfo { Code = "FC08", Name = "Diagnostics", Description = "진단 서브펑션 (에코, 카운터 등)", DataType = "Various" },
            new FunctionCodeInfo { Code = "FC11", Name = "Get Comm Event Counter", Description = "통신 이벤트 카운터 반환", DataType = "Word (16-bit)" },
            new FunctionCodeInfo { Code = "FC12", Name = "Get Comm Event Log", Description = "통신 이벤트 로그 반환", DataType = "Byte array" },
            new FunctionCodeInfo { Code = "FC15", Name = "Write Multiple Coils", Description = "1~1968개 코일 쓰기", DataType = "Bit (1-bit)" },
            new FunctionCodeInfo { Code = "FC16", Name = "Write Multiple Registers", Description = "1~123개 홀딩 레지스터 쓰기", DataType = "Word (16-bit)" },
        };
        foreach (var c in codes) FunctionCodes.Add(c);
    }

    [RelayCommand]
    private void SimulateRead()
    {
        Registers.Clear();
        var rng = new Random();
        var type = SelectedRegisterType;
        int count = type.Contains("Coil") || type.Contains("Discrete") ? 16 : 10;

        for (int i = 0; i < count; i++)
        {
            Registers.Add(new ModbusRegister
            {
                Address = 40001 + i,
                Type = type,
                Name = GetRegisterName(type, i),
                Value = type.Contains("Coil") || type.Contains("Discrete")
                    ? rng.Next(0, 2)
                    : rng.Next(0, 32768)
            });
        }
        StatusMessage = $"[{DateTime.Now:HH:mm:ss}] Read OK — {count} registers from {type}";
    }

    [RelayCommand]
    private void SimulateWrite()
    {
        if (Registers.Count == 0) return;
        var rng = new Random();
        foreach (var reg in Registers)
        {
            reg.Value = reg.Type.Contains("Coil") || reg.Type.Contains("Discrete")
                ? rng.Next(0, 2)
                : rng.Next(0, 32768);
        }
        // Refresh
        var tmp = Registers.ToList();
        Registers.Clear();
        foreach (var r in tmp) Registers.Add(r);
        StatusMessage = $"[{DateTime.Now:HH:mm:ss}] Write OK — {Registers.Count} registers updated";
    }

    private static string GetRegisterName(string type, int index) => type switch
    {
        "Holding Register" => index switch
        {
            0 => "Speed Setpoint",
            1 => "Torque Limit",
            2 => "Acceleration",
            3 => "Deceleration",
            4 => "Position Target",
            5 => "Control Word",
            6 => "Status Word",
            7 => "Fault Code",
            8 => "Temperature",
            9 => "Firmware Ver",
            _ => $"HR_{index:D4}"
        },
        "Input Register" => index switch
        {
            0 => "Actual Speed",
            1 => "Actual Torque",
            2 => "Actual Position",
            3 => "DC Bus Voltage",
            4 => "Output Current",
            _ => $"IR_{index:D4}"
        },
        _ => $"Coil_{index:D4}"
    };

    partial void OnSelectedRegisterTypeChanged(string value) => SimulateRead();
}
