//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="DataRecordMapper.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.IoC
{
    using System;
    using System.Data;

    /// <summary>
    /// Helper class to map a data record onto a new object.
    /// </summary>
    public static class DataRecordMapper
    {
        /// <summary>
        /// Maps the fields from a data record onto a new object, returning the object.
        /// </summary>
        /// <typeparam name="TOutputType">Type of object to output</typeparam>
        /// <param name="record">DataRecord to extract data from</param>
        /// <returns>Newly mapped object</returns>
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

                MemberMapper<TOutputType>.Instance.SetValue(output, fieldName, value);
            }

            return output;
        }
    }
}
