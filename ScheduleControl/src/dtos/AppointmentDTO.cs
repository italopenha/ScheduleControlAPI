using System.ComponentModel.DataAnnotations;
using System;

namespace ScheduleControl.src.dtos
{
    public class CreateAppointmentDTO
    {
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public CreateDoctorDTO Doctor { get; set; }

        [Required]
        public CreatePatientDTO Patient { get; set; }

        public CreateAppointmentDTO(DateTime start, DateTime end, CreateDoctorDTO doctor, CreatePatientDTO patient)
        {
            Start = start;
            End = end;
            Doctor = doctor;
            Patient = patient;
        }
    }

    public class UpdateAppointmentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        public CreateDoctorDTO Doctor { get; set; }

        public UpdateAppointmentDTO(int id, DateTime start, DateTime end, CreateDoctorDTO doctor)
        {
            Id = id;
            Start = start;
            End = end;
            Doctor = doctor;
        }
    }
}
