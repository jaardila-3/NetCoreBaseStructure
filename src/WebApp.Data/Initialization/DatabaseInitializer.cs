using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Common.Configurations;
using WebApp.Common.Constants;
using WebApp.Common.Interfaces;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Initialization;

public class DatabaseInitializer(IUnitOfWork unitOfWork, WebAppDbContext context, IOptions<AdminSettings> adminSettings, ILogger<DatabaseInitializer> logger, IPasswordHasher passwordHasher)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly WebAppDbContext _context = context;
    private readonly AdminSettings _adminSettings = adminSettings.Value;
    private readonly ILogger<DatabaseInitializer> _logger = logger;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task InitializeAsync(bool migrateDatabase)
    {
        try
        {
            if (migrateDatabase)
            {
                _logger.LogInformation("Iniciando migración de la base de datos...");
                await _context.Database.MigrateAsync();
                _logger.LogInformation("Migración completada.");
            }

            await SeedRolesAsync();
            await SeedAdminUserAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al inicializar la base de datos.");
            throw; // Propagate the exception
        }
    }

    private async Task SeedRolesAsync()
    {
        var roles = new List<Role>
            {
                new() { Name = Roles.ADMIN, Description = "Rol de administrador" },
                new() { Name = Roles.USER, Description = "Rol de usuario estándar" }
            };

        foreach (var role in roles)
        {
            var existingRole = await _unitOfWork.Repository<Role>().GetFirstAsync(r => r.Name.Equals(role.Name));
            if (existingRole is null)
            {
                await _unitOfWork.Repository<Role>().AddAsync(role);
                _logger.LogInformation($"Rol {role.Name} creado.");
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        var adminUser = await _unitOfWork.Repository<User>().GetFirstAsync(u => u.Username.Equals(_adminSettings.Username));
        if (adminUser is null)
        {
            adminUser = new User
            {
                Name = _adminSettings.Name,
                LastName = _adminSettings.LastName,
                IdentificationNumber = _adminSettings.IdentificationNumber,
                Username = _adminSettings.Username,
                Email = _adminSettings.Email,
                PasswordHash = _passwordHasher.Hash(_adminSettings.Password),
                CreatedAt = DateTime.Now,
                CreatedBy = "System"
            };

            await _unitOfWork.Repository<User>().AddAsync(adminUser);

            var adminRole = await _unitOfWork.Repository<Role>().GetFirstAsync(r => r.Name.Equals(Roles.ADMIN));
            if (adminRole is not null)
            {
                var userRole = new UserRole
                {
                    UserId = adminUser.Id,
                    RoleId = adminRole.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System"
                };
                await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
                _logger.LogInformation($"Usuario administrador creado y asignado al rol {Roles.ADMIN}.");
            }
        }
        else
        {
            _logger.LogInformation("El usuario administrador ya existe.");
        }
    }
}