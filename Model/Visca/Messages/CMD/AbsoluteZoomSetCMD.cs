using MiniViscaControl.Model.Visca;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class AbsoluteZoomSetCMD : BaseMessage
    {
        #region pool
        private static Pool<AbsoluteZoomSetCMD> pool = new Pool<AbsoluteZoomSetCMD>(10,
                   () => new AbsoluteZoomSetCMD { },
                   (m) => m.Reset());

        public static AbsoluteZoomSetCMD RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new AbsoluteZoomSetCMD();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
        #endregion

        private byte[] _buffer = new byte[9];
        public short EncoderValue { get; set; }

        public void Reset()
        {
            EncoderValue = 0;
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
            _buffer[3] = ViscaCodes.ZOOM_VALUE;
            _buffer[4] = (byte)((EncoderValue & 0xF000) >> 12);
            _buffer[5] = (byte)((EncoderValue & 0x0F00) >> 8);
            _buffer[6] = (byte)((EncoderValue & 0x00F0) >> 4);
            _buffer[7] = (byte)(EncoderValue & 0x000F);
            _buffer[8] = ViscaCodes.TERMINATOR;

            return _buffer;
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }
    }
}
