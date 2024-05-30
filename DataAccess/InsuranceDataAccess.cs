using Dapper.Contrib.Extensions;
using System.Data.SqlClient;

namespace DataAccess
{
    public class InsuranceDataAccess
    {
        private readonly string _connectionString;
        public InsuranceDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        private SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
        // Insert a Person
        public async Task<long> InsertPersonAsync(Person person)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.InsertAsync(person);
            }
        }
        // Retrieve a Person by ID
        public async Task<Person> GetPersonByIdAsync(int id)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAsync<Person>(id);
            }
        }
        // Retrieve all Persons
        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAllAsync<Person>();
            }
        }
        // Update a Person
        public async Task<bool> UpdatePersonAsync(Person person)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.UpdateAsync(person);
            }
        }
        // Delete a Person
        public async Task<bool> DeletePersonAsync(Person person)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.DeleteAsync(person);
            }
        }

        //VEHICLE operations 
        public async Task<long> InsertVehicleAsync(Vehicle vehicle)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.InsertAsync(vehicle);
            }
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAsync<Vehicle>(id);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicleAsync()
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAllAsync<Vehicle>();
            }
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.UpdateAsync(vehicle);
            }
        }

        public async Task<bool> DeleteVehicleAsync(Vehicle vehicle)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.DeleteAsync(vehicle);
            }
        }


        // InsurancePolicy operations
        public async Task<long> InsertInsurancePolicyAsync(InsurancePolicy policy)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.InsertAsync(policy);
            }
        }
        public async Task<InsurancePolicy> GetInsurancePolicyByIdAsync(int id)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAsync<InsurancePolicy>(id);
            }
        }
        public async Task<IEnumerable<InsurancePolicy>> GetAllInsurancePoliciesAsync()
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.GetAllAsync<InsurancePolicy>();
            }
        }

        public async Task<bool> UpdateInsurancePolicyAsync(InsurancePolicy policy)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.UpdateAsync(policy);
            }
        }
        public async Task<bool> DeleteInsurancePolicyAsync(InsurancePolicy policy)
        {
            using (var connection = GetOpenConnection())
            {
                return await connection.DeleteAsync(policy);
            }
        }
    }
}

