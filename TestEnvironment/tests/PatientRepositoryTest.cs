using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
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
    /// <para> Tests to verify the functioning of the methods of the patient class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    [TestClass]
    public class PatientRepositoryTest
    {
        private ScheduleControlContext _context;
        private IPatient _repository;

        // Patient addition Test
        [TestMethod]
        public async Task CreateTwoPatientsInDBReturnsTwoPatients()
        {
            // Context
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol6")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            // Since I register 2 patients
            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ana"));

            // When I request a list, I have 2 patients
            Assert.AreEqual(2, _context.Patients.Count());
        }

        // Test to delete a patient
        [TestMethod]
        public async Task DeletePatientReturnsNull()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol7")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            // Since I register 1 patient
            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            await _repository.DeletePatientAsync(1);

            // Patient is deleted
            Assert.IsNull(await _repository.GetPatientByIdAsync(1));
        }

        // Test to get a patient by name
        [TestMethod]
        public async Task GetPatientByNameReturnsPatient()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol8")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            // Since I register 1 patient
            await _repository.CreatePatientAsync(
                new CreatePatientDTO(
                    "Ítalo"));

            // Since I search by name
            await _repository.GetPatientByNameAsync("Ítalo");

            // I get a patient with that name
            Assert.AreEqual(_context.Patients.Count(), 1);
        }

        // Test to get all patients
        [TestMethod]
        public async Task GetAllPatientsReturnsAllPatients()
        {
            var opt = new DbContextOptionsBuilder<ScheduleControlContext>()
               .UseInMemoryDatabase(databaseName: "db_schedulecontrol9")
               .Options;
            _context = new ScheduleControlContext(opt);
            _repository = new PatientRepository(_context);

            // Since I register 4 patients
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

            // When I request all patients, I get all patients
            Assert.AreEqual(4, list.Count);
        }
    }
}
