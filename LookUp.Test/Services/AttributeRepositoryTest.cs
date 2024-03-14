using FluentAssertions;
using Framework.Core.Enums;
using LookUp.Domain.Entities;
using LookUp.Domain.Repositories;
using LookUp.Test.MockData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace LookUp.Test.Services;

public class AttributeRepositoryTest: IDisposable
{
    protected readonly LookUpDbContext _context;
    private readonly ITestOutputHelper _output;
    public AttributeRepositoryTest(ITestOutputHelper output)
    {
        _output = output;
        var options = new DbContextOptionsBuilder<LookUpDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LookUpDbContext(options);
        _context.Database.EnsureCreated();
    }



    [Fact]
    public async Task GetTaskAsync_ReturnAttributeCollection()
    {
        //Arrange
        _context.Attributes.AddRange(AttributeMockData.GetAttributes());
        _context.SaveChanges();

        var virRepo = new AttributeRepository(_context);

        //Act
        var result = await virRepo.GetAll();
        var count = AttributeMockData.GetAttributes().Count();
        _output.WriteLine($"Result: {count}");

        //Assert
        result.Should().HaveCount(2);
        result.Should().NotHaveCount(2);
    }

    [Fact]
    public async Task GetById_ReturnAttributeCollection()
    {
        //Arrange
        _context.Attributes.AddRange(AttributeMockData.GetAttributes());
        _context.SaveChanges();

        var virRepo = new AttributeRepository(_context);
        
        //Act
        var entity = await virRepo.GetById(new Guid("4C1AD8AD-C085-4E58-81FB-ECE249DD2F4D"));
        
        //Assert
        entity.Unit.Should().Be("Pieces");
    }

    [Fact]
    public async Task Add_ReturnAttributeCollection()
    {
        //Arange
        _context.Attributes.AddRange(AttributeMockData.GetAttributes());
        _context.SaveChanges();

        var virRepo = new AttributeRepository(_context);

        var newEntity = new AttributeEntity
        {
            Id = new Guid("02B87C9C-4166-478F-87A0-73D6C0F088B1"),
            Type = AttributeTypeEnum.Decimal,
            Unit = "Liter",
            Status = RecordStatusEnum.Active
        };

        //Act
        var entity = await virRepo.Add(newEntity);
        _context.SaveChanges();

        var result = await virRepo.GetById(new Guid("02B87C9C-4166-478F-87A0-73D6C0F088B1"));
        var count = _context.Attributes.Count();

        _output.WriteLine($"Result : {JsonConvert.SerializeObject(entity)}");
        _output.WriteLine($"COunt : {count}");

        //Asert
        //entity?.Id.Should().Be(new Guid("02B87C9C-4166-478F-87A0-73D6C0F088B1"));
        entity?.Id.Should().Be(result.Id);
    }

    [Fact]
    public async Task GetById_TestAttributeType()
    {
        //Arrange
        _context.Attributes.AddRange(AttributeMockData.GetAttributes());
        _context.SaveChanges();

        var virRepo = new AttributeRepository(_context);

        //Act
        var entity = await virRepo.GetById(new Guid("4C1AD8AD-C085-4E58-81FB-ECE249DD2F4D"));

        //Assert
        Assert.IsType<AttributeEntity>(entity);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}
