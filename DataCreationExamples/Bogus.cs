using System;
using Bogus;
using Xunit;

namespace DataCreationExamples;

public class Bogus
{
    [Fact]
    public void FakeAPerson()
    {
        var personGenerator = new Faker<FakePerson>()
            .RuleFor(p => p.Name, f => f.Person.FullName)
            .RuleFor(p => p.Id, Guid.NewGuid)
            .RuleFor(p => p.Email, f => f.Person.Email)
            .RuleFor(p => p.Credits, f => f.Finance.Amount());
        
        var person = personGenerator.Generate(1);
        ; // place breakpoint and check the data. Names and emails should be consistent, as well as other data
    }
}

public class FakePerson
{
    public string Name { get; set; }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public decimal Credits { get; set; }
}