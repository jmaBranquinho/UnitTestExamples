using System;
using System.Globalization;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace DataCreationExamples;

public class AutoFixture
{
    [Fact]
    public void AutoFixture_SimpleExample()
    {
        var fixture = new Fixture();
        var person = fixture.Create<ComplexPerson>();
        person.Name.Should().NotBeEmpty();
        person.Id.Should().NotBeEmpty();
        person.Age.ToString(CultureInfo.InvariantCulture).All(char.IsDigit).Should().BeTrue();
        person.Credits.ToString(CultureInfo.InvariantCulture).All(char.IsDigit).Should().BeTrue();
        ; //put a breakpoint here and check the values in person
    }
    
    [Fact]
    public void AutoFixture_PreventLoopsOnProperties()
    {
        var fixture = new Fixture();
        var person = fixture
            .Build<SimplePerson>()
            .Without(p => p.Friends)
            .Create();
        person.Friends.Should().BeNull();
        ; //put a breakpoint here and check the values in person
        var friends = fixture
            .Build<SimplePerson>()
            .Without(p => p.Friends)
            .CreateMany();
        person.Friends = friends.ToArray();
        person.Friends.Should().NotBeEmpty();
        ; //put a breakpoint here and check the values in person
    }
}

public class ComplexPerson
{
    public string Name { get; set; }

    public Guid Id { get; set; }

    public int Age { get; set; }

    public decimal Credits { get; set; }
}

public class SimplePerson
{
    public string Name { get; set; }
    
    public SimplePerson[] Friends { get; set; }
}