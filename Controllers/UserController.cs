using AutoMapper;
using HospitalManagentApi.Core.Contracts;
using HospitalManagentApi.Core.Domain;
using HospitalManagentApi.Models.Patient;
using HospitalManagentApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Html;


namespace HospitalManagentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly IMapper _mapper;
        private readonly IPatientRepo _patientRepo;
        private readonly IEmailSender _emailSender;

        public UserController(IAuthManager authManager,IMapper mapper,IPatientRepo patientRepo,IEmailSender emailSender)
        {
            _authManager = authManager;
            _mapper = mapper;
            _patientRepo = patientRepo;
            this._emailSender = emailSender;
        }

        //api/User/SignUp
        [HttpPost]
        [Route("SignUp")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> SignUp([FromBody]SignUpModel newUser)
        {
            var user = _mapper.Map<ApiUser>(newUser);
            var patient = _mapper.Map<CreatePatientModel>(user);
            var addedPatient = _mapper.Map<Patient>(patient);
            addedPatient.FullName = patient.FirstName + " " + patient.LastName;
            addedPatient.PhoneNumber = patient.PhoneNumber;
            if (!await _patientRepo.ExistByEmailAsync(addedPatient.Email))
            {
                await _patientRepo.AddAsync(addedPatient);
            }
            var errors = await _authManager.SignUp(newUser);
            if (errors==null)
            {
                return BadRequest();
            }

            await _emailSender.SendEmailAsync(newUser.Email, "Welcome to Samadoun NP Hospital",addedPatient.FullName);
            return Ok(errors);
        }
        //api/User/SignUp
        [HttpPost]
        [Route("SignUpEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> sentEmail(string email)
        {
            await _emailSender.SendEmailAsync(email, "new user",email);
            return Ok();
        }


        //api/User/SignIn
        [HttpPost]
        [Route("SignIn")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> SignIn(LogInModel userModel)
        {
            var authResponse = await _authManager.SignIn(userModel);
            if (authResponse == null)
            {
                return Unauthorized();
            }
            return Ok(authResponse);
        }

        //api/user/Post/GetUser
        [HttpPost]
        [Route("GetUser")]
        public async Task<ActionResult<GetUserModel>> GetUser(AuthResponseModel request)
        {
            var userInfo = await _authManager.GetUserInfoFromToken(request);
            if (userInfo == null)
            {
                return Unauthorized();
            }

            return Ok(userInfo);
        }


    }
}
