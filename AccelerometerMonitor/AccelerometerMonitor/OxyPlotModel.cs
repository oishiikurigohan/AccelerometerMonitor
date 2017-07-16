using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.ComponentModel;
using Xamarin.Forms;

namespace AccelerometerMonitor
{
    public class OxyPlotModel
    {
        public PlotModel Model { get; private set; }
        private LineSeries X = new LineSeries();
        private LineSeries Y = new LineSeries();
        private LineSeries Z = new LineSeries();

        public OxyPlotModel ()
        {
            this.Model = new PlotModel { Title = "CocosSharp Accelerometor Monitor" };
            this.Model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -2.0, Maximum = 2.0 });

            X.Title = "X";
            X.RenderInLegend = true;
            X.Color = OxyColors.Red;
            Model.Series.Add(X);

            Y.Title = "Y";
            Y.RenderInLegend = true;
            Y.Color = OxyColors.Blue;
            Model.Series.Add(Y);

            Z.Title = "Z";
            Z.RenderInLegend = true;
            Z.Color = OxyColors.Green;
            Model.Series.Add(Z);
        }

        public void Update(object sender, PropertyChangedEventArgs e)
        {
            var accelModel = sender as CocosSharpAccelerometorModel;
            if(X.Points.Count > 50)
            {
                X.Points.RemoveAt(0);
                Y.Points.RemoveAt(0);
                Z.Points.RemoveAt(0);
            }
            X.Points.Add(new DataPoint(accelModel.Count, accelModel.X));
            Y.Points.Add(new DataPoint(accelModel.Count, accelModel.Y));
            Z.Points.Add(new DataPoint(accelModel.Count, accelModel.Z));
            Device.BeginInvokeOnMainThread(() => { this.Model.InvalidatePlot(true); });
        }
    }
}
