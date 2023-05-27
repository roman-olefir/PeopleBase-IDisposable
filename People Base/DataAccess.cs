using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace People_Base
{
    public class DataAccess : IDisposable
    {
        private bool isDisposed = false;

        private SqlConnection connection;
        public DataAccess(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public List<String> GetAll()
        {
            string sqlExpression = "SELECT FullName FROM people";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<string> peopleNames = new List<string>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    peopleNames.Add(Convert.ToString(reader.GetValue(0)));
                }
                reader.Close();
            }
            return peopleNames;
        }

        public People Get(string name)
        {
            string sqlExpression = $"SELECT FullName, Gender, Birthday FROM People WHERE FullName = '{name}'";
            SqlCommand command = new SqlCommand(sqlExpression, connection);

            SqlDataReader reader = command.ExecuteReader();
            People PeopleInfo = new People();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PeopleInfo.FullName = (string)reader["FullName"];
                    PeopleInfo.Gender = (string)reader["Gender"];

                    string[] dataTime = reader["BirthDay"].ToString().Split('.');
                    PeopleInfo.BirthDay = new DateTime(Convert.ToInt32(dataTime[2]), Convert.ToInt32(dataTime[1]), Convert.ToInt32(dataTime[0]));
                }
            }
            return PeopleInfo;
        }

        public void Add(People people)
        {
            string sqlExpression = $"INSERT INTO People VALUES ('{people.FullName}', '{people.Gender}', '{people.BirthDay}')";
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            command.ExecuteNonQuery();
        }


        ~DataAccess()
        {
            Dispose(false);
        }

        // IDisposable Dispose method
        public void Dispose()
        {
            connection.Close();
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    connection.Close();
                }
                isDisposed = true;
            }
        }
    }
}