using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories.implementations
{
    public class AppointmentRepository : IAppointment
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        public AppointmentRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        public async Task CreateAppointmentAsync(CreateAppointmentDTO appointmentDTO)
        {
            await _context.Appointments.AddAsync(new AppointmentModel
            {
                Time = appointmentDTO.Time,
                Doctor = _context.Doctors.FirstOrDefault(d => d.Name == appointmentDTO.Doctor.Name),
                Patient = _context.Patients.FirstOrDefault(p => p.Name == appointmentDTO.Doctor.Name)
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            _context.Appointments.Remove(await GetAppointmentByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<AppointmentModel>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(d => d.Doctor)
                .Include(p => p.Patient)
                .ToListAsync();
        }

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
