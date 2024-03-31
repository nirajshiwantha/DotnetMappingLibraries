using System.Text.Json;
using AutoMapper;
using MappingSample.Dtos;
using MappingSample.Mappings;
using MappingSample.Models;
using Mapster;

public class Program {
    public static void Main(string[] args)
    {
        Engine engine = new()
        {
            Type = "V8",
            Horsepower = 720
        };

        Owner owner = new()
        {
            Name = "Niraj Ranasinghe",
            ContactNumber = "94123456789",
            PurchaseDate = DateTime.Now
        };

        Car car = new()
        {
            Id = 101,
            Make = "McLaren",
            Model = "720S",
            Year = 2023,
            Color = "Red", 
            Price = 400000, 
            Engine = engine,
            Transmission = TransmissionType.Automatic,
            Fuel = FuelType.Gasoline, 
            Body = BodyStyle.Coupe, 
            SerialNumbers = new[] { 12345, 67890 },
            Features = new List<string> { "Carbon fiber body", "Mid-engine layout", "Active suspension" },
            Options = new Dictionary<string, string>
                {
                    { "Color", "Red" },
                    { "Interior", "Black leather" },
                    { "Wheels", "20-inch lightweight alloy" }
                },
            ManufactureDate = new DateTime(2024, 3, 1), 
            WarrantyPeriod = TimeSpan.FromDays(730),
            IsFourWheelDrive = false,
            OwnerInfo = owner
        };

        void Mapperly()
        {
            var mapper = new CarMapperMapperly();
            CarDto carDto = mapper.CartoCarDto(car);
            carDto.EngineData = mapper.EnginetoEngineDto(engine);
            carDto.OwnerDetails = mapper.OwnertoOwnerDto(owner);
            var output = JsonSerializer.Serialize(carDto, new JsonSerializerOptions
            {
                WriteIndented = true,
                IndentCharacter = '\t',
                IndentSize = 1
            });
            Console.WriteLine(output);
        }

        void Mapster()
        {
            var mapperMapster = new CarMapperMapster();
            mapperMapster.ConfigureMapster();
            var carDto = car.Adapt<CarDto>();
            var output = JsonSerializer.Serialize(carDto, new JsonSerializerOptions
            {
                WriteIndented = true,
                IndentCharacter = '\t',
                IndentSize = 1
            });
            Console.WriteLine(output);
        }

        void AutoMapper()
        {
            CarMapperAutoMapper carMapperAutoMapper = new CarMapperAutoMapper();
            IMapper autoMapper = new Mapper(carMapperAutoMapper.ConfigureAutoMapper());
            CarDto carDto = autoMapper.Map<CarDto>(car);
            var output = JsonSerializer.Serialize(carDto, new JsonSerializerOptions
            {
                WriteIndented = true,
                IndentCharacter = '\t',
                IndentSize = 1
            });
            Console.WriteLine(output);
        }


        Console.WriteLine("-------------- AutoMapper Output --------------");
        AutoMapper();
        Console.WriteLine();

        Console.WriteLine("-------------- Mapster Output --------------");
        Mapster();
        Console.WriteLine();

        Console.WriteLine("-------------- Mapperly Output --------------");
        Mapperly();
    }
}


