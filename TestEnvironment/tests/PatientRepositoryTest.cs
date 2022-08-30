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
    public class PatientRepositoryTest
    {
        private ScheduleControlContext _context;
        private IPatient _repository;

        [TestMethod]
        public async Task CreateTwoPatientsInDBReturnsTwoPatients()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol6")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ana"));

            Assert.AreEqual(2, _context.Patients.Count());
        }

        [TestMethod]
        public async Task DeletePatientReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol7")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.DeletePatientAsync(1);

            Assert.IsNull(await _repository.GetPatientByIdAsync(1));
        }

        [TestMethod]
        public async Task GetPatientByNameReturnsPatient()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol8")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.GetPatientByNameAsync("Ítalo");

            Assert.AreEqual(_context.Patients.Count(), 1);
        }

        [TestMethod]
        public async Task GetAllPatientsReturnsAllPatients()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol9")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Lara"));

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ana"));

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Bruno"));

            var list = await _repository.GetAllPatientsAsync();

            Assert.AreEqual(4, list.Count);
        }
    }
}
