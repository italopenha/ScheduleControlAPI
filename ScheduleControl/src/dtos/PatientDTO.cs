using System.ComponentModel.DataAnnotations;

namespace ScheduleControl.src.dtos
{
    public class CreatePatientDTO
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        public CreatePatientDTO(string name)
        {
            Name = name;
        }
    }
}
