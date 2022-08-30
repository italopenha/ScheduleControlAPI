using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories.implementations
{
    public class PatientRepository : IPatient
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        public PatientRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        public async Task CreatePatientAsync(CreatePatientDTO patientDTO)
        {
            await _context.Patients.AddAsync(new PatientModel
            {
                Name = patientDTO.Name,
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            _context.Patients.Remove(await GetPatientByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<PatientModel>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.MyAppointments)
                .ToListAsync();
        }

        public async Task<PatientModel> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PatientModel> GetPatientByNameAsync(string name)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Name == name);
        }

        #endregion
    }
}
