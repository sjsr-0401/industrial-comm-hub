using CommunityToolkit.Mvvm.ComponentModel;

namespace IndustrialCommHub.ViewModels;

public partial class PlcViewModel : ObservableObject
{
    public string MxBlockReadCode => @"// Mitsubishi MX Component - Block Read (C#)
using ActUtlTypeLib;

ActUtlType plc = new ActUtlType();
plc.ActLogicalStationNumber = 1;

int ret = plc.Open();
if (ret != 0) throw new Exception($""PLC Open Error: 0x{ret:X}"");

// D100~D109 (10 words) 읽기
short[] data = new short[10];
int readCount = 0;
ret = plc.ReadDeviceBlock(""D100"", 10, out readCount, out data[0]);

Console.WriteLine($""Read {readCount} words from D100"");
for (int i = 0; i < readCount; i++)
    Console.WriteLine($""D{100+i} = {data[i]}"");

plc.Close();";

    public string MxBlockWriteCode => @"// Mitsubishi MX Component - Block Write (C#)
using ActUtlTypeLib;

ActUtlType plc = new ActUtlType();
plc.ActLogicalStationNumber = 1;
plc.Open();

// D200~D204에 값 쓰기
short[] writeData = { 100, 200, 300, 400, 500 };
int ret = plc.WriteDeviceBlock(""D200"", 5, ref writeData[0]);

if (ret == 0) Console.WriteLine(""Write OK"");
else Console.WriteLine($""Write Error: 0x{ret:X}"");

plc.Close();";

    public string S7Code => @"// Siemens S7 - S7.Net (C#)
using S7.Net;

var plc = new Plc(CpuType.S71200, ""192.168.1.100"", 0, 1);
plc.Open();

// DB1.DBW0 읽기
var value = plc.Read(""DB1.DBW0"");
Console.WriteLine($""DB1.DBW0 = {value}"");

// M0.0 비트 읽기
bool bit = (bool)plc.Read(""M0.0"");
Console.WriteLine($""M0.0 = {bit}"");

// VW100에 값 쓰기 (S7-200)
plc.Write(""DB1.DBW2"", (short)1234);

plc.Close();";

    public List<DeviceTypeInfo> DeviceTypes { get; } = new()
    {
        new() { Symbol = "D", Name = "Data Register", Size = "16-bit Word", Description = "범용 데이터 저장 (D0~D7999)" },
        new() { Symbol = "M", Name = "Internal Relay", Size = "1-bit", Description = "보조 릴레이 (M0~M7679)" },
        new() { Symbol = "X", Name = "Input Relay", Size = "1-bit (Octal)", Description = "외부 입력 (X0~X1FFF)" },
        new() { Symbol = "Y", Name = "Output Relay", Size = "1-bit (Octal)", Description = "외부 출력 (Y0~Y1FFF)" },
        new() { Symbol = "W", Name = "Link Register", Size = "16-bit Word", Description = "CC-Link 링크 레지스터 (W0~W1FFF)" },
        new() { Symbol = "T", Name = "Timer", Size = "1-bit / 16-bit", Description = "타이머 (T0~T511)" },
        new() { Symbol = "C", Name = "Counter", Size = "1-bit / 16-bit", Description = "카운터 (C0~C255)" },
        new() { Symbol = "B", Name = "Link Relay", Size = "1-bit", Description = "CC-Link 링크 릴레이 (B0~B1FFF)" },
    };
}

public class DeviceTypeInfo
{
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
