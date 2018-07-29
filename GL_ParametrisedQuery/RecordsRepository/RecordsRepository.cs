using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class RecordsRepository : IDisposable
{
    private SqlConnection _connection;

    public RecordsRepository()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json");
        IConfigurationRoot config = builder.Build();

        string conStr = config["connectionString"];

        _connection = new SqlConnection(conStr);
    }

    public IEnumerable<Record> GetAll()
    {
        string commandString = "SELECT * FROM [Records]";

        using (SqlCommand command = new SqlCommand(commandString, _connection))
        {
            _connection.Open();

            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (reader.Read())
                {
                    yield return new Record()
                    {
                        Id = (int)reader["Id"],
                        Text = (string)reader["Text"],
                        Author = (string)reader["Author"],
                        RecordDate = (DateTime)reader["RecordDate"],
                    };
                }
            }
        }
    }

    public void Add(Record record)
    {
        string commandString = @"INSERT INTO [Records] (Text, Author, RecordDate)
                        VALUES (@Text, @Author, @RecordDate)";

        using (SqlCommand command = new SqlCommand(commandString, _connection))
        {
            command.Parameters.AddWithValue("@Text", record.Text);
            command.Parameters.AddWithValue("@Author", record.Author);
            command.Parameters.AddWithValue("@RecordDate", record.RecordDate);

            _connection.Open();

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        string commandString = "DELETE [Records] WHERE Id = @Id";

        using (SqlCommand command = new SqlCommand(commandString, _connection))
        {
            command.Parameters.AddWithValue("@Id", id);

            _connection.Open();

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }

    public void Delete(Record record)
    {
        Delete(record.Id);
    }

    public void Update(Record record)
    {
        string commandString = "UPDATE [Records] SET Text = @Text, Author = @Author, RecordDate = @RecordDate WHERE Id = @Id";

        using (SqlCommand command = new SqlCommand(commandString, _connection))
        {
            command.Parameters.AddWithValue("@Text", record.Text);
            command.Parameters.AddWithValue("@Author", record.Author);
            command.Parameters.AddWithValue("@RecordDate", record.RecordDate);
            command.Parameters.AddWithValue("@Id", record.Id);

            _connection.Open();

            command.ExecuteNonQuery();

            _connection.Close();
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}