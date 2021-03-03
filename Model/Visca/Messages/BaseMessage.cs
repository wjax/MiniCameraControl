using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace MiniCameraControl.Model.Visca.Messages
{
    public abstract class BaseMessage : IDisposable
    {
        public abstract void Dispose();

        public abstract byte[] Serialize(int cameraIndex = 1);

        public abstract void Deserialize(byte[] buffer, int count);
    }
}
