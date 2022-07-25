using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MusicLibrary.Models
{
  public class Record
  {
    public string Title {get; set;}
    public int Id {get; set; }
    
  
    public Record(string title)
    {
      Title = title;
    }

    public Record(string title, int id)
    {
      Title = title;
      Id = id;
    }

    public override bool Equals(System.Object otherRecord)
    {
      if (!(otherRecord is Record))
      {
        return false;
      }
      else
      {
        Record newRecord = (Record) otherRecord;
        bool idEquality = (this.Id == newRecord.Id);
        bool titleEquality = (this.Title == newRecord.Title);
        return titleEquality;        
      }
    }

    public void Save()
    {
       MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;

        // Begin new code

        cmd.CommandText = "INSERT INTO records (title) VALUES (@RecordTitle);";
        MySqlParameter param = new MySqlParameter();
        param.ParameterName = "@RecordTitle";
        param.Value = this.Title;
        cmd.Parameters.Add(param);    
        cmd.ExecuteNonQuery();
        Id = (int) cmd.LastInsertedId;

        // End new code

        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
    }

    public static List<Record> GetAll()
    {
      List<Record> allRecords = new List<Record> { };
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = "SELECT * FROM records;";
        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
        while (rdr.Read())
        {
            int recordId = rdr.GetInt32(0);
            string recordTitle = rdr.GetString(1);
            Record newRecord = new Record(recordTitle, recordId);
            allRecords.Add(newRecord);
        }
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return allRecords;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "DELETE FROM records;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Record Find(int id)
    {
          // We open a connection.
      MySqlConnection conn = DB.Connection();
      conn.Open();

      // We create MySqlCommand object and add a query to its CommandText property. We always need to do this to make a SQL query.
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = "SELECT * FROM records WHERE id = @ThisId;";

      // We have to use parameter placeholders @ThisId and a `MySqlParameter` object to prevent SQL injection attacks. This is only necessary when we are passing parameters into a query. We also did this with our Save() method.
      MySqlParameter param = new MySqlParameter();
      param.ParameterName = "@ThisId";
      param.Value = id;
      cmd.Parameters.Add(param);

      // We use the ExecuteReader() method because our query will be returning results and we need this method to read these results. This is in contrast to the ExecuteNonQuery() method, which we use for SQL commands that don't return results like our Save() method.
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int recordId = 0;
      string recordTitle = "";
      while (rdr.Read())
      {
        recordId = rdr.GetInt32(0);
        recordTitle = rdr.GetString(1);
      }
      Record foundRecord = new Record(recordTitle, recordId);

      // We close the connection.
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundRecord;
        }
      }

}