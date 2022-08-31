using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleControl.src.models;
using ScheduleControl.src.repositories;
using System.Threading.Tasks;
using System;
using ScheduleControl.src.dtos;

namespace ScheduleControl.src.controllers
{
    /// <summary>
    /// <para>Resume: Controllers for patient class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [ApiController]
    [Route("api/Patient")]
    [Produces("application/json")]
    public class PatientController : ControllerBase
    {
        #region ATTRIBUTES

        private readonly IPatient _repository;

        #endregion

        #region CONSTRUCTORS

        public PatientController(IPatient patient)
        {
            _repository = patient;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create a new patient
        /// </summary>
        /// <param name="patientDTO">CreatePatientDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Patient
        ///     {
        ///        "name": "Ítalo"
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Returns created patient </response>
        /// <response code="400"> Request error </response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PatientModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult> CreatePatientAsync([FromBody] CreatePatientDTO patientDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _repository.CreatePatientAsync(patientDTO);
                return Created($"api/Patient", patientDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Delete a patient by id
        /// </summary>
        /// <param name="idPatient">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204"> Patient deleted </response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPatient}")]
        public async Task<ActionResult> DeletePatientAsync([FromRoute] int idPatient)
        {
            await _repository.DeletePatientAsync(idPatient);
            return NoContent();
        }

        /// <summary>
        /// Get a patient by name
        /// </summary>
        /// <param name="namePatient">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Returns a patient </response>
        /// <response code="204"> Patient does not exist </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("name/{namePatient}")]
        public async Task<ActionResult> GetPatientByNameAsync([FromQuery] string namePatient)
        {
            var patient = await _repository.GetPatientByNameAsync(namePatient);

            if (patient == null) return NotFound();

            return Ok(patient);
        }

        /// <summary>
        /// Get a patient by id
        /// </summary>
        /// <param name="idPatient">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Returns a patient </response>
        /// <response code="404"> Patient does not exist </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPatient}")]
        public async Task<ActionResult> GetPatientById([FromRoute] int idPatient)
        {
            var patient = await _repository.GetPatientByIdAsync(idPatient);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lists all patients</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PatientModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> GetAllPatientsAsync()
        {
            var list = await _repository.GetAllPatientsAsync();

            if (list.Count < 1) return NotFound();
            return Ok(list);
        }

        #endregion
    }
}
