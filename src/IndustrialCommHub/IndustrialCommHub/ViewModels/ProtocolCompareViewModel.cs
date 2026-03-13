using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using IndustrialCommHub.Models;

namespace IndustrialCommHub.ViewModels;

public partial class ProtocolCompareViewModel : ObservableObject
{
    public ObservableCollection<ProtocolInfo> Protocols { get; } = new();

    public ProtocolCompareViewModel()
    {
        var data = new[]
        {
            new ProtocolInfo
            {
                Protocol    = "Modbus TCP",
                Speed       = "100 Mbps (Ethernet)",
                Topology    = "Star / Bus",
                MaxNodes    = "247 (Unit ID)",
                RealTime    = "낮음 (~ms)",
                MainUse     = "SCADA, PLC 범용 통신"
            },
            new ProtocolInfo
            {
                Protocol    = "EtherCAT",
                Speed       = "100 Mbps / 1 Gbps",
                Topology    = "Line (Daisy-chain)",
                MaxNodes    = "65,535",
                RealTime    = "매우 높음 (<100μs)",
                MainUse     = "고속 서보, CNC 모션"
            },
            new ProtocolInfo
            {
                Protocol    = "SSCNET III/H",
                Speed       = "150 Mbps (광)",
                Topology    = "Line (Optical)",
                MaxNodes    = "최대 16축",
                RealTime    = "매우 높음 (0.444ms)",
                MainUse     = "Mitsubishi 서보 네트워크"
            },
            new ProtocolInfo
            {
                Protocol    = "CC-Link IE",
                Speed       = "1 Gbps",
                Topology    = "Ring / Star",
                MaxNodes    = "120",
                RealTime    = "높음 (~1ms)",
                MainUse     = "Mitsubishi FA 필드버스"
            },
            new ProtocolInfo
            {
                Protocol    = "PROFINET",
                Speed       = "100 Mbps / 1 Gbps",
                Topology    = "Star / Ring",
                MaxNodes    = "제한 없음",
                RealTime    = "높음 (IRT: <1ms)",
                MainUse     = "Siemens FA, 유럽 표준"
            },
            new ProtocolInfo
            {
                Protocol    = "CC-Link (클래식)",
                Speed       = "10 Mbps",
                Topology    = "Bus (RS-485 기반)",
                MaxNodes    = "64",
                RealTime    = "중간 (~10ms)",
                MainUse     = "Mitsubishi PLC I/O 확장"
            },
            new ProtocolInfo
            {
                Protocol    = "DeviceNet",
                Speed       = "500 kbps",
                Topology    = "Bus (CAN 기반)",
                MaxNodes    = "64",
                RealTime    = "중간",
                MainUse     = "Allen-Bradley PLC, 센서"
            },
            new ProtocolInfo
            {
                Protocol    = "RS-485 / Modbus RTU",
                Speed       = "최대 10 Mbps",
                Topology    = "Multi-drop Bus",
                MaxNodes    = "32~256",
                RealTime    = "낮음~중간",
                MainUse     = "저비용 PLC, 인버터"
            },
        };
        foreach (var p in data) Protocols.Add(p);
    }
}
