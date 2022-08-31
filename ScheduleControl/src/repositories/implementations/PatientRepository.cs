using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories.implementations
{
    /// <summary>
    /// <para>Resume: Implementing methods and constructors for the patient class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    public class PatientRepository : IPatient
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// <para>Resume: Constructor of the class </para>
        /// </summary>
        /// <param name="context">ScheduleControlContext</param>
        public PatientRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// <para>Resume: Asynchronous method to create a patient </para>
        /// </summary>
        public async Task CreatePatientAsync(CreatePatientDTO patientDTO)
        {
            await _context.Patients.AddAsync(new PatientModel
            {
                Name = patientDTO.Name,
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to delete a patient </para>
        /// </summary>
        public async Task DeletePatientAsync(int id)
        {
            _context.Patients.Remove(await GetPatientByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get all patients </para>
        /// </summary>
        public async Task<List<PatientModel>> GetAllPatientsAsync()
        {
            return await _context.Patients
                .Include(p => p.MyAppointments)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a patient by id </para>
        /// </summary>
        public async Task<PatientModel> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a patient by name </para>
        /// </summary>
        public async Task<PatientModel> GetPatientByNameAsync(string name)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.Name == name);
        }

        #endregion
    }
}
