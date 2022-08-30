using System.ComponentModel.DataAnnotations;

namespace ScheduleControl.src.dtos
{
    public class CreateDoctorDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Specialty { get; set; }

        public CreateDoctorDTO(string name, string specialty)
        {
            Name = name;
            Specialty = specialty;
        }
    }

    public class UpdateDoctorDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Specialty { get; set; }

        public UpdateDoctorDTO(int id, string specialty)
        {
            Id = id;
            Specialty = specialty;
        }
    }
}
