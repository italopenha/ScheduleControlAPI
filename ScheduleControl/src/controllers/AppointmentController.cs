using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using ScheduleControl.src.repositories;
using System.Threading.Tasks;

namespace ScheduleControl.src.controllers
{
    /// <summary>
    /// <para>Resume: Controllers for appointment class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [ApiController]
    [Route("api/Appointment")]
    [Produces("application/json")]
    public class AppointmentController : ControllerBase
    {
        #region ATTRIBUTES

        private readonly IAppointment _repository;

        #endregion

        #region CONSTRUCTORS

        public AppointmentController(IAppointment appointment)
        {
            _repository = appointment;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="appointmentDTO">CreateAppointmentDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example:
        ///
        ///     POST /api/Appointment
        ///     {
        ///         "time": "2022-08-20T19:45:35",
        ///         "doctor": {
        ///             "name": "Ricardo Nunes",
        ///             "specialty": "Ortopedista"
        ///             },
        ///         "patient": {
        ///             "name": "Ítalo"
        ///             }
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Returns a created appointment </response>
        /// <response code="400"> Request error </response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AppointmentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentDTO appointmentDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.CreateAppointmentAsync(appointmentDTO);
            return Created($"api/Appointment", appointmentDTO);
        }

        /// <summary>
        /// Delete a appointment by id
        /// </summary>
        /// <param name="idAppointment">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204"> Deleted appointment </response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idAppointment}")]
        public async Task<ActionResult> DeleteAppointmentAsync([FromRoute] int idAppointment)
        {
            await _repository.DeleteAppointmentAsync(idAppointment);
            return NoContent();
        }

        /// <summary>
        /// Get a appointment by id
        /// </summary>
        /// <param name="idAppointment">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200"> Returns a appointment </response>
        /// <response code="404"> Appointment does not exist </response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idAppointment}")]
        public async Task<ActionResult> GetAppointmentById([FromRoute] int idAppointment)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(idAppointment);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        /// <summary>
        /// Get all appointments
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lists all appointments</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult> GetAllAppointmentsAsync()
        {
            var list = await _repository.GetAllAppointmentsAsync();

            if (list.Count < 1) return NotFound();
            return Ok(list);
        }

        #endregion
    }
}
