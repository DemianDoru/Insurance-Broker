namespace InsuranceBroker
{
    public class Program
    {
        static string connectionString = "Data Source= DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        static DataAccess.InsuranceDataAccess dataAccess = new DataAccess.InsuranceDataAccess(connectionString);
        public static async Task Main()
        {
            // ArrayExample();
            // ListExample();
            //IEnumerableExample();
            //DeferredExecutionExample();
            //HashSetExample();
            //DictionaryExample();
            //InsurancePolicyExample();
            //InsuranceDataAccessInsertExample();
            //InsuranceDataAccessUpdateExample();
            //InsuranceDataAccessDeleteExample();
            //InsuranceDataAccessExample();
            //InsuranceDataAccessGetPoliciesExample();
            // await DapperInsuranceDataAccessPersonsExampleAsync();
            //await DapperInsuranceDataAccessVehiclesExampleAsync();
            await ApplicationMenu();
        }


        public static async Task ApplicationMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1: Manage Persons");
                Console.WriteLine("2: Manage Vehicles");
                Console.WriteLine("3: Manage Insurance Policies");
                Console.WriteLine("4: Exit");
                Console.Write("Select an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        ManagePersons();
                        break;
                    case 2:
                        ManageVehicles();
                        break;
                    case 3:
                        ManageInsurancePolicies();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }


        static async Task ManageInsurancePolicies()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n-- Insurance Policy Management --");
                Console.WriteLine("1: Add an Insurance Policy");
                Console.WriteLine("2: View an Insurance Policy by ID");
                Console.WriteLine("3: View all Insurance Policies");
                Console.WriteLine("4: Update an Insurance Policy");
                Console.WriteLine("5: Delete an Insurance Policy");
                Console.WriteLine("6: Return to Main Menu");
                Console.Write("Choose an action: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        await AddInsurancePolicy();
                        break;
                    case 2:
                        await ViewInsurancePolicyById();
                        break;
                    case 3:
                        await ViewAllInsurancePolicies();
                        break;
                    case 4:
                        await UpdateInsurancePolicy();
                        break;
                    case 5:
                        await DeleteInsurancePolicy();
                        break;
                    case 6:
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        static async Task AddInsurancePolicy()
        {
            try
            {
                Console.Write("Enter Person ID (optional, press ENTER to skip): ");
                string personIdInput = Console.ReadLine();
                int? personId = string.IsNullOrWhiteSpace(personIdInput) ? null : int.Parse(personIdInput);
                Console.Write("Enter Vehicle ID (optional, press ENTER to skip): ");
                string vehicleIdInput = Console.ReadLine();
                int? vehicleId = string.IsNullOrWhiteSpace(vehicleIdInput) ? null : int.Parse(vehicleIdInput);
                Console.Write("Enter Duration in Months: ");
                int durationMonths = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter Max Insured Value: ");
                decimal maxInsuredValue = Convert.ToDecimal(Console.ReadLine());
                DataAccess.InsurancePolicy newPolicy = new DataAccess.InsurancePolicy
                {
                    PersonID = personId,
                    VehicleID = vehicleId,
                    DurationMonths = durationMonths,
                    MaxInsuredValue = maxInsuredValue
                };
                long policyId = await dataAccess.InsertInsurancePolicyAsync(newPolicy);
                Console.WriteLine($"Insurance Policy added with ID: {policyId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task ViewInsurancePolicyById()
        {
            try
            {
                Console.Write("Enter Insurance Policy ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.InsurancePolicy policy = await dataAccess.GetInsurancePolicyByIdAsync(id);
                if (policy != null)
                {
                    // Build the display string dynamically based on available data
                    var details = new List<string>
{
$"ID: {policy.PolicyID}",
$"Duration: {policy.DurationMonths} months",
$"Max Insured Value: {policy.MaxInsuredValue:C}"
};
                    if (policy.PersonID.HasValue) // Check if there is a Person ID
                    {
                        details.Insert(1, $"Person ID: {policy.PersonID}");
                    }
                    if (policy.VehicleID.HasValue) // Check if there is a Vehicle ID
                    {
                        details.Insert(1, $"Vehicle ID: {policy.VehicleID}");
                    }
                    // Combine the details into a single string for display
                    Console.WriteLine(string.Join(", ", details));
                }
                else
                {
                    Console.WriteLine("Insurance Policy not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task ViewAllInsurancePolicies()
        {
            try
            {
                IEnumerable<DataAccess.InsurancePolicy> policies = await dataAccess.GetAllInsurancePoliciesAsync();
                foreach (DataAccess.InsurancePolicy policy in policies)
                {
                    // Build the display string dynamically based on available data
                    var details = new List<string>
{
$"ID: {policy.PolicyID}",
$"Duration: {policy.DurationMonths} months",
$"Max Insured Value: {policy.MaxInsuredValue:C}"
};
                    if (policy.PersonID.HasValue) // Check if there is a Person ID
                    {
                        details.Insert(1, $"Person ID: {policy.PersonID}");
                    }
                    if (policy.VehicleID.HasValue) // Check if there is a Vehicle ID
                    {
                        details.Insert(1, $"Vehicle ID: {policy.VehicleID}");
                    }
                    // Combine the details into a single string for display
                    Console.WriteLine(string.Join(", ", details));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task UpdateInsurancePolicy()
        {
            try
            {
                Console.Write("Enter Insurance Policy ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.InsurancePolicy policyToUpdate = await dataAccess.GetInsurancePolicyByIdAsync(id);
                if (policyToUpdate == null)
                {
                    Console.WriteLine("Insurance Policy not found.");
                    return;
                }
                Console.Write("Update Person ID (optional, press ENTER to skip): ");
                string personIdInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(personIdInput))
                    policyToUpdate.PersonID = int.Parse(personIdInput);
                Console.Write("Update Vehicle ID (optional, press ENTER to skip): ");
                string vehicleIdInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(vehicleIdInput))
                    policyToUpdate.VehicleID = int.Parse(vehicleIdInput);
                Console.Write("Update Duration in Months (optional, press ENTER to skip): ");
                string durationInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(durationInput))
                    policyToUpdate.DurationMonths = int.Parse(durationInput);
                Console.Write("Update Max Insured Value (optional, press ENTER to skip): ");
                string maxValueInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(maxValueInput))
                    policyToUpdate.MaxInsuredValue = decimal.Parse(maxValueInput);
                bool updated = await dataAccess.UpdateInsurancePolicyAsync(policyToUpdate);
                Console.WriteLine(updated ? "Insurance Policy updated successfully." : "Failed to update insurance policy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task DeleteInsurancePolicy()
        {
            try
            {
                Console.Write("Enter Insurance Policy ID to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.InsurancePolicy policyToDelete = await dataAccess.GetInsurancePolicyByIdAsync(id);
                if (policyToDelete == null)
                {
                    Console.WriteLine("Insurance Policy not found.");
                    return;
                }
                bool deleted = await dataAccess.DeleteInsurancePolicyAsync(policyToDelete);
                Console.WriteLine(deleted ? "Insurance Policy deleted successfully." : "Failed to delete insurance policy.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        static async Task ManageVehicles()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n-- Vehicle Management --");
                Console.WriteLine("1: Add a Vehicle");
                Console.WriteLine("2: View a Vehicle by ID");
                Console.WriteLine("3: View all Vehicles");
                Console.WriteLine("4: Update a Vehicle");
                Console.WriteLine("5: Delete a Vehicle");
                Console.WriteLine("6: Return to Main Menu");
                Console.Write("Choose an action: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        await AddVehicle();
                        break;
                    case 2:
                        await ViewVehicleById();
                        break;
                    case 3:
                        await ViewAllVehicles();
                        break;
                    case 4:
                        await UpdateVehicle();
                        break;
                    case 5:
                        await DeleteVehicle();
                        break;
                    case 6:
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        static async Task AddVehicle()
        {
            try
            {
                Console.Write("Enter Make: ");
                string make = Console.ReadLine();
                Console.Write("Enter Model: ");
                string model = Console.ReadLine();
                DataAccess.Vehicle newVehicle = new DataAccess.Vehicle { Make = make, Model = model };
                long vehicleId = await dataAccess.InsertVehicleAsync(newVehicle);
                Console.WriteLine($"Vehicle added with ID: {vehicleId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task ViewVehicleById()
        {
            try
            {
                Console.Write("Enter Vehicle ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.Vehicle vehicle = await dataAccess.GetVehicleByIdAsync(id);
                if (vehicle != null)
                {
                    Console.WriteLine($"ID: {vehicle.ID}, Make: {vehicle.Make}, Model: {vehicle.Model}");
                }
                else
                {
                    Console.WriteLine("Vehicle not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task ViewAllVehicles()
        {
            try
            {
                IEnumerable<DataAccess.Vehicle> vehicles = await dataAccess.GetAllVehicleAsync();
                foreach (DataAccess.Vehicle vehicle in vehicles)
                {
                    Console.WriteLine($"ID: {vehicle.ID}, Make: {vehicle.Make}, Model: {vehicle.Model}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task UpdateVehicle()
        {
            try
            {
                Console.Write("Enter Vehicle ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.Vehicle vehicleToUpdate = await dataAccess.GetVehicleByIdAsync(id);
                if (vehicleToUpdate == null)
                {
                    Console.WriteLine("Vehicle not found.");
                    return;
                }
                Console.Write("Enter new Make (leave empty to keep current): ");
                string make = Console.ReadLine();
                Console.Write("Enter new Model (leave empty to keep current): ");
                string model = Console.ReadLine();
                if (!string.IsNullOrEmpty(make))
                    vehicleToUpdate.Make = make;
                if (!string.IsNullOrEmpty(model))
                    vehicleToUpdate.Model = model;
                bool updated = await dataAccess.UpdateVehicleAsync(vehicleToUpdate);
                Console.WriteLine(updated ? "Vehicle updated successfully." : "Failed to update vehicle.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        static async Task DeleteVehicle()
        {
            try
            {
                Console.Write("Enter Vehicle ID to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());
                DataAccess.Vehicle vehicleToDelete = await dataAccess.GetVehicleByIdAsync(id);
                if (vehicleToDelete == null)
                {
                    Console.WriteLine("Vehicle not found.");
                    return;
                }
                bool deleted = await dataAccess.DeleteVehicleAsync(vehicleToDelete);
                Console.WriteLine(deleted ? "Vehicle deleted successfully." : "Failed to delete vehicle.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static async Task ManagePersons()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n-- Person Management --");
                Console.WriteLine("1: Add a Person");
                Console.WriteLine("2: View a Person by ID");
                Console.WriteLine("3: View all Persons");
                Console.WriteLine("4: Update a Person");
                Console.WriteLine("5: Delete a Person");
                Console.WriteLine("6: Return to Main Menu");
                Console.Write("Choose an action: ");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        await AddPerson();
                        break;
                    case 2:
                        await ViewPersonById();
                        break;
                    case 3:
                        await ViewAllPersons();
                        break;
                    case 4:
                        await UpdatePerson();
                        break;
                    case 5:
                        await DeletePerson();
                        break;
                    case 6:
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }

            static async Task AddPerson()
            {
                try
                {
                    Console.Write("Enter First Name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter Last Name: ");
                    string lastName = Console.ReadLine();

                    DataAccess.Person newPerson = new DataAccess.Person { FirstName = firstName, LastName = lastName };
                    long personId = await dataAccess.InsertPersonAsync(newPerson);
                    Console.WriteLine($"Person added with ID: {personId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            static async Task ViewPersonById()
            {
                try
                {
                    Console.Write("Enter Person ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DataAccess.Person person = await dataAccess.GetPersonByIdAsync(id);
                    if (person != null)
                    {
                        Console.WriteLine($"ID: {person.ID}, First Name: {person.FirstName}, Last Name: {person.LastName}");
                    }
                    else
                    {
                        Console.WriteLine("Person not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            static async Task ViewAllPersons()
            {
                try
                {
                    IEnumerable<DataAccess.Person> persons = await dataAccess.GetAllPersonsAsync();
                    foreach (DataAccess.Person person in persons)
                    {
                        Console.WriteLine($"ID: {person.ID}, First Name: {person.FirstName}, Last Name: {person.LastName}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            static async Task UpdatePerson()
            {
                try
                {
                    Console.Write("Enter Person ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DataAccess.Person personToUpdate = await dataAccess.GetPersonByIdAsync(id);
                    if (personToUpdate == null)
                    {
                        Console.WriteLine("Person not found.");
                        return;
                    }
                    Console.Write("Enter new First Name (leave empty to keep current): ");
                    string firstName = Console.ReadLine();
                    Console.Write("Enter new Last Name (leave empty to keep current): ");
                    string lastName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(firstName))
                        personToUpdate.FirstName = firstName;
                    if (!string.IsNullOrEmpty(lastName))
                        personToUpdate.LastName = lastName;
                    bool updated = await dataAccess.UpdatePersonAsync(personToUpdate);
                    Console.WriteLine(updated ? "Person updated successfully." : "Failed to update person.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            static async Task DeletePerson()
            {
                try
                {
                    Console.Write("Enter Person ID to delete: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    DataAccess.Person personToDelete = await dataAccess.GetPersonByIdAsync(id);
                    if (personToDelete == null)
                    {
                        Console.WriteLine("Person not found.");
                        return;
                    }
                    bool deleted = await dataAccess.DeletePersonAsync(personToDelete);
                    Console.WriteLine(deleted ? "Person deleted successfully." : "Failed to delete person.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        //        static async Task DapperInsuranceDataAccessVehiclesExampleAsync()
        //{

        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";

        //    var dataAccess = new DataAccess.InsuranceDataAccess(connectionString);

        //    var newVehicle = new DataAccess.Vehicle { Make = "Audi", Model = "A4" };
        //    long vehicleId = await dataAccess.InsertVehicleAsync(newVehicle);

        //    //Retrieving all vehicles
        //    var vehicles = await dataAccess.GetAllVehicleAsync();
        //    Console.WriteLine("All vehicles in database after insertion:");
        //    foreach (var vehicle in vehicles)
        //    {
        //        Console.WriteLine($"ID: {vehicle.ID}, Make: {vehicle.Make}, Model: {vehicle.Model}");
        //    }

        //    // Updating the inserted vehicle
        //    var vehiclesToUpdate = vehicles.FirstOrDefault(v => v.ID == vehicleId);
        //    if (vehiclesToUpdate != null)
        //    {
        //        vehiclesToUpdate.ID = 1;
        //        bool updateSuccess = await dataAccess.UpdateVehicleAsync(vehiclesToUpdate);
        //        Console.WriteLine(updateSuccess ? $"Updated Person with ID: {vehiclesToUpdate.ID}" : "Failed to update person.");
        //    }

        //    // Retrieving the updated vehicle
        //    var updatedVehicle = await dataAccess.GetVehicleByIdAsync((int)vehicleId);
        //    if (updatedVehicle != null)
        //    {
        //        Console.WriteLine($"Retrieved Updated Vehicle: {updatedVehicle.Make} {updatedVehicle.Model}");
        //    }
        //    // Deleting the updated vehicle
        //    if (updatedVehicle != null)
        //    {
        //        bool deleteSuccess = await dataAccess.DeleteVehicleAsync(updatedVehicle);
        //        Console.WriteLine(deleteSuccess ? $"Deleted Vehicle with ID: {updatedVehicle.ID}" : "Failed to delete vehicle.");
        //    }
        //    // Retrieving all persons to confirm deletion
        //    vehicles = await dataAccess.GetAllVehicleAsync();
        //    Console.WriteLine("All vehicles in database after deletion:");
        //    foreach (var vehicle in vehicles)
        //    {
        //        Console.WriteLine($"ID: {vehicle.ID}, Make: {vehicle.Make}, Model: {vehicle.Model}");
        //    }

        //}
        //static async Task DapperInsuranceDataAccessPersonsExampleAsync()
        //{
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";

        //    var dataAccess = new DataAccess.InsuranceDataAccess(connectionString);
        //    // Inserting a new person
        //    var newPerson = new DataAccess.Person { FirstName = "Nathan", LastName = "Jackson" };
        //    long personId = await dataAccess.InsertPersonAsync(newPerson);
        //    Console.WriteLine($"Inserted person with ID: {personId}");
        //    // Retrieving all persons
        //    var persons = await dataAccess.GetAllPersonsAsync();
        //    Console.WriteLine("All persons in database after insertion:");
        //    foreach (var person in persons)
        //    {
        //        Console.WriteLine($"ID: {person.ID}, Name: {person.FirstName} {person.LastName}");
        //    }

        //    // Updating the inserted person
        //    var personToUpdate = persons.FirstOrDefault(p => p.ID == personId);
        //    if (personToUpdate != null)
        //    {
        //        personToUpdate.LastName = "Ingram";
        //        bool updateSuccess = await dataAccess.UpdatePersonAsync(personToUpdate);
        //        Console.WriteLine(updateSuccess ? $"Updated Person with ID: {personToUpdate.ID}" : "Failed to update person.");
        //    }
        //    // Retrieving the updated person
        //    var updatedPerson = await dataAccess.GetPersonByIdAsync((int)personId);
        //    if (updatedPerson != null)
        //    {
        //        Console.WriteLine($"Retrieved Updated Person: {updatedPerson.FirstName} {updatedPerson.LastName}");
        //    }
        //    // Deleting the updated person
        //    if (updatedPerson != null)
        //    {
        //        bool deleteSuccess = await dataAccess.DeletePersonAsync(updatedPerson);
        //        Console.WriteLine(deleteSuccess ? $"Deleted Person with ID: {updatedPerson.ID}" : "Failed to delete person.");
        //    }
        //    // Retrieving all persons to confirm deletion
        //    persons = await dataAccess.GetAllPersonsAsync();
        //    Console.WriteLine("All persons in database after deletion:");
        //    foreach (var person in persons)
        //    {
        //        Console.WriteLine($"ID: {person.ID}, Name: {person.FirstName} {person.LastName}");
        //    }
        //}
        //static void InsuranceDataAccessGetPoliciesExample()
        //{
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        //    InsuranceDataAccess dataAccess = new InsuranceDataAccess(connectionString);
        //    dataAccess.GetInsurancePolicies();
        //}
        //static void InsuranceDataAccessDeleteExample()
        //{
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        //    InsuranceDataAccess dataAccess = new InsuranceDataAccess(connectionString);
        //    int personIdToDelete = 5; // Example person ID
        //    int vehicleIdToDelete = 5; // Example vehicle ID
        //    dataAccess.DeletePerson(personIdToDelete);
        //    dataAccess.DeleteVehicle(vehicleIdToDelete);
        //}


        //static void InsuranceDataAccessUpdateExample()
        //{
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        //    InsuranceDataAccess dataAccess = new InsuranceDataAccess(connectionString);
        //    int personIdToUpdate = 5; // Example person ID
        //    Person updatedPerson = new Person { FirstName = "Lionel", LastName = "Thornhill" };
        //    int vehicleIdToUpdate = 5; // Example vehicle ID
        //    Vehicle updatedVehicle = new Vehicle { Make = "Peugeot", Model = "308" };
        //    dataAccess.EditPerson(personIdToUpdate, updatedPerson);
        //    dataAccess.EditVehicle(vehicleIdToUpdate, updatedVehicle);
        //}
        //static void InsuranceDataAccessInsertExample()
        //{
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        //    InsuranceDataAccess dataAccess = new InsuranceDataAccess(connectionString);
        //    Person newPerson = new Person { FirstName = "Lionel", LastName = "Fusco" };
        //    Vehicle newVehicle = new Vehicle { Make = "Peugeot", Model = "3008" };
        //    dataAccess.InsertPerson(newPerson);
        //    dataAccess.InsertVehicle(newVehicle);
        //}

        //static void InsuranceDataAccessExample()
        //{
        //    // We define our database connection string, this is based on server name, and the name of the Database
        //    string connectionString = "Data Source = DESKTOP-4LIGH8T\\SQLEXPRESS01; Integrated Security=SSPI; Initial Catalog = InsuranceBrokerDB; TrustServerCertificate=True";
        //    InsuranceDataAccess dataAccess = new InsuranceDataAccess(connectionString);
        //    // Fetch and display persons
        //    List<Person> persons = dataAccess.GetPersons();
        //    Console.WriteLine("Persons:");
        //    foreach (Person person in persons)
        //    {
        //        Console.WriteLine($"ID: {person.ID}, Name: {person.FirstName} {person.LastName}");
        //    }
        //    // Fetch and display vehicles
        //    List<Vehicle> vehicles = dataAccess.GetVehicles();
        //    Console.WriteLine("\nVehicles:");
        //    foreach (Vehicle vehicle in vehicles)
        //    {
        //        Console.WriteLine($"ID: {vehicle.ID}, Make: {vehicle.Make}, Model: {vehicle.Model}");
        //    }
        //}
        //static void ArrayExample()
        //{// Array example
        //    int[] arrayOfInts = { 1, 2, 3, 4, 5 };
        //    Console.WriteLine("Array elements:");
        //    foreach (int i in arrayOfInts)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}
        //static void ListExample()
        //{
        //    // List example
        //    List<int> listOfInts = new List<int> { 6, 7, 8, 9, 10 };
        //    Console.WriteLine("\nList elements:");
        //    foreach (int i in listOfInts)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}


        //static IEnumerable<int> GetNumbers()
        //{
        //    for (int i = 16; i <= 20; i++)
        //    {
        //        yield return i;
        //    }
        //}

        //static void IEnumerableExample()
        //{
        //    //IEnumerable example
        //    IEnumerable<int> numbers = GetNumbers();
        //    Console.WriteLine("\nIEnumerable elements:");
        //    foreach (int number in numbers)
        //    {
        //        Console.WriteLine(number);
        //    }
        //}
        //static void DeferredExecutionExample()
        //{
        //    // Deferred Execution example with IEnumerable and LINQ
        //    List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        //    // Define a LINQ query on the list for deferred execution
        //    IEnumerable<int> evenNumbersQuery = numbers.Where(n => n % 2 == 0);

        //    Console.WriteLine("\nList has been created, and even numbers query has been defined.");
        //    Console.WriteLine("Query has not run yet.");
        //    // Change the original data before executing the querynumbers.Add(12);
        //    // Adding a new even number to the list
        //    Console.WriteLine("Added a new even number (12) to the list.");
        //    // Execute the query by iterating over it
        //    Console.WriteLine("\nEven numbers after deferred execution:");
        //    foreach (var n in evenNumbersQuery)
        //    {
        //        Console.WriteLine(n);
        //        // Now, the query actually executes}}
        //    }
        //}

        //static void HashSetExample()
        //{// HashSet example (another collection that implements IEnumerable)
        //    HashSet<int> hashSet = new HashSet<int>() { 11, 12, 13, 14, 15 };
        //    Console.WriteLine("\nHashSet elements:");
        //    foreach (int item in hashSet)
        //    {
        //        Console.WriteLine(item);
        //    }
        //}

        //static void DictionaryExample()
        //{
        //    // Creating a new Dictionary
        //    Dictionary<string, int> carInventory = new Dictionary<string, int>();
        //    // Adding key-value pairs to the Dictionary
        //    carInventory.Add("Toyota", 5);
        //    carInventory.Add("Honda", 10);
        //    carInventory.Add("Ford", 3);

        //    // Accessing a value using its key
        //    Console.WriteLine("Number of Toyotas: " + carInventory["Toyota"]);
        //    // Updating a value
        //    carInventory["Toyota"] = 8;
        //    Console.WriteLine("Updated number of Toyotas: " + carInventory["Toyota"]);

        //    // Iterating over the Dictionary
        //    Console.WriteLine("Cars in the inventory:");
        //    foreach (KeyValuePair<string, int> item in carInventory)
        //    {
        //        Console.WriteLine(item.Key + ": " + item.Value);
        //    }
        //}

        //static void InsurancePolicyExample()
        //{// Creating instances of Vehicle and Person
        //    Vehicle car = new Vehicle
        //    {
        //        ID = "V123",
        //        Make = "Toyota",
        //        Model = "Corolla"
        //    };
        //    Person owner = new Person
        //    {
        //        ID = "P456",
        //        FirstName = "John",
        //        LastName = "Doe"
        //    };
        //    // Creating instances of InsurancePolicy for each entity
        //    InsurancePolicy carPolicy = new InsurancePolicy
        //    {
        //        InsuredEntity = car,
        //        DurationMonths = 12,
        //        MaxInsuredValue = 10000m // Using 'm' to indicate decimal
        //    };

        //    InsurancePolicy ownerPolicy = new InsurancePolicy
        //    {
        //        InsuredEntity = owner,
        //        DurationMonths = 24,
        //        MaxInsuredValue = 200000m
        //    };
        //    //Displaying policy details demonstrating polymorphism
        //    Console.WriteLine("Car Insurance Policy:");
        //    carPolicy.DisplayPolicyDetails();
        //    Console.WriteLine("\nOwner Insurance Policy:");
        //    ownerPolicy.DisplayPolicyDetails();
        //}
    }
}