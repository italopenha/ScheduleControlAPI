using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    public interface IPatient
    {
        Task CreatePatientAsync(CreatePatientDTO patientDTO);
        Task DeletePatientAsync(int id);
        Task<PatientModel> GetPatientByNameAsync(string name);
        Task<PatientModel> GetPatientByIdAsync(int id);
        Task<List<PatientModel>> GetAllPatientsAsync();
    }
}
