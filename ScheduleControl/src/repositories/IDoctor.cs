using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    /// <summary>
    /// <para> Interface to represent doctor CRUD actions </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    
    public interface IDoctor
    {
        Task CreateDoctorAsync(CreateDoctorDTO doctordto);
        Task UpdateDoctorAsync(UpdateDoctorDTO doctordto);
        Task DeleteDoctorAsync(int id);
        Task<DoctorModel> GetDoctorByNameAsync(string name);
        Task<List<DoctorModel>> GetAllDoctorsAsync();
        Task<DoctorModel> GetDoctorByIdAsync(int id);
        Task<List<DoctorModel>> GetDoctorBySpecialtyAsync(string specialty);
    }
}
