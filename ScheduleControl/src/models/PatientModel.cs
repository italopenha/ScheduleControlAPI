using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScheduleControl.src.models
{
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
