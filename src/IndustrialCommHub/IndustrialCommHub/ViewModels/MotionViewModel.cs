using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScottPlot;

namespace IndustrialCommHub.ViewModels;

public partial class MotionViewModel : ObservableObject
{
    [ObservableProperty] private double _kp = 1.0;
    [ObservableProperty] private double _ki = 0.1;
    [ObservableProperty] private double _kd = 0.05;
    [ObservableProperty] private string _pidInfo = string.Empty;

    public event Action? PlotUpdateRequested;

    public (double[] xs, double[] ys, double[] setpoint) ComputeStepResponse()
    {
        int n = 300;
        double dt = 0.02;
        double[] xs = new double[n];
        double[] ys = new double[n];
        double[] sp = new double[n];

        double setpointVal = 1.0;
        double output = 0;
        double integral = 0;
        double prevError = 0;

        for (int i = 0; i < n; i++)
        {
            xs[i] = i * dt;
            sp[i] = setpointVal;

            double error = setpointVal - output;
            integral += error * dt;
            double derivative = (error - prevError) / dt;
            double control = Kp * error + Ki * integral + Kd * derivative;

            // Simple first-order plant: G(s) = 1/(Ts+1), T=0.5
            double tau = 0.5;
            output += dt * (control - output) / tau;
            output = Math.Clamp(output, -2, 3);
            ys[i] = output;
            prevError = error;
        }

        // Calculate overshoot and settling time
        double max = ys.Max();
        double overshoot = (max - setpointVal) / setpointVal * 100;
        double settlingTime = -1;
        for (int i = n - 1; i >= 0; i--)
        {
            if (Math.Abs(ys[i] - setpointVal) > 0.02 * setpointVal)
            {
                settlingTime = xs[Math.Min(i + 1, n - 1)];
                break;
            }
        }
        PidInfo = $"Overshoot: {overshoot:F1}%   Settling Time: {settlingTime:F2}s   Kp={Kp:F2} Ki={Ki:F2} Kd={Kd:F2}";
        return (xs, ys, sp);
    }

    partial void OnKpChanged(double value) => PlotUpdateRequested?.Invoke();
    partial void OnKiChanged(double value) => PlotUpdateRequested?.Invoke();
    partial void OnKdChanged(double value) => PlotUpdateRequested?.Invoke();
}
