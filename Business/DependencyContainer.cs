using Business.Interfaces;
using Business.Services;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class DependencyContainer
{
    public static IServiceCollection AddAplicationDependency(this IServiceCollection services, IConfiguration configuration)
    {
        #region Tables
        services.AddDataDependency(configuration);
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IBeginSessionService, BeginSessionService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IStudentSubjectService, StudentSubjectService>();
        services.AddScoped<IProfessorSubjectService, ProfessorSubjectService>();
        #endregion

        return services;
    }
}
