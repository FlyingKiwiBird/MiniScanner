using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using EveScanner.IoC.Attributes;
using EveScanner.IoC.Extensions;

namespace EveScanner.IoC
{
    public class DataRecordMapper
    {
        public static TOutputType MapDataRecordToOutputType<TOutputType>(IDataRecord record) where TOutputType : class, new()
        {
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
