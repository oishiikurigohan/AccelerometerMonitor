using System;
using System.Threading;
using System.ComponentModel;
using CocosSharp;

namespace AccelerometerMonitor
{
    public class CocosSharpAccelerometorModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly PropertyChangedEventArgs SensorPropertyChangedEventArgs = new PropertyChangedEventArgs(nameof(Count));
        private double count = 0;
        private const double Interval = 20;
        private CCAccelerometer accelerometer;
        private Timer timer;

        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double Count
        {
            get { return this.count; }
            set {
                this.count = value;
                this.PropertyChanged?.Invoke(this, SensorPropertyChangedEventArgs);
            }
        }

        public void Create(object sender, EventArgs e)
        {
            var ccgameView = sender as CCGameView;
            var scene = new CCScene(ccgameView);

            accelerometer = new CCAccelerometer(ccgameView);
            accelerometer.Enabled = true;

            var listener = new CCEventListenerAccelerometer();
            listener.OnAccelerate = DidAccelerate;
            scene.AddEventListener(listener);

            var callBack = new TimerCallback((o) => {
                accelerometer.Update(); });
            timer = new Timer(callBack, null, Timeout.Infinite, Timeout.Infinite);

            ccgameView.RunWithScene(scene);
        }

        private void DidAccelerate(CCEventAccelerate AccelEvent)
        {
            X = AccelEvent.Acceleration.X;
            Y = AccelEvent.Acceleration.Y;
            Z = AccelEvent.Acceleration.Z;
            Count = Count + (Interval / 1000.0);
        }

        public void Start(object sender, EventArgs e) => timer.Change(0, (int)Interval);
        public void Stop(object sender, EventArgs e) => timer.Change(Timeout.Infinite, Timeout.Infinite);
    }
}
