using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ScheduleControl.src.models;
using System;

namespace ScheduleControl.src.models
{
    [Table("tb_appointments")]
    public class AppointmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [ForeignKey("fk_doctor")]
        public DoctorModel Doctor { get; set; }

        [ForeignKey("fk_patient")]
        public PatientModel Patient { get; set; }
    }
}
