using System.Data;
using System.Data.SqlClient;

namespace InsuranceBroker
{
    public class InsuranceDataAccess
    {
        private readonly string _connectionString;
        public InsuranceDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }


        public void GetInsurancePolicies()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("GetInsurancePolicies", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"PolicyID: {reader["PolicyID"]}, Name: {reader["FirstName"]} {reader["LastName"]}, Vehicle: {reader["Make"]} {reader["Model"]}, Duration: {reader["DurationMonths"]} months, Max Value: {reader["MaxInsuredValue"]}");
                        }
                    }
                }
            }
        }

        public List<Person> GetPersons()
        {
            List<Person> persons = new List<Person>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string sqlQuery = "SELECT ID, FirstName, LastName FROM Persons";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persons.Add(new Person
                            {
                                ID = reader["ID"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString()

                            });
                        }
                    }
                }
            }
            return persons;
        }
        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string sqlQuery = "SELECT ID, Make, Model FROM Vehicles";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vehicles.Add(new Vehicle
                            {
                                ID = reader["ID"].ToString(),
                                Make = reader["Make"].ToString(),
                                Model = reader["Model"].ToString()
                            });
                        }
                    }
                }
            }
            return vehicles;
        }
        public void InsertPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string insertQuery = @"
INSERT INTO Persons (FirstName, LastName)
VALUES (@FirstName, @LastName);";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void InsertVehicle(Vehicle vehicle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string insertQuery = @"
INSERT INTO Vehicles (Make, Model)
VALUES (@Make, @Model);";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Make", vehicle.Make);
                    command.Parameters.AddWithValue("@Model", vehicle.Model);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void EditPerson(int personId, Person updatedPerson)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string updateQuery = @"
UPDATE Persons
SET FirstName = @FirstName, LastName = @LastName
WHERE ID = @ID;";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", updatedPerson.FirstName);
                    command.Parameters.AddWithValue("@LastName", updatedPerson.LastName);
                    command.Parameters.AddWithValue("@ID", personId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void EditVehicle(int vehicleId, Vehicle updatedVehicle)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string updateQuery = @"
UPDATE Vehicles
SET Make = @Make, Model = @Model
WHERE ID = @ID;";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Make", updatedVehicle.Make);
                    command.Parameters.AddWithValue("@Model", updatedVehicle.Model);
                    command.Parameters.AddWithValue("@ID", vehicleId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeletePerson(int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string deleteQuery = "DELETE FROM Persons WHERE ID = @ID;";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", personId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteVehicle(int vehicleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                const string deleteQuery = "DELETE FROM Vehicles WHERE ID = @ID;";
                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@ID", vehicleId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

