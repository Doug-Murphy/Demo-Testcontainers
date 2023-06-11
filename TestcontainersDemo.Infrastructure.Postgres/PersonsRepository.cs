using Microsoft.EntityFrameworkCore;
using TestcontainersDemo.Infrastructure.Postgres.Models;

namespace TestcontainersDemo.Infrastructure.Postgres;

public sealed class PersonsRepository {
    private readonly PersonContext _personContext;

    public PersonsRepository(PersonContext personContext) {
        _personContext = personContext;
    }

    public async Task<Person> CreatePersonAsync(Person person) {
        _personContext.Persons.Add(person);
        await _personContext.SaveChangesAsync();

        return person;
    }

    public IQueryable<Person> GetAllPersons(int pageNumber, byte pageSize) {
        return _personContext.Persons
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public async Task<Person?> GetPersonByIdAsync(Guid id) {
        return await _personContext.Persons.FirstOrDefaultAsync(person => person.Id == id);
    }
}