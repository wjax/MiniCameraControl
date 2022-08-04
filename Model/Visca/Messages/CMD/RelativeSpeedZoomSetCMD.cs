using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class RelativeSpeedZoomSetCMD : BaseMessage
    {
        #region pool
        private static Pool<RelativeSpeedZoomSetCMD> pool = new Pool<RelativeSpeedZoomSetCMD>(2,
                   () => new RelativeSpeedZoomSetCMD { },
                   (m) => m.Reset());

        public static RelativeSpeedZoomSetCMD RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new RelativeSpeedZoomSetCMD();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
        #endregion

        private byte[] _buffer = new byte[6];
        public short DirectionSpeed { get; set; }

        public void Reset()
        {
            DirectionSpeed = 0;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);

            ReturnToPool();
        }

        public override byte[] Serialize(int cameraIndex = 1)
        {
            _buffer[0] = ViscaCodes.HEADER;
            _buffer[0] |= (byte)cameraIndex;
            _buffer[1] = ViscaCodes.COMMAND;
            _buffer[2] = ViscaCodes.CATEGORY_CAMERA1;
            _buffer[3] = ViscaCodes.ZOOM;
            if (DirectionSpeed == 0)
                _buffer[4] = 0;
            else
                _buffer[4] = DirectionSpeed > 0 ? (byte)0x30 : (byte)0x20;

            _buffer[4] |= (byte)(Math.Abs(DirectionSpeed) & 0x0F);
            _buffer[5] = ViscaCodes.TERMINATOR;

            return _buffer;
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }
    }
}
