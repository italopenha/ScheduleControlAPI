using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    public interface IAppointment
    {
        Task CreateAppointmentAsync(CreateAppointmentDTO appointmentDTO);
        Task UpdateAppointmentAsync(UpdateAppointmentDTO appointmentDTO);
        Task DeleteAppointmentAsync(int id);
        Task<AppointmentModel> GetAppointmentByIdAsync(int id);
        Task<List<AppointmentModel>> GetAllAppointmentsAsync();
    }
}
