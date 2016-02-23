using System;
using System.Data;

namespace EveScanner.IoC
{
    public class FakeDataRecord<T> : IDataRecord where T : class, new()
    {
        private T record;

        public FakeDataRecord(T record)
        {
            this.record = record;
        }

        public object this[string name]
        {
            get
            {
                return PropertyMapper<T>.Instance.GetValue(record, name);
            }
        }

        public object this[int i]
        {
            get
            {
                return this.GetValue(i);
            }
        }

        public int FieldCount
        {
            get
            {
                return PropertyMapper<T>.Instance.Properties.Count;
            }
        }

        public bool GetBoolean(int i)
        {
            return (bool)this.GetValue(i);
        }

        public byte GetByte(int i)
        {
            return (byte)this.GetValue(i);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public char GetChar(int i)
        {
            return (char)this.GetValue(i);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException();
        }

        public string GetDataTypeName(int i)
        {
            return this.GetValue(i).GetType().ToString();
        }

        public DateTime GetDateTime(int i)
        {
            return (DateTime)this.GetValue(i);
        }

        public decimal GetDecimal(int i)
        {
            return (decimal)this.GetValue(i);
        }

        public double GetDouble(int i)
        {
            return (double)this.GetValue(i);
        }

        public Type GetFieldType(int i)
        {
            return this.GetValue(i).GetType();
        }

        public float GetFloat(int i)
        {
            return (float)this.GetValue(i);
        }

        public Guid GetGuid(int i)
        {
            return (Guid)this.GetValue(i);
        }

        public short GetInt16(int i)
        {
            return (short)this.GetValue(i);
        }

        public int GetInt32(int i)
        {
            return (int)this.GetValue(i);
        }

        public long GetInt64(int i)
        {
            return (long)this.GetValue(i);
        }

        public string GetName(int i)
        {
            return PropertyMapper<T>.Instance.Properties[i];
        }

        public int GetOrdinal(string name)
        {
            return PropertyMapper<T>.Instance.Properties.IndexOf(name);
        }

        public string GetString(int i)
        {
            return (string)this.GetValue(i);
        }

        public object GetValue(int i)
        {
            return this[this.GetName(i)];
        }

        public int GetValues(object[] values)
        {
            throw new NotSupportedException();
        }

        public bool IsDBNull(int i)
        {
            return this.GetValue(i) == null;
        }
    }
}
