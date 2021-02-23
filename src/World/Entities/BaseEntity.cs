using System;
using System.Collections;
using Classic.Common;

namespace Classic.World.Entities
{
    public abstract class BaseEntity
    {
        public static int Level = 1;
        public int Model = 0;

        // Base Stats
        public float Size = 1.0f;

        protected BaseEntity(int build)
        {
            var dataLength = this.GetDatalength(build);
            this.MaskSize = (dataLength + 32) / 32;
            this.Mask = new BitArray(dataLength, false);
            this.UpdateData = new Hashtable();
        }

        protected abstract int GetDatalength(int build);

        public int MaskSize { get; private set; }
        public BitArray Mask { get; private set; }
        public Hashtable UpdateData { get; private set; }
        public int UpdateCount { get; private set; }

        public virtual string Name { get; set; }

        public void SetUpdateField<T>(int index, T value)
        {
            UpdateCount++;
            Mask.Set(index, true);
            switch (value)
            {
                case byte _:
                case ushort _:
                    var _update = (uint)Convert.ChangeType(value, typeof(uint));

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (uint)UpdateData[index] | _update;
                    else
                        UpdateData[index] = _update;
                    break;
                case long l:
                    Mask.Set(index + 1, true);
                   
                    UpdateData[index] = (uint) (l & int.MaxValue);
                    UpdateData[index + 1] = (uint) ((l >> 32) & int.MaxValue);
                    break;
                case ulong u:
                    Mask.Set(index + 1, true);

                    UpdateData[index] = (uint) (u & uint.MaxValue);
                    UpdateData[index + 1] = (uint) ((u >> 32) & uint.MaxValue);
                    break;
                default:
                    UpdateData[index] = value;
                    break;
            }
        }

        public void WriteUpdateFields(PacketWriter writer)
        {
            writer.WriteUInt8((byte) MaskSize);
            
            var bufferarray = new byte[Convert.ToByte((Mask.Length + 8) / 8) + 1];
            Mask.CopyTo(bufferarray, 0);
            writer.WriteByteArrayWithLength(bufferarray, MaskSize * 4);

            for (var i = 0; i < Mask.Count; i++)
            {
                if (!Mask.Get(i)) continue;

                switch (UpdateData[i])
                {
                    case uint u:
                        writer.WriteUInt32(u);
                        break;
                    case float f:
                        writer.WriteFloat(f);
                        break;
                    default:
                        writer.WriteInt32((int) UpdateData[i]);
                        break;
                }
            }

            Mask.SetAll(false);
            UpdateCount = 0;
        }
    }
}