using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.ClinicDoctor;
using HospitalManagentApi.Persistence;
using HospitalManagentApi.Core.Contracts;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicDoctorsController : ControllerBase
    {
        private readonly Core.Contracts.IClinicDoctorRepo _clinicDoctorRepo;
        private readonly IMapper _mapper;

        public ClinicDoctorsController(IClinicDoctorRepo clinicDoctorRepo,IMapper mapper)
        {
            _clinicDoctorRepo = clinicDoctorRepo;
            _mapper = mapper;
        }

        // GET: api/ClinicDoctors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetClinicDoctorModel>>> GetClinicDoctors()
        {
            var Clinics = await _clinicDoctorRepo.GetAllAsync();
                /*_clinicDoctorRepo.ClinicDoctors.ToListAsync();*/
            var records = _mapper.Map<List<GetClinicDoctorModel>>(Clinics);
            return Ok(records);
        }

        // GET: api/ClinicDoctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDoctorModel>> GetClinicDoctor(int id)
        {
            var clinicDoctor = await _clinicDoctorRepo.GetDetailsAsync(id);
                //_clinicDoctorRepo.ClinicDoctors.FindAsync(id);

            if (clinicDoctor == null)
            {
                return NotFound();
            }

            var record = _mapper.Map<ClinicDoctorModel>(clinicDoctor);
            record.DrName = clinicDoctor.Doctor.FullName;
            record.ClinicName = clinicDoctor.Clinic.Name;
            return Ok(record);
        }

        // PUT: api/ClinicDoctors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicDoctor(int id, UpdateClinicDoctorModel updateClinicDoctor)
        {
            if (id != updateClinicDoctor.Id)
            {
                return BadRequest();
            }

            var clinicDoctor = await _clinicDoctorRepo.GetAsync(id);
                //ClinicDoctors.FindAsync(id);

            if (!await ClinicDoctorExists(id))
                return NotFound();

            _mapper.Map(updateClinicDoctor, clinicDoctor);

            //_clinicDoctorRepo.Entry(updateClinicDoctor).State = EntityState.Modified;

            try
            {
                await _clinicDoctorRepo.UpdateAsync(clinicDoctor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClinicDoctorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ClinicDoctors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClinicDoctor>> PostClinicDoctor(CreateClinicDoctorModel CreatedClinicDoctor)
        {
            var clinicDoctor = _mapper.Map<ClinicDoctor>(CreatedClinicDoctor);
            //_clinicDoctorRepo.ClinicDoctors.Add(clinicDoctor);
            //await _clinicDoctorRepo.SaveChangesAsync();

            await _clinicDoctorRepo.AddAsync(clinicDoctor);

            return CreatedAtAction("GetClinicDoctor", new { id = clinicDoctor.Id }, clinicDoctor);
        }

        // DELETE: api/ClinicDoctors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicDoctor(int id)
        {
            var clinicDoctor = await _clinicDoctorRepo.GetAsync(id);
                //_clinicDoctorRepo.ClinicDoctors.FindAsync(id);
            if (clinicDoctor == null)
            {
                return NotFound();
            }

            //_clinicDoctorRepo.ClinicDoctors.Remove(clinicDoctor);
            //await _clinicDoctorRepo.SaveChangesAsync();
            await _clinicDoctorRepo.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> ClinicDoctorExists(int id)
        {
            return await _clinicDoctorRepo.Exists(id);
            //_clinicDoctorRepo.ClinicDoctors.Any(e => e.Id == id);
        }
    }
}
