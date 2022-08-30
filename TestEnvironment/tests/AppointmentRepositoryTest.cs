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
    [TestClass]
    public class AppointmentRepositoryTest
    {
        private ScheduleControlContext _context;
        private IAppointment _repository;

        [TestMethod]
        public async Task CreateTwoAppointmentsInDBReturnsTwoAppointments()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol10")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Maria Aparecida", "Ginecologista"), new CreatePatientDTO("Ana")));
        }

        [TestMethod]
        public async Task DeleteAppointmentReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol11")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            await _repository.DeleteAppointmentAsync(1);

            Assert.IsNull(await _repository.GetAppointmentByIdAsync(1));
        }

        [TestMethod]
        public async Task GetAllAppointmentsReturnsAllAppointments()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol12")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new AppointmentRepository(_context);

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Ricardo Nunes", "Ortopedista"), new CreatePatientDTO("Ítalo")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Maria Aparecida", "Ginecologista"), new CreatePatientDTO("Ana")));

            await _repository.CreateAppointmentAsync(
                new CreateAppointmentDTO(DateTime.Now, new CreateDoctorDTO("Luana Oliveira", "Dentista"), new CreatePatientDTO("Ronaldo")));

            var list = await _repository.GetAllAppointmentsAsync();

            Assert.AreEqual(3, list.Count);
        }

    }
}
