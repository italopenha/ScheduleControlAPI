using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories.implementations
{
    /// <summary>
    /// <para>Resume: Implementing methods and constructors for the doctor class </para>
    /// <para>Created by: Ítalo Penha </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 30/08/2022</para>
    /// </summary>
    public class DoctorRepository : IDoctor
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// <para>Resume: Constructor of the class </para>
        /// </summary>
        /// <param name="context">ScheduleControlContext</param>
        public DoctorRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// <para>Resume: Asynchronous method to create a doctor </para>
        /// </summary>
        public async Task CreateDoctorAsync(CreateDoctorDTO doctordto)
        {
            var doctor = await GetDoctorByNameAsync(doctordto.Name);
            if (doctor != null) throw new Exception("Médico já existe!");

            await _context.Doctors.AddAsync(new DoctorModel
            {
                Name = doctordto.Name,
                Specialty = doctordto.Specialty
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to delete a doctor </para>
        /// </summary>
        public async Task DeleteDoctorAsync(int id)
        {
            _context.Doctors.Remove(await GetDoctorByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get all doctors </para>
        /// </summary>
        public async Task<List<DoctorModel>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(x => x.MyAppointments)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a doctor by id </para>
        /// </summary>
        public async Task<DoctorModel> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a doctor by name </para>
        /// </summary>
        public async Task<DoctorModel> GetDoctorByNameAsync(string name)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Name == name);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get doctors by specialty </para>
        /// </summary>
        public async Task<List<DoctorModel>> GetDoctorBySpecialtyAsync(string specialty)
        {
            return await _context.Doctors
                .Where(d => d.Specialty == specialty)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to update a doctor </para>
        /// </summary>
        public async Task UpdateDoctorAsync(UpdateDoctorDTO doctordto)
        {
            var oldDoctor = await GetDoctorByIdAsync(doctordto.Id);
            oldDoctor.Specialty = doctordto.Specialty;
            _context.Doctors.Update(oldDoctor);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
