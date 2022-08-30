using System.ComponentModel.DataAnnotations;
using System;

namespace ScheduleControl.src.dtos
{
    public class CreateAppointmentDTO
    {
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public CreateDoctorDTO Doctor { get; set; }

        [Required]
        public CreatePatientDTO Patient { get; set; }

        public CreateAppointmentDTO(DateTime time, CreateDoctorDTO doctor, CreatePatientDTO patient)
        {
            Time = time;
            Doctor = doctor;
            Patient = patient;
        }
    }

    public class UpdateAppointmentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public CreateDoctorDTO Doctor { get; set; }

        public UpdateAppointmentDTO(int id, DateTime time, CreateDoctorDTO doctor)
        {
            Id = id;
            Time = time;
            Doctor = doctor;
        }
    }
}
