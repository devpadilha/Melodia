using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine;
using System.Collections.Generic;

public class DataBase
{
    private string database = "melodia_database.db";
    private string connectionString; 

    public DataBase()
    {
        this.connectionString = "URI=file:" + Application.dataPath + "/" + database;
    }


    public Dictionary<int, List<string>> Select(string query, Dictionary<string,string> param)
    {
        Dictionary<int, List<string>> retorno = new Dictionary<int, List<string>>(); ;
        using (var connection = new SqliteConnection(this.connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                foreach(var p in param)
                {
                    command.Parameters.Add(new SqliteParameter(p.Key, p.Value));
                }
               
                using (var reader = command.ExecuteReader())
                {
                    int k = 0;
                    while (reader.Read())
                    {
                        List<string> result = new List<string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result.Add(reader.GetValue(i).ToString());
                        }
                        retorno.Add(k,result);
                        k++;
                    }
                }
            }
        }
        return retorno;
    }

    public int Insert(string query, Dictionary<string, string> param)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                using (var command = connection.CreateCommand())
                {
                  
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    foreach (var p in param)
                    {
                        command.Parameters.Add(new SqliteParameter(p.Key, p.Value));
                    }

                    command.Transaction = transaction;

                    var rows = command.ExecuteNonQuery();

                    command.CommandText = "select last_insert_rowid();";

                    Int64 fkId64 = (Int64)command.ExecuteScalar();
                    int fkId = (int)fkId64;
                    transaction.Commit();

                    return fkId;
                }
            }
        }
    }

    public string DateTimeSQLite(DateTime datetime)
    {
        string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
        return string.Format(dateTimeFormat, datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
    }

}
