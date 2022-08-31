using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using ScheduleControl.src.repositories;
using System;
using System.Threading.Tasks;

namespace ScheduleControl.src.controllers
{
    /// <summary>
    /// <para>Resume: Controllers for doctor class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [ApiController]
    [Route("api/Doctor")]
    [Produces("application/json")]
    public class DoctorController : ControllerBase
    {
        #region ATTRIBUTES

        private readonly IDoctor _repository;

        #endregion

        #region CONSTRUCTORS

        public DoctorController(IDoctor doctor)
        {
            _repository = doctor;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create a new doctor
        /// </summary>
        /// <param name="doctorDTO">CreateDoctorDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Doctor
        ///     {
        ///        "name": "Ricardo Nunes",
        ///        "specialty": "Ortopedista"
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Returns created doctor </response>
        /// <response code="400"> Request error </response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DoctorModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateDoctorAsync([FromBody] CreateDoctorDTO doctorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _repository.CreateDoctorAsync(doctorDTO);
                return Created($"api/Doctor", doctorDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Update a doctor
        /// </summary>
        /// <param name="doctorDTO">UpdateDoctorDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     PUT /api/Doctor
        ///     {
        ///        "id": 1,
        ///        "specialty": "Ortopedista"
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> Returns updated doctor </response>
        /// <response code="400"> Request error </response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult> UpdateDoctorAsync([FromBody] UpdateDoctorDTO doctorDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateDoctorAsync(doctorDTO);

            return Ok(doctorDTO);
        }

        /// <summary>
        /// Delete a doctor by id
        /// </summary>
        /// <param name="idDoctor">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204"> Doctor deleted </response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idDoctor}")]
        public async Task<ActionResult> DeleteDoctorAsync([FromRoute] int idDoctor)
        {
            await _repository.DeleteDoctorAsync(idDoctor);
            return NoContent();
        }

        /// <summary>
        /// Get all doctors
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lists all doctors</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> GetAllDoctorsAsync()
        {
            var list = await _repository.GetAllDoctorsAsync();

            if (list.Count < 1) return NotFound();
            return Ok(list);
        }

        /// <summary>
        /// Get a doctor by id
        /// </summary>
        /// <param name="idDoctor">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Returns a doctor </response>
        /// <response code="404"> Doctor does not exist </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idDoctor}")]
        public async Task<ActionResult> GetDoctorById([FromRoute] int idDoctor)
        {
            var doctor = await _repository.GetDoctorByIdAsync(idDoctor);
            if (doctor == null) return NotFound();
            return Ok(doctor);
        }

        /// <summary>
        /// Get a doctor by name
        /// </summary>
        /// <param name="nameDoctor">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Returns a doctor </response>
        /// <response code="204"> Doctor does not exist </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("name/{nameDoctor}")]
        public async Task<ActionResult> GetDoctorByNameAsync([FromRoute] string nameDoctor)
        {
            var doctor = await _repository.GetDoctorByNameAsync(nameDoctor);

            if (doctor == null) return NotFound();

            return Ok(doctor);
        }

        /// <summary>
        /// Get doctors by specialty
        /// </summary>
        /// <param name="specialty">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns doctors</response>
        /// <response code="204">Specialty does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DoctorModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("specialty")]
        public async Task<ActionResult> GetDoctorBySpecialtyAsync([FromQuery] string specialty)
        {
            var doctors = await _repository.GetDoctorBySpecialtyAsync(specialty);

            if (doctors.Count < 1) return NoContent();
            return Ok(doctors);
        }

        #endregion Methods
    }
}