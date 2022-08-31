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
    /// <para> Tests to verify the functioning of the methods of the doctor class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [TestClass]
    public class DoctorRepositoryTest
    {
        private ScheduleControlContext _context;
        private IDoctor _repository;

        // Doctor addition Test
        [TestMethod]
        public async Task CreateTwoDoctorsInDBReturnTwoDoctorsAsync()
        {
            // Context
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol1")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            // Since I register 2 doctors
            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Larissa Oliveira", "Cardiologista"));

            // When I request a list, I have 2 doctors
            Assert.AreEqual(2, _context.Doctors.Count());
        }

        // Update doctor test
        [TestMethod]
        public async Task UpdateDoctorReturnsUpdatedDoctor()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol2")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            // Since I register a doctor
            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            // When I update the doctor's specialty
            await _repository.UpdateDoctorAsync(
                new UpdateDoctorDTO(_context.Doctors.FirstOrDefault(d => d.Name == "Ricardo Nunes").Id, "Pediatra"));

            var oldDoctor = await _repository.GetDoctorByNameAsync("Ricardo Nunes");

            // I have a updated doctor
            Assert.AreEqual("Pediatra", _context.Doctors.FirstOrDefault(x => x.Id == oldDoctor.Id).Specialty);
        }

        // Delete doctor test
        [TestMethod]
        public async Task DeleteDoctorReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol3")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            // Since I register a doctor
            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            // I delete the doctor by id
            await _repository.DeleteDoctorAsync(1);

            // The doctor was deleted
            Assert.IsNull(await _repository.GetDoctorByIdAsync(1));
        }

        // Get all doctors test
        [TestMethod]
        public async Task GetAllDoctorsReturnsAllDoctors()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol4")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            // Since I register 3 doctors
            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Lara Croft", "Pediatra"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Arthur Morgan", "Urologista"));

            // When I request all doctors
            var list = await _repository.GetAllDoctorsAsync();

            // I get all doctors
            Assert.AreEqual(3, list.Count);
        }

        // Get doctors by specialty test
        [TestMethod]
        public async Task GetDoctorsBySpecialtyReturnsDoctors()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol5")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new DoctorRepository(_context);

            // Since I register 2 doctors with a specialty "Ortopedista" and 1 with "Urologista"
            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Ricardo Nunes", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Lara Croft", "Ortopedista"));

            await _repository.CreateDoctorAsync(
                new CreateDoctorDTO(
                    "Arthur Morgan", "Urologista"));

            // When I request doctors with a specialty "Ortopedista"
            var list = await _repository.GetDoctorBySpecialtyAsync("Ortopedista");

            // Then I have 2 doctors
            Assert.AreEqual(2, list.Count);
        }
    }
}
