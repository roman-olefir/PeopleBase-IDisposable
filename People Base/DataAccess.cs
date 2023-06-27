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
            string sqlExpression = $"SELECT p.FullName, p.Gender, p.Birthday, c.Brand, c.Model, c.Color FROM People p JOIN Car c ON p.FullName = c.OwnerName WHERE p.FullName = '{name}'";
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

                    PeopleInfo.Car = new Car();
                    PeopleInfo.Car.Brand = (string)reader["Brand"];
                    PeopleInfo.Car.Model = (string)reader["Model"];
                    PeopleInfo.Car.Color = (string)reader["Color"];
                }
            }
            return PeopleInfo;
        }

        public void Add(People people)
        {
            string sqlExpressionPeople = $"INSERT INTO People VALUES ('{people.FullName}', '{people.Gender}', '{people.BirthDay}')";
            SqlCommand commandPeople = new SqlCommand(sqlExpressionPeople, connection);
            commandPeople.ExecuteNonQuery();

            string sqlExpressionCar = $"INSERT INTO Car VALUES ('{people.FullName}', '{people.Car.Brand}', '{people.Car.Model}', '{people.Car.Color}')";
            SqlCommand commandCar = new SqlCommand(sqlExpressionCar, connection);
            commandCar.ExecuteNonQuery();
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