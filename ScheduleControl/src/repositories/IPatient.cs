using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    /// <summary>
    /// <para> Interface to represent patient CRUD actions </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    
    public interface IPatient
    {
        Task CreatePatientAsync(CreatePatientDTO patientDTO);
        Task DeletePatientAsync(int id);
        Task<PatientModel> GetPatientByNameAsync(string name);
        Task<PatientModel> GetPatientByIdAsync(int id);
        Task<List<PatientModel>> GetAllPatientsAsync();
    }
}
