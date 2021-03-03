using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommsLIB.Communications.FrameWrappers;
using MiniCameraControl.Model.Visca.Messages;
using MiniViscaControl.Model.Visca;

namespace MiniCameraControl.FrameWrapper.Visca
{
    public class ViscaFrameWrapper : SyncFrameWrapper<BaseMessage>, IDisposable
    {
        byte[] _buffer = ArrayPool<byte>.Shared.Rent(1024);
        int _bufferIndex = 0;

        byte _terminator = 0xFF;

        public ViscaFrameWrapper(byte terminator = 0xFF) : base(false)
        {
            _terminator = terminator;
        }

        public override void AddBytes(byte[] bytes, int length)
        {
            for (int i = 0; i < length; i++)
            {
                _buffer[_bufferIndex++] = bytes[i];
                if (bytes[i] == _terminator)
                    ProcessPacket(_buffer, _bufferIndex);
            }
        }

        private void ProcessPacket(byte[] buffer, int length)
        {
            if (length >= 3)
            {
                // Got the correct amount of bytes from the camera
                int response = buffer[1] & 0xF0;
                BaseMessage msg = default;

                switch (response)
                {
                    case ViscaCodes.RESPONSE_ACK:
                        break;
                    case ViscaCodes.RESPONSE_ADDRESS:
                        var m = AddressRES.RentFromPool();
                        m.Deserialize(buffer, length);
                        msg = m;
                        break;
                    case ViscaCodes.RESPONSE_COMPLETED:
                        // Simple COMPLETED CMD
                        if (buffer[0] == 0x90 && length == 3)
                            msg = CompletedRES.RentFromPool();
                        // COMPLETED ZOOM
                        else if (buffer[0] == 0x90 && length == 7)
                        {
                            var rZoom = AbsoluteZoomRES.RentFromPool();
                            rZoom.Deserialize(buffer, length);
                            msg = rZoom;
                        }
                        break;
                }

                if (msg != null)
                    FireEvent(msg);
            }

            ResetWrapper();
        }

        private void ResetWrapper()
        {
            _bufferIndex = 0;
        }

        public override byte[] Data2BytesSync(BaseMessage data, out int count)
        {
            var buffer = data.Serialize();
            count = buffer.Length;

            return buffer;
        }

        public void Dispose()
        {
            if (_buffer != null)
            {
                ArrayPool<byte>.Shared.Return(_buffer);
                _buffer = null;
            }
        }
    }
}
