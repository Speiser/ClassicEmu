using System;
using System.Collections;
using Classic.Shared;

namespace Classic.World.Entities;

public abstract class BaseEntity
{
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

    protected void SetUpdateField<T>(int index, T value)
    {
        this.UpdateCount++;
        this.Mask.Set(index, true);
        switch (value)
        {
            case byte _:
            case ushort _:
                var _update = (uint)Convert.ChangeType(value, typeof(uint));

                this.UpdateData[index] = this.UpdateData.ContainsKey(index) ? (uint)this.UpdateData[index] | _update : _update;
                break;

            case long l:
                this.Mask.Set(index + 1, true);

                this.UpdateData[index] = (uint)(l & int.MaxValue);
                this.UpdateData[index + 1] = (uint)((l >> 32) & int.MaxValue);
                break;

            case ulong u:
                this.Mask.Set(index + 1, true);

                this.UpdateData[index] = (uint)(u & uint.MaxValue);
                this.UpdateData[index + 1] = (uint)((u >> 32) & uint.MaxValue);
                break;

            default:
                this.UpdateData[index] = value;
                break;
        }
    }

    public void WriteUpdateFields(PacketWriter writer)
    {
        writer.WriteUInt8((byte)this.MaskSize);

        var bufferarray = new byte[Convert.ToByte((this.Mask.Length + 8) / 8) + 1];
        this.Mask.CopyTo(bufferarray, 0);
        writer.WriteByteArrayWithLength(bufferarray, this.MaskSize * 4);

        for (var i = 0; i < this.Mask.Count; i++)
        {
            if (!this.Mask.Get(i)) continue;

            switch (this.UpdateData[i])
            {
                case uint u:
                    writer.WriteUInt32(u);
                    break;
                case float f:
                    writer.WriteFloat(f);
                    break;
                default:
                    try
                    {
                        writer.WriteInt32((int)this.UpdateData[i]);
                    }
                    catch
                    {
                        var x = this.UpdateData[i];
                    }
                    break;
            }
        }

        this.Mask.SetAll(false);
        this.UpdateCount = 0;
    }
}
