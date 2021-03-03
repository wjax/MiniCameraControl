using CommsLIB.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniCameraControl.Model
{
    public delegate void CameraParameterChangedDelegate(string cameraID, string parameter, double value);
    public delegate void CameraConnectedDelegate(string cameraID, bool connected);

    public interface ICameraController
    {
        public event CameraParameterChangedDelegate CameraParameterChangedEvent;
        public event CameraConnectedDelegate CameraConnectedEvent;

        public string ID { get; set; }
        public void Connect(ConnUri cameraUri);
        public ValueTask<bool> SetParameter(string parameter, double value);
        public ValueTask<double> GetParameter(string parameter);
        public void RefreshParameter(string parameter);

        bool CheckParameterWithinRange(string parameters, double value);
    }
}
