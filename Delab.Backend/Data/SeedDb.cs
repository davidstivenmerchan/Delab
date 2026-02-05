using Delab.AccessData.Data;
using Delab.Shared.Entities;

namespace Delab.Backend.Data;

public class SeedDb
{
    private readonly DataContext _context;
    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCountries();
    }

    private async Task CheckCountries()
    {
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                CodPhone = "+57",
                States = new List<State>
                {
                    new State
                    {
                        Name = "Antioquia",
                        Cities = new List<City>
                        {
                            new City { Name = "Medellín" },
                            new City { Name = "Envigado" },
                            new City { Name = "Bello" },
                        }
                    },
                    new State
                    {
                        Name = "Cundinamarca",
                        Cities = new List<City>
                        {
                            new City { Name = "Bogotá" },
                            new City { Name = "Soacha" },
                            new City { Name = "Chía" },
                        }
                    }
                }
            });
            _context.Countries.Add(new Country
            {
                Name = "Argentina",
                CodPhone = "+54",
                States = new List<State>
                {
                    new State
                    {
                        Name = "Buenos Aires",
                        Cities = new List<City>
                        {
                            new City { Name = "La Plata" },
                            new City { Name = "Mar del Plata" },
                            new City { Name = "Bahía Blanca" },
                        }
                    },
                    new State
                    {
                        Name = "Córdoba",
                        Cities = new List<City>
                        {
                            new City { Name = "Córdoba" },
                            new City { Name = "Villa Carlos Paz" },
                            new City { Name = "Río Cuarto" },
                        }
                    }
                }
            });
            _context.Countries.Add(new Country
            {
                Name = "México",
                CodPhone = "+52",
                States = new List<State>
                {
                    new State
                    {
                        Name = "Jalisco",
                        Cities = new List<City>
                        {
                            new City { Name = "Guadalajara" },
                            new City { Name = "Zapopan" },
                            new City { Name = "Tlaquepaque" },
                        }
                    },
                    new State
                    {
                        Name = "Nuevo León",
                        Cities = new List<City>
                        {
                            new City { Name = "Monterrey" },
                            new City { Name = "San Nicolás de los Garza" },
                            new City { Name = "Guadalupe" },
                        }
                    }
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
