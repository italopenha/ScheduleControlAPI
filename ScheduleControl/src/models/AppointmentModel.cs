using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ScheduleControl.src.models;
using System;

namespace ScheduleControl.src.models
{
    /// <summary>
    /// <para>Resume: Class responsible for creating appointments in the database </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>

    [Table("tb_appointments")]
    public class AppointmentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Time { get; set; }

        [ForeignKey("fk_doctor")]
        public DoctorModel Doctor { get; set; }

        [ForeignKey("fk_patient")]
        public PatientModel Patient { get; set; }
    }
}
