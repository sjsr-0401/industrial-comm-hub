using CommunityToolkit.Mvvm.ComponentModel;

namespace IndustrialCommHub.ViewModels;

public partial class SecsGemViewModel : ObservableObject
{
    public string EquipmentStateModel => @"
┌─────────────────────────────────────────────────────────────────────┐
│                    GEM Equipment State Model (E30)                  │
└─────────────────────────────────────────────────────────────────────┘

  ┌─────────┐   Power On    ┌──────────────────────────────────────┐
  │ POWER   │ ─────────────▶│           EQUIPMENT OFF-LINE         │
  │  OFF    │               │  ┌───────────────┐  ┌─────────────┐ │
  └─────────┘               │  │ ATTEMPT ON-   │  │ HOST OFF-   │ │
                            │  │ LINE (Timeout)│  │ LINE        │ │
                            │  └───────────────┘  └─────────────┘ │
                            └───────────────────────────────────────┘
                                         │ On-Line Request
                                         ▼
                            ┌──────────────────────────────────────┐
                            │           EQUIPMENT ON-LINE          │
                            │                                      │
                            │  ┌───────────────────────────────┐  │
                            │  │         ON-LINE LOCAL         │  │
                            │  │  Equipment controls itself    │  │
                            │  └───────────────────────────────┘  │
                            │              │  ▲                    │
                            │   Remote Cmd │  │ Local Request      │
                            │              ▼  │                    │
                            │  ┌───────────────────────────────┐  │
                            │  │        ON-LINE REMOTE         │  │
                            │  │  Host controls equipment      │  │
                            │  └───────────────────────────────┘  │
                            └──────────────────────────────────────┘";

    public string PmcCtcHostDiagram => @"
┌──────────────────────────────────────────────────────────────────┐
│                    반도체 장비 통신 구조                          │
└──────────────────────────────────────────────────────────────────┘

  ┌────────────┐    SECS-II/HSMS     ┌──────────────┐
  │            │ ◀──────────────────▶│              │
  │    HOST    │    GEM Protocol     │  CTC (Cell   │
  │  (MES/EAP) │                     │  Controller) │
  │            │                     │              │
  └────────────┘                     └──────┬───────┘
                                            │
                              Internal Bus  │  (RS-232/Ethernet)
                                            │
                     ┌──────────────────────┼──────────────────────┐
                     │                      │                      │
              ┌──────▼──────┐       ┌──────▼──────┐       ┌──────▼──────┐
              │    PMC #1   │       │    PMC #2   │       │    PMC #3   │
              │  (Process   │       │  (Process   │       │  (Process   │
              │  Module     │       │  Module     │       │  Module     │
              │  Controller)│       │  Controller)│       │  Controller)│
              └─────────────┘       └─────────────┘       └─────────────┘
                     │                      │                      │
              ┌──────▼──────┐       ┌──────▼──────┐       ┌──────▼──────┐
              │  Chamber 1  │       │  Chamber 2  │       │  Chamber 3  │
              │  (Process)  │       │  (Transfer) │       │  (Loadlock) │
              └─────────────┘       └─────────────┘       └─────────────┘

  Key Messages:
  S1F1  Are You There / S1F2 On Line Data
  S1F13 Establish Communications / S1F14 Establish Comm Ack
  S2F41 Host Command Send / S2F42 Host Command Ack
  S6F11 Event Report Send / S6F12 Event Report Ack
  S7F1  Process Program Load / S7F2 Process Program Load Ack";
}
