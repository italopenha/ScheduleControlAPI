using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ScheduleControl.src.models
{
    /// <summary>
    /// <para>Resume: Class responsible for creating doctors in the database </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>

    [Table("tb_doctors")]
    public class DoctorModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Specialty { get; set; }

        [JsonIgnore]

        public List<AppointmentModel> MyAppointments { get; set; }
    }
}
