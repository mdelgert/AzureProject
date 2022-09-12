var customers = CustomerFake.Get(100);

foreach(var customer in customers)
{
    Console.WriteLine($"{customer.FirstName} {customer.LastName}");
}

Console.ReadKey();