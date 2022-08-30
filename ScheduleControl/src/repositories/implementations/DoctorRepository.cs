using Microsoft.EntityFrameworkCore;
using ScheduleControl.src.data;
using ScheduleControl.src.dtos;
using ScheduleControl.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ScheduleControl.src.repositories.implementations
{
    public class DoctorRepository : IDoctor
    {
        #region ATTRIBUTES

        private readonly ScheduleControlContext _context;

        #endregion

        #region CONSTRUCTORS

        public DoctorRepository(ScheduleControlContext context)
        {
            _context = context;
        }

        #endregion

        #region METHODS

        public async Task CreateDoctorAsync(CreateDoctorDTO doctordto)
        {
            await _context.Doctors.AddAsync(new DoctorModel
            {
                Name = doctordto.Name,
                Specialty = doctordto.Specialty
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorAsync(int id)
        {
            _context.Doctors.Remove(await GetDoctorByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<DoctorModel>> GetAllDoctorsAsync()
        {
            return await _context.Doctors
                .Include(x => x.MyAppointments)
                .ToListAsync();
        }

        public async Task<DoctorModel> GetDoctorByIdAsync(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DoctorModel> GetDoctorByNameAsync(string name)
        {
            return await _context.Doctors.FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<List<DoctorModel>> GetDoctorBySpecialtyAsync(string specialty)
        {
            return await _context.Doctors
                .Where(d => d.Specialty == specialty)
                .ToListAsync();
        }

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
