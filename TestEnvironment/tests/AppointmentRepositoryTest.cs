using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.repositories;
using ScheduleControl.src.repositories.implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnvironment.tests
{
    /// <summary>
    /// <para> Tests to verify the functioning of the methods of the appointment class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [TestClass]
    public class AppointmentRepositoryTest
    {
        private ScheduleControlContext _context;
        private IAppointment _repository;

        // Appointment addition Test
        [TestMethod]
        public async Task CreateTwoAppointmentsInDBReturnsTwoAppointments()
        {
            // Context
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol10")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            // Since I register 2 appointments
            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Maria Aparecida", "Ginecologista"), new CreatePatientDTO("Ana")));

            // When I request a list, I have 2 appointments
            Assert.AreEqual(2, _context.Appointments.Count());
        }

        // Delete appointment test
        [TestMethod]
        public async Task DeleteAppointmentReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol11")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            // Since I register 1 appointment
            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            // I delete the appointment by id
            await _repository.DeleteAppointmentAsync(1);

            // The appointment was deleted
            Assert.IsNull(await _repository.GetAppointmentByIdAsync(1));
        }

        // Get all appointments test
        [TestMethod]
        public async Task GetAllAppointmentsReturnsAllAppointments()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol12")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            // Since I register 3 appointments
            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Maria Aparecida", "Ginecologista"), new CreatePatientDTO("Ana")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Luana Oliveira", "Dentista"), new CreatePatientDTO("Ronaldo")));

            // When I request all appointments
            var list = await _repository.GetAllAppointmentsAsync();

            // I get all appointments
            Assert.AreEqual(3, list.Count);
        }

    }
}
