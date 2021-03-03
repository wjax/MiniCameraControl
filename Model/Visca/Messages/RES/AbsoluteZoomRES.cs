using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class AbsoluteZoomRES : BaseMessage
    {
        #region pool
        private static Pool<AbsoluteZoomRES> pool = new Pool<AbsoluteZoomRES>(10,
                   () => new AbsoluteZoomRES { },
                   (m) => m.Reset());

        public static AbsoluteZoomRES RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new AbsoluteZoomRES();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
        #endregion

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
            throw new NotImplementedException();
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            EncoderValue = (short)((buffer[2] << 12) | (buffer[3] << 8) | (buffer[4] << 4) | (buffer[5]));
        }
    }
}
