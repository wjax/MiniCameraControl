using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class AddressRES : BaseMessage
    {
        #region pool
        private static Pool<AddressRES> pool = new Pool<AddressRES>(2,
                   () => new AddressRES { } );

        public static AddressRES RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new AddressRES();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }

        public override byte[] Serialize(int cameraIndex = 1)
        {
            throw new NotImplementedException();
        }

        
        #endregion

        public int Address { get; set; }
        
        public override void Dispose()
        {
            GC.SuppressFinalize(this);

            ReturnToPool();
        }

        public override void Deserialize(byte[] buffer, int count)
        {
            Address = buffer[2] - 1;
        }
    }
}
