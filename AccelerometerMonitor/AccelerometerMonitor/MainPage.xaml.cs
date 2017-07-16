using Xamarin.Forms;

namespace AccelerometerMonitor
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var accelerometerModel = new CocosSharpAccelerometorModel();
            var oxyPlotModel = new OxyPlotModel();

            OxyPlotView.Model = oxyPlotModel.Model;
            CocosView.ViewCreated = accelerometerModel.Create;
            StartButton.Clicked += accelerometerModel.Start;
            StopButton.Clicked += accelerometerModel.Stop;
            accelerometerModel.PropertyChanged += oxyPlotModel.Update;
        }
    }
}
