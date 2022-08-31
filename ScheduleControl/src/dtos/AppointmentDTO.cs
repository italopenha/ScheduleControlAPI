using System.ComponentModel.DataAnnotations;
using System;

namespace ScheduleControl.src.dtos
{
    /// <summary>
    /// <para>Summary: Mirror class responsible for creating new appointments </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 30/08/2022</para>
    /// </summary>
    public class CreateAppointmentDTO
    {
        [Required]
        public string Time { get; set; }

        [Required]
        public CreateDoctorDTO Doctor { get; set; }

        [Required]
        public CreatePatientDTO Patient { get; set; }

        public CreateAppointmentDTO(string time, CreateDoctorDTO doctor, CreatePatientDTO patient)
        {
            Time = time;
            Doctor = doctor;
            Patient = patient;
        }
    }

    /// <summary>
    /// <para>Summary: Mirror class responsible for updating appointments </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 30/08/2022</para>
    /// </summary>
    public class UpdateAppointmentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Time { get; set; }

        public CreateDoctorDTO Doctor { get; set; }

        public UpdateAppointmentDTO(int id, string time, CreateDoctorDTO doctor)
        {
            Id = id;
            Time = time;
            Doctor = doctor;
        }
    }
}
