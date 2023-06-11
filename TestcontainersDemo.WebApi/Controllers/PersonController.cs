using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestcontainersDemo.Infrastructure.Postgres;
using TestcontainersDemo.Infrastructure.Postgres.Models;
using TestcontainersDemo.WebApi.Models.Request;
using TestcontainersDemo.WebApi.Models.Response;

namespace TestcontainersDemo.WebApi.Controllers;

[Route("[controller]")]
public sealed class PersonController : ControllerBase {
    private readonly PersonContext _personContext;

    public PersonController(PersonContext personContext) {
        _personContext = personContext;
    }

    /// <summary>
    /// Retrieve all persons from the database.
    /// </summary>
    /// <param name="pageNumber">Which page of persons to fetch.</param>
    /// <param name="pageSize">The amount of persons to retrieve.</param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<Person>), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NoContent)]
    public async Task<IActionResult> Get(int pageNumber = 1, byte pageSize = 10) {
        var records = await _personContext.Persons
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();

        if (records.Count == 0) {
            return NoContent();
        }

        return Ok(records);
    }

    /// <summary>
    /// Retrieve a single person record from the database.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(CreatePersonResponse), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(Guid id) {
        var record = await _personContext.Persons.FirstOrDefaultAsync(person => person.Id == id);

        if (record is null) {
            return NotFound();
        }

        return Ok(new CreatePersonResponse(record));
    }

    /// <summary>
    /// Create a new person record in the DB.
    /// </summary>
    /// <param name="request">The person record to create.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePersonResponse), (int) HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ProblemDetails), (int) HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequest request) {
        var personRecordForDatabase = request.ToDatabasePerson();

        _personContext.Persons.Add(personRecordForDatabase);
        await _personContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new {id = personRecordForDatabase.Id}, new CreatePersonResponse(personRecordForDatabase));
    }
}