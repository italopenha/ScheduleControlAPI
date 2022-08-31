using System.ComponentModel.DataAnnotations;

namespace ScheduleControl.src.dtos
{
    /// <summary>
    /// <para>Summary: Mirror class responsible for creating new patients </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 30/08/2022</para>
    /// </summary>
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
