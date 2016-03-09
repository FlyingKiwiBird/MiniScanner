namespace EveScanner.SQLiteStorage
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;

    using EveScanner.IoC;

    public abstract class SQLiteQueryable
    {
        public abstract string ConnectionString { get; }

        internal void ExecuteNonQuery(string query, params object[] paramNameThenValue)
        {
            using (SQLiteConnection cn = new SQLiteConnection(this.ConnectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, cn))
            {
                cn.Open();
                if (paramNameThenValue != null && paramNameThenValue.Length >= 2)
                {
                    for (int i = 0; i < paramNameThenValue.Length; i = i + 2)
                    {
                        cmd.Parameters.AddWithValue(paramNameThenValue[i].ToString(), paramNameThenValue[i + 1]);
                    }
                }

                cmd.ExecuteNonQuery();
            }
        }

        internal TOutputType ExecuteScalar<TOutputType>(string query, params object[] paramNameThenValue)
        {
            using (SQLiteConnection cn = new SQLiteConnection(this.ConnectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, cn))
            {
                cn.Open();
                if (paramNameThenValue != null && paramNameThenValue.Length >= 2)
                {
                    for (int i = 0; i < paramNameThenValue.Length; i = i + 2)
                    {
                        cmd.Parameters.AddWithValue(paramNameThenValue[i].ToString(), paramNameThenValue[i + 1]);
                    }
                }

                return (TOutputType)cmd.ExecuteScalar();
            }
        }

        internal TOutputType RunSingleRecordQuery<TOutputType>(string query, params object[] paramNameThenValue) where TOutputType : class, new()
        {
            return this.RunMultipleRecordQuery<TOutputType>(query, paramNameThenValue).FirstOrDefault();
        }

        internal IEnumerable<TOutputType> RunMultipleRecordQuery<TOutputType>(string query, params object[] paramNameThenValue) where TOutputType : class, new()
        {
            using (SQLiteConnection cn = new SQLiteConnection(this.ConnectionString))
            using (SQLiteCommand cmd = new SQLiteCommand(query, cn))
            {
                cn.Open();
                if (paramNameThenValue != null && paramNameThenValue.Length >= 2)
                {
                    for (int i = 0; i < paramNameThenValue.Length; i = i + 2)
                    {
                        cmd.Parameters.AddWithValue(paramNameThenValue[i].ToString(), paramNameThenValue[i + 1]);
                    }
                }
                
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return DataRecordMapper.MapDataRecordToOutputType<TOutputType>(reader);
                    }
                }
            }
        }
    }
}
