using System;
using System.Collections;
using Classic.Common;

namespace Classic.World.Entities
{
    public class BaseEntity
    {
        public static int Level = 1;
        public int Model = 0;

        // Base Stats
        public float Size = 1.0f;

        public BaseEntity()
        {
            MaskSize = (DataLength + 32) / 32;
            Mask = new BitArray(DataLength, false);
            UpdateData = new Hashtable();
        }

        public int MaskSize { get; }
        public BitArray Mask { get; }
        public Hashtable UpdateData { get; }
        public int UpdateCount { get; private set; }

        public virtual int DataLength { get; internal set; }
        public virtual string Name { get; set; }

        public void SetUpdateField<T>(int index, T value, byte offset = 0)
        {
            UpdateCount++;
            switch (value.GetType().Name)
            {
                case "SByte":
                case "Int16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (int) UpdateData[index] |
                                            ((int) Convert.ChangeType(value, typeof(int)) <<
                                             (offset * (value.GetType().Name == "Byte" ? 8 : 16)));
                    else
                        UpdateData[index] = (int) Convert.ChangeType(value, typeof(int)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Byte":
                case "UInt16":
                {
                    Mask.Set(index, true);

                    if (UpdateData.ContainsKey(index))
                        UpdateData[index] = (uint) UpdateData[index] |
                                            ((uint) Convert.ChangeType(value, typeof(uint)) <<
                                             (offset * (value.GetType().Name == "Byte" ? 8 : 16)));
                    else
                        UpdateData[index] = (uint) Convert.ChangeType(value, typeof(uint)) <<
                                            (offset * (value.GetType().Name == "Byte" ? 8 : 16));

                    break;
                }
                case "Int64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    var tmpValue = (long) Convert.ChangeType(value, typeof(long));

                    UpdateData[index] = (uint) (tmpValue & int.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & int.MaxValue);

                    break;
                }
                case "UInt64":
                {
                    Mask.Set(index, true);
                    Mask.Set(index + 1, true);

                    var tmpValue = (ulong) Convert.ChangeType(value, typeof(ulong));

                    UpdateData[index] = (uint) (tmpValue & uint.MaxValue);
                    UpdateData[index + 1] = (uint) ((tmpValue >> 32) & uint.MaxValue);

                    break;
                }
                default:
                {
                    Mask.Set(index, true);
                    UpdateData[index] = value;

                    break;
                }
            }
        }

        public void WriteUpdateFields(PacketWriter writer)
        {
            writer.WriteUInt8((byte) MaskSize);
            
            // writer.WriteBitArray(Mask, MaskSize * 4); // Int32 = 4 Bytes
            // Start WriteBitArray
            var bufferarray = new byte[Convert.ToByte((Mask.Length + 8) / 8) + 1];
            Mask.CopyTo(bufferarray, 0);
            writer.WriteByteArrayWithLength(bufferarray, MaskSize * 4);
            // End WriteBitArray

            for (var i = 0; i < Mask.Count; i++)
            {
                if (!Mask.Get(i)) continue;
                try
                {
                    switch (UpdateData[i].GetType().Name)
                    {
                        case "UInt32":
                            writer.WriteUInt32((uint) UpdateData[i]);
                            break;
                        case "Single":
                            writer.WriteFloat((float) UpdateData[i]);
                            break;
                        default:
                            writer.WriteInt32((int) UpdateData[i]);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Logger.Log($"Error in WriteUpdateFields, {e.Message}");
                }
            }

            Mask.SetAll(false);
            UpdateCount = 0;
        }
    }
}