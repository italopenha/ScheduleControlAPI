using System.ComponentModel.DataAnnotations;

namespace ScheduleControl.src.dtos
{
    /// <summary>
    /// <para>Summary: Mirror class responsible for creating new doctors </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 30/08/2022</para>
    /// </summary>
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

    /// <summary>
    /// <para>Summary: Mirror class responsible for updating doctors </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 30/08/2022</para>
    /// </summary>
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
