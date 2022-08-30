using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.models;

namespace ScheduleControl.src.data
{
    public class ScheduleControlContext : DbContext
    {
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<PatientModel> Patients { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }

        public ScheduleControlContext(DbContextOptions<ScheduleControlContext> opt) : base(opt)
        {

        }
    }
}
