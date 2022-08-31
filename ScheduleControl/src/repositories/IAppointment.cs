using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories
{
    /// <summary>
    /// <para> Interface to represent appointment CRUD actions </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    
    public interface IAppointment
    {
        Task CreateAppointmentAsync(CreateAppointmentDTO appointmentDTO);
        Task DeleteAppointmentAsync(int id);
        Task<AppointmentModel> GetAppointmentByIdAsync(int id);
        Task<List<AppointmentModel>> GetAllAppointmentsAsync();
    }
}
