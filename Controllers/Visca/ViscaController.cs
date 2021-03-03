using CommsLIB;
using CommsLIB.Base;
using CommsLIB.Communications;
using MiniCameraControl.Controllers.Visca;
using MiniCameraControl.FrameWrapper.Visca;
using MiniCameraControl.Model;
using MiniCameraControl.Model.Visca.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCameraControl.Controllers.Visca
{
    public abstract class ViscaController : ICameraController
    {
        protected Dictionary<string, double> _liveParameters { get; } = new Dictionary<string, double>();

        protected Dictionary<double, short> _zoomLevels { get; } = new Dictionary<double, short>();

        TCPNETCommunicator<BaseMessage> _communicator;
        ViscaFrameWrapper _viscaFrameWrapper;

        private int _cameraIndex = 0;

        #region ICameraController

        public string ID { get; set; }

        public event CameraParameterChangedDelegate CameraParameterChangedEvent;
        public event CameraConnectedDelegate CameraConnectedEvent;

        public void Connect(ConnUri cameraUri)
        {
            if (_communicator != null)
            {
                _viscaFrameWrapper.FrameAvailableEvent -= OnMessage;
                _viscaFrameWrapper.Dispose();
                _viscaFrameWrapper = null;

                _communicator.ConnectionStateEvent -= OnConnection;
                _communicator.Dispose();
                _communicator = null;
            }

            _viscaFrameWrapper = new ViscaFrameWrapper();
            _viscaFrameWrapper.FrameAvailableEvent += OnMessage;
            _communicator = new TCPNETCommunicator<BaseMessage>(_viscaFrameWrapper);
            _communicator.ConnectionStateEvent += OnConnection;
            _communicator.Init(cameraUri, true, ID, 0);

            _communicator.Start();
        }

        private void OnConnection(string ID, ConnUri uri, bool connected)
        {
            // Send addres msg
            if (connected)
            {
                var cmd = AddressCMD.RentFromPool();
                SendMessage(cmd);
            }
            else
                CameraConnectedEvent?.Invoke(ID, false);
        }

        private void OnMessage(string ID, BaseMessage payload)
        {
            switch (payload)
            {
                case AbsoluteZoomRES zM:
                    System.Diagnostics.Debug.WriteLine($"Encoder: {zM.EncoderValue}");
                    var zoomValue = GetZoomFromEncoder(zM.EncoderValue);
                    if (zoomValue >= 0)
                        CameraParameterChangedEvent?.Invoke(ID, ViscaParameters.AbsoluteZoom, zoomValue);
                    break;
                case AddressRES zAdr:
                    System.Diagnostics.Debug.WriteLine($"Address: {zAdr.Address}");
                    CameraParameterChangedEvent?.Invoke(ID, ViscaParameters.Address, (double)zAdr.Address);
                    CameraConnectedEvent?.Invoke(ID, true);
                    break;
                case CompletedRES completedRES:
                    // Send zoom inquiry
                    System.Diagnostics.Debug.WriteLine("Completed zoom. Sending inquiry");
                    SendMessage(AbsoluteZoomInquiryCMD.RentFromPool());
                    break;
            }

            payload.Dispose();
        }

        private double GetZoomFromEncoder(short encoder)
        {
            foreach (KeyValuePair<double, short> kv in _zoomLevels)
            {
                if (kv.Value == encoder)
                    return kv.Key;
            }

            return -1;
        }

        public abstract ValueTask<bool> SetParameter(string parameter, double value);

        public async ValueTask<double> GetParameter(string parameter)
        {
            if (_liveParameters.TryGetValue(parameter, out double value))
                return value;
            else
                throw new ArgumentException("Parameter not supported");
        }

        public void RefreshParameter(string parameter)
        {
            switch (parameter)
            {
                case ViscaParameters.AbsoluteZoom:
                    SendMessage(AbsoluteZoomInquiryCMD.RentFromPool());
                    break;
            }
        }

        public abstract bool CheckParameterWithinRange(string parameters, double value);

        public void SendMessage(BaseMessage msg)
        {
            _communicator.SendSync(msg);
            msg.Dispose();
        }

        #endregion

        public ViscaController()
        {
            PopulateAvailableParameters();
        }

        private void PopulateAvailableParameters()
        {
            _liveParameters.Add(ViscaParameters.AbsoluteZoom, 0);
        }

        protected void AddZoom(double zoom, short encoder) => _zoomLevels.Add(zoom, encoder);
    }
}
