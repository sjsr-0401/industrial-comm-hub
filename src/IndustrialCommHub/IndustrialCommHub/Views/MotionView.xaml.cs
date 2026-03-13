using System.Windows;
using System.Windows.Controls;
using IndustrialCommHub.ViewModels;
using ScottPlot;

namespace IndustrialCommHub.Views;

public partial class MotionView : UserControl
{
    private MotionViewModel? _vm;

    public MotionView()
    {
        InitializeComponent();
        InitGlossary();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (_vm != null) _vm.PlotUpdateRequested -= UpdatePlot;
        _vm = DataContext as MotionViewModel;
        if (_vm != null)
        {
            _vm.PlotUpdateRequested += UpdatePlot;
            UpdatePlot();
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        _vm = DataContext as MotionViewModel;
        if (_vm != null)
        {
            _vm.PlotUpdateRequested += UpdatePlot;
            UpdatePlot();
        }
    }

    private void UpdatePlot()
    {
        if (_vm == null) return;
        var (xs, ys, sp) = _vm.ComputeStepResponse();

        PidPlot.Plot.Clear();
        PidPlot.Plot.Add.SignalXY(xs, sp);
        var spLine = PidPlot.Plot.Add.SignalXY(xs, sp);
        spLine.Color = new ScottPlot.Color(255, 193, 7, 180);
        spLine.LineWidth = 1.5f;
        spLine.LegendText = "Setpoint";

        var respLine = PidPlot.Plot.Add.SignalXY(xs, ys);
        respLine.Color = new ScottPlot.Color(0, 120, 215);
        respLine.LineWidth = 2;
        respLine.LegendText = "Response";

        PidPlot.Plot.Axes.AutoScale();
        PidPlot.Plot.XLabel("Time (s)");
        PidPlot.Plot.YLabel("Output");
        PidPlot.Plot.Title("PID Step Response");

        // Dark theme
        PidPlot.Plot.FigureBackground.Color = new ScottPlot.Color(30, 30, 30);
        PidPlot.Plot.DataBackground.Color = new ScottPlot.Color(45, 45, 45);
        PidPlot.Plot.Axes.Color(new ScottPlot.Color(224, 224, 224));
        PidPlot.Plot.Legend.BackgroundColor = new ScottPlot.Color(45, 45, 45);
        PidPlot.Plot.Legend.FontColor = new ScottPlot.Color(224, 224, 224);
        PidPlot.Plot.Legend.IsVisible = true;

        PidPlot.Refresh();
    }

    private void InitGlossary()
    {
        var terms = new[]
        {
            new { Term = "Servo Motor", Description = "엔코더 피드백으로 위치/속도/토크를 정밀 제어하는 모터" },
            new { Term = "Encoder", Description = "모터 축의 회전량(위치/속도)을 전기 신호로 변환하는 센서" },
            new { Term = "Homing", Description = "기계 원점(Home) 복귀 동작. 절대 위치 기준 설정" },
            new { Term = "Jog", Description = "수동 저속 이송. 안전 확인/조정용" },
            new { Term = "Torque", Description = "모터 회전력. 단위 N·m" },
            new { Term = "Inertia", Description = "관성 모멘트. 모터가 부하를 가속/감속시키는 데 필요한 힘" },
            new { Term = "Backlash", Description = "기어/스크류의 역방향 유격. 위치 정밀도에 영향" },
            new { Term = "Electronic Cam", Description = "마스터 축과 슬레이브 축 간의 전자적 캠 관계 정의" },
            new { Term = "Following Error", Description = "목표 위치와 실제 위치의 차이. 과부하/튜닝 불량 시 증가" },
            new { Term = "Feedforward", Description = "예측 기반 제어. PID 지연을 보상하여 응답성 향상" },
            new { Term = "Gain Scheduling", Description = "동작 조건에 따라 PID 게인을 동적으로 전환하는 기법" },
            new { Term = "CSP/CSV/CST", Description = "EtherCAT 동기화 모드: 위치/속도/토크 사이클릭 동기 제어" },
        };

        var dt = new System.Data.DataTable();
        dt.Columns.Add("Term");
        dt.Columns.Add("Description");
        foreach (var t in terms)
        {
            var row = dt.NewRow();
            row["Term"] = t.Term;
            row["Description"] = t.Description;
            dt.Rows.Add(row);
        }
        GlossaryGrid.ItemsSource = dt.DefaultView;
    }
}
