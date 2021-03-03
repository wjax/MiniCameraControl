using MiniViscaControl.Model.Visca;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class AddressCMD : BaseMessage
    {
        #region pool
        private static Pool<AddressCMD> pool = new Pool<AddressCMD>(2,
                   () => new AddressCMD { } );

        public static AddressCMD RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new AddressCMD();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
        #endregion

        private byte[] _buffer = new byte[4];
        public int Address { get; set; }

        public override byte[] Serialize(int cameraIndex = 1)
        {
            _buffer[0] = ViscaCodes.HEADER;
            _buffer[0] |= (1 << 3);
            _buffer[0] &= 0xF8;
            _buffer[1] = 0x30;
            _buffer[2] = 0x01;
            _buffer[3] = 0xFF;

            return _buffer;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);

            ReturnToPool();
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }
    }
}
