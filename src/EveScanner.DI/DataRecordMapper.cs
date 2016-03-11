namespace EveScanner.IoC
{
    using System;
    using System.Data;

    public static class DataRecordMapper
    {
        public static TOutputType MapDataRecordToOutputType<TOutputType>(IDataRecord record) where TOutputType : class, new()
        {
            if (record == null)
            {
                return default(TOutputType);
            }

            TOutputType output = new TOutputType();

            for (int i = 0; i < record.FieldCount; i++)
            {
                string fieldName = record.GetName(i);
                object value = record[i];

                if (value == DBNull.Value)
                {
                    value = null;
                }

                PropertyMapper<TOutputType>.Instance.SetValue(output, fieldName, value);
            }

            return output;
        }
    }
}
