using MiniViscaControl.Model.Visca;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class AbsoluteZoomInquiryCMD : BaseMessage
    {
        #region pool
        private static Pool<AbsoluteZoomInquiryCMD> pool = new Pool<AbsoluteZoomInquiryCMD>(10,
                   () => new AbsoluteZoomInquiryCMD { },
                   (m) => m.Reset());

        public static AbsoluteZoomInquiryCMD RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new AbsoluteZoomInquiryCMD();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
        #endregion

        private byte[] _buffer = new byte[5];

        public void Reset()
        {
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
            _buffer[1] = ViscaCodes.INQUIRY;
            _buffer[2] = ViscaCodes.CATEGORY_CAMERA1;
            _buffer[3] = ViscaCodes.ZOOM_VALUE;
            _buffer[4] = ViscaCodes.TERMINATOR;

            return _buffer;
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }
    }
}
