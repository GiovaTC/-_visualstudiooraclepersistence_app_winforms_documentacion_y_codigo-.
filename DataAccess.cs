using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace OracleWinFormsApp
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DataAccess : IDisposable
    {
        private OracleConnection _connection;

        public DataAccess(string connectionString)
        {
            _connection = new OracleConnection(connectionString);
            _connection.Open();
        }

        public void Dispose()
        {
            try { _connection?.Close(); }
            catch { }
            _connection = null;
        }

        public void InsertPerson(string name, string email)
        {
            using (var cmd = _connection.CreateCommand())
            {
                // INSERT ajustado EXACTO a tu tabla
                cmd.CommandText = @"
                    INSERT INTO PERSONS_F (NAME, EMAIL, CREATED_AT)
                    VALUES (:name, :email, SYSTIMESTAMP)";

                cmd.Parameters.Add(new OracleParameter("name", OracleDbType.Varchar2, name, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("email", OracleDbType.Varchar2, email, ParameterDirection.Input));

                cmd.ExecuteNonQuery();
            }
        }

        public List<Person> GetPersons()
        {
            var list = new List<Person>();

            using (var cmd = _connection.CreateCommand())
            {
                // SELECT idéntico al orden real de tu tabla
                cmd.CommandText = @"
                    SELECT ID, NAME, EMAIL, CREATED_AT 
                    FROM PERSONS_F 
                    ORDER BY ID";

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Person
                        {
                            Id = reader.GetInt64(0),
                            Name = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Email = reader.IsDBNull(2) ? null : reader.GetString(2),
                            CreatedAt = reader.IsDBNull(3) ? DateTime.MinValue : reader.GetDateTime(3)
                        };

                        list.Add(p);
                    }
                }
            }

            return list;
        }
    }
}
