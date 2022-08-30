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
    public class DoctorRepositoryTest
    {
        private ScheduleControlContext _context;
        private IDoctor _repository;

        [TestMethod]
        public async Task CreateTwoDoctorsInDBReturnTwoDoctorsAsync()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol1")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Larissa Oliveira", "Cardiologista"));

            Assert.AreEqual(2, _context.Doctors.Count());
        }

        [TestMethod]
        public async Task UpdateDoctorReturnsUpdatedDoctor()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol2")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.UpdateDoctorAsync(
                new UpdateDoctorDTO(_context.Doctors.FirstOrDefault(d => d.Name == "Ricardo Nunes").Id, "Pediatra"));

            var oldDoctor = await _repository.GetDoctorByNameAsync("Ricardo Nunes");

            Assert.AreEqual("Pediatra", _context.Doctors.FirstOrDefault(x => x.Id == oldDoctor.Id).Specialty);
        }

        [TestMethod]
        public async Task DeleteDoctorReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol3")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.DeleteDoctorAsync(1);

            Assert.IsNull(await _repository.GetDoctorByIdAsync(1));
        }

        [TestMethod]
        public async Task GetAllDoctorsReturnsAllDoctors()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol4")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Lara Croft", "Pediatra"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Arthur Morgan", "Urologista"));

            var list = await _repository.GetAllDoctorsAsync();

            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public async Task GetDoctorsBySpecialtyReturnsDoctors()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol5")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Lara Croft", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Arthur Morgan", "Urologista"));

            var list = await _repository.GetDoctorBySpecialtyAsync("Ortopedista");

            Assert.AreEqual(2, list.Count);
        }
    }
}
