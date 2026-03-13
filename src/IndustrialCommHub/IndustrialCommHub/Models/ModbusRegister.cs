namespace IndustrialCommHub.Models;

public class ModbusRegister
{
    public int Address { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Value { get; set; }
    public string HexValue => $"0x{Value:X4}";
    public string BinaryValue => Convert.ToString(Value, 2).PadLeft(16, '0');
}

public class FunctionCodeInfo
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
}
