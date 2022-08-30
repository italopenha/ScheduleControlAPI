using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    public interface IDoctor
    {
        Task CreateDoctorAsync(CreateDoctorDTO doctordto);
        Task UpdateDoctorAsync(UpdateDoctorDTO doctordto);
        Task DeleteDoctorAsync(int id);
        Task<List<DoctorModel>> GetAllDoctors();
        Task<DoctorModel> GetDoctorByIdAsync(int id);
        Task<List<DoctorModel>> GetDoctorBySpecialtyAsync(string specialty);
    }
}
