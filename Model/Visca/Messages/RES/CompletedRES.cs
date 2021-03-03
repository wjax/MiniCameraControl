using System;
using System.Collections.Generic;
using System.Text;
using Tools.Base;

namespace MiniCameraControl.Model.Visca.Messages
{
    public class CompletedRES : BaseMessage
    {
        #region pool
        private static Pool<CompletedRES> pool = new Pool<CompletedRES>(5,
                   () => new CompletedRES { } );

        public static CompletedRES RentFromPool()
        {
            // try to get from the pool; only allocate new obj if necessary
            return pool.Pop() ?? new CompletedRES();
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
