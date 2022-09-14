using Bogus;
using Bogus.DataSets;

namespace AzureProject.Shared.Fakes;

public static class CustomerFake
{
    public static List<CustomerModel> Get(int batchSize)
    {
        var customers = new List<CustomerModel>();

        for (var i = 1; i <= batchSize; i++)
        {
            var faker = new Faker();
            var phone = new PhoneNumbers();
            var customer = new CustomerModel
            {
                FirstName = faker.Person.FirstName,
                LastName = faker.Person.LastName,
                Address = faker.Person.Address.Street,
                City = faker.Person.Address.City,
                State = faker.Person.Address.State,
                ZipCode = faker.Person.Address.ZipCode,
                PhoneNumber = phone.PhoneNumberFormat(1)
            };
            customers.Add(customer);
        }

        return customers;
    }
}