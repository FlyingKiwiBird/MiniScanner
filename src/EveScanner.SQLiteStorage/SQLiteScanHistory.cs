namespace EveScanner.SQLiteStorage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Linq;

    using EveScanner.Core;
    using EveScanner.Interfaces;

    public class SQLiteScanHistory : IScanHistory
    {
        private static string ConnectionString = "Data Source=ScanHistory.db;Version=3;";

        public SQLiteScanHistory()
        {
            if (!File.Exists("ScanHistory.db"))
            {
                SQLiteScanHistory.CreateDatabase();
            }
        }

        public Guid AddScan(IScanResult result)
        {
            string checkForUUIDText = "SELECT COUNT(ID) FROM tblScanHistory WHERE Id = @Id";
            string insertCommandText = "INSERT INTO tblScanHistory (Id, ScanDate, RawScan, BuyValue, SellValue, Stacks, Volume, AppraisalURL, ShipType, Location, CharacterName, FitInfo, Notes) VALUES(@Id, @ScanDate, @RawScan, @BuyValue, @SellValue, @Stacks, @Volume, @AppraisalURL, @ShipType, @Location, @CharacterName, @FitInfo, @Notes);";

            int output = -1;

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();

                Guid uuid = result.Id;
                bool isNewId = true;

                do
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(checkForUUIDText, cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", uuid.ToString());
                        long rowCount = (long)cmd.ExecuteScalar();
                        if (rowCount > 0)
                        {
                            isNewId = false;
                            uuid = Guid.NewGuid();
                        }
                        else
                        {
                            isNewId = true;
                        }
                    }
                } while (isNewId == false);

                using (SQLiteCommand cmd = new SQLiteCommand(insertCommandText, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", uuid.ToString());
                    cmd.Parameters.AddWithValue("@ScanDate", result.ScanDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@RawScan", result.RawScan);
                    cmd.Parameters.AddWithValue("@BuyValue", result.BuyValue);
                    cmd.Parameters.AddWithValue("@SellValue", result.SellValue);
                    cmd.Parameters.AddWithValue("@Stacks", result.Stacks);
                    cmd.Parameters.AddWithValue("@Volume", result.Volume);
                    cmd.Parameters.AddWithValue("@AppraisalURL", result.AppraisalUrl);
                    cmd.Parameters.AddWithValue("@ShipType", result.ShipType);
                    cmd.Parameters.AddWithValue("@Location", result.Location);
                    cmd.Parameters.AddWithValue("@CharacterName", result.CharacterName);
                    cmd.Parameters.AddWithValue("@FitInfo", result.FitInfo);
                    cmd.Parameters.AddWithValue("@Notes", result.Notes);

                    cmd.ExecuteNonQuery();

                    output = (int)cn.LastInsertRowId;
                }

                return uuid;
            }
        }

        public IScanResult GetResultById(Guid id)
        {
            string selectCommandText = "SELECT * FROM tblScanHistory WHERE Id = @Id";
            IScanResult output = null;

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(selectCommandText, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // public ScanResult(Guid id, DateTime scanDate, string rawScan, decimal buyValue, decimal sellValue, int stacks, decimal volume, string appraisalUrl, IEnumerable<int> imageIndex)
                            output = this.ResolveReaderToScanResult(rdr);
                        }
                    }
                }
            }

            return output;
        }

        public IEnumerable<IScanResult> GetAllScans()
        {
            string selectCommandText = "SELECT * FROM tblScanHistory ORDER BY rowid DESC";

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(selectCommandText, cn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // public ScanResult(Guid id, DateTime scanDate, string rawScan, decimal buyValue, decimal sellValue, int stacks, decimal volume, string appraisalUrl, IEnumerable<int> imageIndex)
                            yield return this.ResolveReaderToScanResult(rdr);
                        }
                    }
                }
            }

            yield break;
        }

        public IEnumerable<IScanResult> GetScansByCharacterName(string characterName)
        {
            string selectCommandText = "SELECT * FROM tblScanHistory WHERE CharacterName = @CharacterName ORDER BY rowid DESC";

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(selectCommandText, cn))
                {
                    cmd.Parameters.AddWithValue("@CharacterName", characterName);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            // public ScanResult(Guid id, DateTime scanDate, string rawScan, decimal buyValue, decimal sellValue, int stacks, decimal volume, string appraisalUrl, IEnumerable<int> imageIndex)
                            yield return this.ResolveReaderToScanResult(rdr);
                        }
                    }
                }
            }

            yield break;
        }

        public void UpdateScan(IScanResult result)
        {
            string checkForUUIDText = "SELECT COUNT(ID) FROM tblScanHistory WHERE Id = @Id;";
            string updateCommandText = "UPDATE tblScanHistory SET ShipType = @ShipType, Location = @Location, CharacterName = @CharacterName, FitInfo = @FitInfo, Notes = @Notes WHERE ID = @Id;";
            bool isAdd = false;

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(checkForUUIDText, cn))
                {
                    cmd.Parameters.AddWithValue("@Id", result.Id.ToString());
                    long rowCount = (long)cmd.ExecuteScalar();
                    if (rowCount == 0)
                    {
                        isAdd = true;
                    }
                }

                if (!isAdd)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(updateCommandText, cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", result.Id.ToString());
                        cmd.Parameters.AddWithValue("@ShipType", result.ShipType);
                        cmd.Parameters.AddWithValue("@Location", result.Location);
                        cmd.Parameters.AddWithValue("@CharacterName", result.CharacterName);
                        cmd.Parameters.AddWithValue("@FitInfo", result.FitInfo);
                        cmd.Parameters.AddWithValue("@Notes", result.Notes);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (isAdd)
            {
                this.AddScan(result);
            }
        }

        private static void CreateDatabase()
        {
            #region Create Table SQL
            string createTable = @"
CREATE TABLE ""tblScanHistory"" (
	`ID`	TEXT NOT NULL UNIQUE,
	`ScanDate`	TEXT NOT NULL,
	`RawScan`	TEXT NOT NULL,
	`BuyValue`	NUMERIC NOT NULL,
	`SellValue`	NUMERIC NOT NULL,
	`Stacks`	INTEGER NOT NULL,
	`Volume`	NUMERIC NOT NULL,
	`AppraisalURL`	TEXT NOT NULL,
	`ShipType`	TEXT,
	`Location`	TEXT,
	`CharacterName`	TEXT,
	`FitInfo`	TEXT,
	`Notes`	TEXT,
	PRIMARY KEY(ID)
);";
            #endregion Create Table SQL

            using (SQLiteConnection cn = new SQLiteConnection(SQLiteScanHistory.ConnectionString))
            {
                cn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(createTable, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private IScanResult ResolveReaderToScanResult(IDataReader reader)
        {
            string rawScan = (string)this.GetResultData(reader, "RawScan");
            string[] items = rawScan.Split(new string[] { "\r", "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Substring(x.IndexOf(' ') + 1)).ToArray();

            return new ScanResult(
                Guid.Parse((string)this.GetResultData(reader, "Id")),
                DateTime.Parse((string)this.GetResultData(reader, "ScanDate")),
                rawScan,
                (decimal)this.GetResultData(reader, "BuyValue"),
                (decimal)this.GetResultData(reader, "SellValue"),
                (int)(long)this.GetResultData(reader, "Stacks"),
                (decimal)this.GetResultData(reader, "Volume"),
                (string)this.GetResultData(reader, "AppraisalUrl"),
                ConfigHelper.Instance.FindImagesToDisplay(items)
            )
            {
                ShipType = (string)this.GetResultData(reader, "ShipType"),
                Location = (string)this.GetResultData(reader, "Location"),
                CharacterName = (string)this.GetResultData(reader, "CharacterName"),
                FitInfo = (string)this.GetResultData(reader, "FitInfo"),
                Notes = (string)this.GetResultData(reader, "Notes")
            };
        }

        private object GetResultData(IDataReader reader, string columnName)
        {
            object obj = reader[columnName];

            if (obj == DBNull.Value)
            {
                return null;
            }

            return obj;
        }
    }
}
