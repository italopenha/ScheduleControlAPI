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
    /// <para>Resume: Implementing methods and constructors for the appointment class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    public class AppointmentRepository : IAppointment
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// <para>Resume: Constructor of the class </para>
        /// </summary>
        /// <param name="context">ScheduleControlContext</param>
        public AppointmentRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// <para>Resume: Asynchronous method to create a appointment </para>
        /// </summary>
        public async Task CreateAppointmentAsync(CreateAppointmentDTO appointmentDTO)
        {
            await _context.Appointments.AddAsync(new AppointmentModel
            {
                Time = appointmentDTO.Time,
                Doctor = _context.Doctors.FirstOrDefault(d => d.Name == appointmentDTO.Doctor.Name),
                Patient = _context.Patients.FirstOrDefault(p => p.Name == appointmentDTO.Patient.Name)
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to delete a appointment </para>
        /// </summary>
        public async Task DeleteAppointmentAsync(int id)
        {
            _context.Appointments.Remove(await GetAppointmentByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get all appointments </para>
        /// </summary>
        public async Task<List<AppointmentModel>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a appointment by id </para>
        /// </summary>
        public async Task<AppointmentModel> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion
    }
}
