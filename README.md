# Industrial Communication Hub

> **산업용 모션/통신 프로토콜 레퍼런스 & 시뮬레이터** — Portfolio Edition

![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)
![WPF](https://img.shields.io/badge/UI-WPF-0078D7?style=flat-square&logo=windows)
![MVVM](https://img.shields.io/badge/Pattern-MVVM-4CAF50?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-yellow?style=flat-square)

반도체/디스플레이/FA 장비 개발에서 사용하는 주요 산업용 통신 프로토콜을 한눈에 참조하고, 시뮬레이션해볼 수 있는 WPF 데스크톱 애플리케이션입니다.

---

## 📸 Features

### 탭 구성

| 탭 | 내용 |
|---|---|
| 📡 **Modbus TCP/RTU** | MBAP 헤더 구조, FC01~FC16 레퍼런스, 레지스터 뷰어 (시뮬레이션) |
| 🔌 **Serial RS-232/485** | COM 포트 설정, 터미널 뷰 (Hex/ASCII), STX/ETX 프레이밍, Loopback 시뮬레이션 |
| ⚡ **Motion Control** | EtherCAT/SSCNET/PMAC 개요, 모션 프로파일, PID 튜닝 + ScottPlot 실시간 차트 |
| 🏭 **PLC Communication** | Mitsubishi MX Component 레퍼런스, 디바이스 타입 테이블, Siemens S7 개요 |
| 💎 **SECS/GEM** | SEMI E5/E30/E37 설명, GEM 장비 상태 다이어그램, PMC↔CTC↔Host 구조도 |
| 📊 **Protocol Comparison** | Modbus vs EtherCAT vs SSCNET vs CC-Link vs PROFINET 비교표 |

### UI
- 다크 테마 (`#1E1E1E` 배경, `#0078D7` 강조)
- MVVM 패턴 (`CommunityToolkit.Mvvm`)
- PID Step Response 실시간 차트 (`ScottPlot 5.x`)

---

## 🛠️ Tech Stack

| 항목 | 내용 |
|---|---|
| Framework | .NET 8 / WPF (`net8.0-windows`) |
| Pattern | MVVM (CommunityToolkit.Mvvm 8.x) |
| Charting | ScottPlot.WPF 5.x |
| Language | C# 12 |
| IDE | Visual Studio 2022 / Rider |

---

## 🚀 Build & Run

### Prerequisites
- .NET 8 SDK ([download](https://dotnet.microsoft.com/download/dotnet/8.0))
- Windows 10/11 (WPF는 Windows 전용)

### Clone & Build
```bash
git clone https://github.com/sjsr-0401/industrial-comm-hub.git
cd industrial-comm-hub
dotnet build
```

### Run
```bash
dotnet run --project src/IndustrialCommHub/IndustrialCommHub/IndustrialCommHub.csproj
```

또는 Visual Studio 2022에서 `IndustrialCommHub.sln` 열고 F5.

---

## 🏗️ Architecture

```
industrial-comm-hub/
├── IndustrialCommHub.sln
└── src/
    └── IndustrialCommHub/
        └── IndustrialCommHub/
            ├── App.xaml                   # 글로벌 다크 테마 스타일
            ├── MainWindow.xaml            # TabControl 6탭
            ├── Models/
            │   ├── ModbusRegister.cs      # 레지스터/FC 모델
            │   └── ProtocolInfo.cs        # 프로토콜 비교 모델
            ├── ViewModels/
            │   ├── MainViewModel.cs
            │   ├── ModbusViewModel.cs     # FC 테이블, 레지스터 시뮬레이션
            │   ├── SerialViewModel.cs     # Loopback 시뮬레이션
            │   ├── MotionViewModel.cs     # PID 계산 + PlotUpdate 이벤트
            │   ├── PlcViewModel.cs        # MX Component 예제 코드
            │   ├── SecsGemViewModel.cs    # 상태 다이어그램 텍스트
            │   └── ProtocolCompareViewModel.cs
            └── Views/
                ├── ModbusView.xaml
                ├── SerialView.xaml
                ├── MotionView.xaml        # ScottPlot PID 차트
                ├── PlcView.xaml
                ├── SecsGemView.xaml
                └── ProtocolCompareView.xaml
```

### MVVM 바인딩 구조
```
MainViewModel
├── ModbusViewModel  ←→ ModbusView
├── SerialViewModel  ←→ SerialView
├── MotionViewModel  ←→ MotionView  (Event: PlotUpdateRequested)
├── PlcViewModel     ←→ PlcView
├── SecsGemViewModel ←→ SecsGemView
└── ProtocolCompareViewModel ←→ ProtocolCompareView
```

---

## 📡 Supported Protocols Reference

| Protocol | Standard | Speed | Realtime |
|---|---|---|---|
| Modbus TCP | IEC 61158 | 100Mbps | Low (ms) |
| Modbus RTU | EIA-485 | ≤10Mbps | Medium |
| EtherCAT | IEC 61158 | 100Mbps~1Gbps | Very High (<100μs) |
| SSCNET III/H | Mitsubishi | 150Mbps (optical) | Very High (0.444ms) |
| CC-Link IE | Mitsubishi | 1Gbps | High (~1ms) |
| PROFINET | IEC 61158 | 100Mbps~1Gbps | High (IRT <1ms) |
| SECS-II/HSMS | SEMI E5/E37 | Ethernet | N/A (Factory Automation) |

---

## 📝 Notes

- 실제 통신은 구현하지 않으며, 모든 데이터는 **시뮬레이션**입니다.
- Modbus 레지스터 Read/Write 버튼은 랜덤 값으로 갱신됩니다.
- Serial 탭의 Loopback은 보낸 데이터를 수신창에 그대로 표시합니다.
- PID 차트는 1차계 플랜트(`G(s)=1/(0.5s+1)`)에 대한 응답을 실시간 계산합니다.

---

## 👨‍💻 Author

**김성진 (Kim Seongjin)**  
- 전자공학과 졸업
- 경력: 레이저 장비 제어, PMAC, 갈바노 캘리브레이션
- GitHub: [@sjsr-0401](https://github.com/sjsr-0401)

---

## 📄 License

MIT License © 2026 Kim Seongjin

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
