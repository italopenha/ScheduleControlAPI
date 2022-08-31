using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScheduleControl.src.models
{
    /// <summary>
    /// <para>Resume: Class responsible for creating patients in the database </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>

    [Table("tb_patients")]
    public class PatientModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [JsonIgnore]

        public List<AppointmentModel> MyAppointments { get; set; }
    }
}
