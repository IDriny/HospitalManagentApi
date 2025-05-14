using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        private readonly IPatientRepo _patientRepo;

        public UserController(IAuthManager authManager,IMapper mapper,IPatientRepo patientRepo)
        {
            _authManager = authManager;
            _mapper = mapper;
            _patientRepo = patientRepo;
        }

        //api/User/SignUp
        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SignUp([FromBody]SignUpModel newUser)
        {
            var user = _mapper.Map<ApiUser>(newUser);
            var patient = _mapper.Map<CreatePatientModel>(user);
            var addedPatient = _mapper.Map<Patient>(patient);
            addedPatient.FullName = patient.FirstName + " " + patient.LastName;
            if (!await _patientRepo.ExistByEmailAsync(addedPatient.Email))
            {
                await _patientRepo.AddAsync(addedPatient);
            }
            
            var errors = await _authManager.SignUp(newUser);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
                }

                return BadRequest();
            }
            return Ok();
        }

        //api/User/SignIn
        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SignIn([FromBody] LogInModel userModel)
        {
            var authResponse = await _authManager.SignIn(userModel);
            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }

    }
}
