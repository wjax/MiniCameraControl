using MiniCameraControl.Model.Visca.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniCameraControl.Controllers.Visca
{
    public class ViscaFCB7250AController : ViscaController
    {
        private const int MAX_RELATIVE_ZOOM_SPEED = 7;
        private const int MIN_RELATIVE_ZOOM_SPEED = 2;
        private const int ZERO_RELATIVE_ZOOM_SPEED = 0;

        public ViscaFCB7250AController() : base()
        {
            AddZoom(1.0, (short)0x0000);
            AddZoom(2.0, (short)0x16A1);
            AddZoom(3.0, (short)0x2063);
            AddZoom(4.0, (short)0x2628);
            AddZoom(5.0, (short)0x2A1D);
            AddZoom(6.0, (short)0x2D13);
            AddZoom(7.0, (short)0x2F6D);
            AddZoom(8.0, (short)0x3161);
            AddZoom(9.0, (short)0x330D);
            AddZoom(10.0, (short)0x3486);
            AddZoom(11.0, (short)0x35D7);
            AddZoom(12.0, (short)0x3709);
            AddZoom(13.0, (short)0x3820);
            AddZoom(14.0, (short)0x3920);
            AddZoom(15.0, (short)0x3A0A);
            AddZoom(16.0, (short)0x3ADD);
            AddZoom(17.0, (short)0x3B9C);
            AddZoom(18.0, (short)0x3C46);
            AddZoom(19.0, (short)0x3CDC);
            AddZoom(20.0, (short)0x3D60);
            AddZoom(21.0, (short)0x3DD4);
            AddZoom(22.0, (short)0x3E39);
            AddZoom(23.0, (short)0x3E90);
            AddZoom(24.0, (short)0x3EDC);
            AddZoom(25.0, (short)0x3F1E);
            AddZoom(26.0, (short)0x3F57);
            AddZoom(27.0, (short)0x3F8A);
            AddZoom(28.0, (short)0x3FB6);
            AddZoom(29.0, (short)0x3FDC);
            AddZoom(30.0, (short)0x4000);
        }

        public override bool CheckParameterWithinRange(string parameter, double value)
        {
            if (!_liveParameters.ContainsKey(parameter))
                throw new ArgumentException("Parameter not supported");

            switch (parameter)
            {
                case ViscaParameters.AbsoluteZoom:
                    // Ugly but need to do it fast
                    if (_zoomLevels.ContainsKey(value))
                        return true;
                    break;
                case ViscaParameters.RelativeSpeedZoom:
                    if (value == 0 || (Math.Abs(value) >= MIN_RELATIVE_ZOOM_SPEED && Math.Abs(value) <= MAX_RELATIVE_ZOOM_SPEED))
                        return true;
                    break;
            }

            return false;
        }

        public override async ValueTask<bool> SetParameter(string parameter, double value)
        {
            if (CheckParameterWithinRange(parameter, value))
            {
                switch (parameter)
                {
                    case ViscaParameters.AbsoluteZoom:
                        SetAbsoluteZoom(value);
                        return true;
                    case ViscaParameters.RelativeSpeedZoom:
                        SetRelativeSpeedZoom(value);
                        return true;
                }
            }

            return false;
        }

        private void SetRelativeSpeedZoom(double value)
        {
            var cmd = RelativeSpeedZoomSetCMD.RentFromPool();
            cmd.DirectionSpeed = (short)value;

            SendMessage(cmd);
        }

        private void SetAbsoluteZoom(double value)
        {
            var cmd = AbsoluteZoomSetCMD.RentFromPool();
            cmd.EncoderValue = _zoomLevels[value];

            SendMessage(cmd);
        }
    }
}
