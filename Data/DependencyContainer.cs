using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DependencyContainer
{
    public static IServiceCollection AddDataDependency(this IServiceCollection services, IConfiguration configuration)
    {
        #region Tables
        services.AddDbContext<ContextInter>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("ConnectionMain"), sql =>
            sql.CommandTimeout(120)
        ));
        services.AddScoped<IStudentData, StudentData>();
        services.AddScoped<IProfessorData, ProfessorData>();
        services.AddScoped<ISubjectData, SubjectData>();
        services.AddScoped<IProfessorSubjectData, ProfessorSubjectData>();
        services.AddScoped<IStudentSubjectData, StudentSubjectData>();
       

        #endregion

        return services;
    }
}
