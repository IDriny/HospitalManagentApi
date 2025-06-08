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
        public async Task<ActionResult> SignUp([FromBody]SignUpModel newUser)
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
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code,error.Description);
                }

                return BadRequest();
            }

            string body = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        body {\r\n            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n            background-color: #f7f9fc;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .email-container {\r\n            max-width: 600px;\r\n            margin: auto;\r\n            background-color: #ffffff;\r\n            border-radius: 10px;\r\n            padding: 30px;\r\n            box-shadow: 0 5px 15px rgba(0,0,0,0.05);\r\n        }\r\n        .header {\r\n            text-align: center;\r\n            padding-bottom: 20px;\r\n        }\r\n        .header h1 {\r\n            color: #2f54eb;\r\n            margin-bottom: 5px;\r\n        }\r\n        .content {\r\n            font-size: 16px;\r\n            color: #333333;\r\n            line-height: 1.6;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            margin-top: 25px;\r\n            padding: 12px 24px;\r\n            background-color: #2f54eb;\r\n            color: #ffffff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-weight: bold;\r\n        }\r\n        .footer {\r\n            margin-top: 40px;\r\n            font-size: 13px;\r\n            color: #999999;\r\n            text-align: center;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <h1>Welcome to Samadoun NP Hospital!</h1>\r\n            <p>Your health, our priority.</p>\r\n        </div>\r\n        <div class=\"content\">\r\n            <p>Dear <strong>"+$"{addedPatient.FullName}"+"</strong>,</p>\r\n            <p>\r\n                We’re thrilled to welcome you to <strong>Samadoun NP Hospital</strong>. Thank you for registering with us!\r\n                Whether you're here to book appointments, check medical records, or consult our doctors,\r\n                we’re committed to making your experience smooth and secure.\r\n            </p>\r\n            <p>\r\n                To get started, simply log into your dashboard and explore the services available to you.\r\n            </p>\r\n            <p style=\"text-align: center;\">\r\n                <a href=\"https://hospital-project-ruddy.vercel.app/\" class=\"button\">Go to Website</a>\r\n            </p>\r\n            <p>\r\n                If you have any questions, don’t hesitate to contact our support team. We're always here to help.\r\n            </p>\r\n            <p>Warm regards,<br/>The HospitalX Team</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            &copy; 2025 HospitalX. All rights reserved.\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";
            await _emailSender.SendEmailAsync(newUser.Email, "Welcome to Samadoun NP Hospital",body);

            
            return Ok();
        }
        //api/User/SignUp
        [HttpPost]
        [Route("SignUpEmail/{email}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> sentEmail(string email)
        {
            string body ="<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <style>\r\n        body {\r\n            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\r\n            background-color: #f7f9fc;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .email-container {\r\n            max-width: 600px;\r\n            margin: auto;\r\n            background-color: #ffffff;\r\n            border-radius: 10px;\r\n            padding: 30px;\r\n            box-shadow: 0 5px 15px rgba(0,0,0,0.05);\r\n        }\r\n        .header {\r\n            text-align: center;\r\n            padding-bottom: 20px;\r\n        }\r\n        .header h1 {\r\n            color: #2f54eb;\r\n            margin-bottom: 5px;\r\n        }\r\n        .content {\r\n            font-size: 16px;\r\n            color: #333333;\r\n            line-height: 1.6;\r\n        }\r\n        .button {\r\n            display: inline-block;\r\n            margin-top: 25px;\r\n            padding: 12px 24px;\r\n            background-color: #2f54eb;\r\n            color: #ffffff;\r\n            text-decoration: none;\r\n            border-radius: 5px;\r\n            font-weight: bold;\r\n        }\r\n        .footer {\r\n            margin-top: 40px;\r\n            font-size: 13px;\r\n            color: #999999;\r\n            text-align: center;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"email-container\">\r\n        <div class=\"header\">\r\n            <h1>Welcome to Samadoun NP Hospital!</h1>\r\n            <p>Your health, our priority.</p>\r\n        </div>\r\n        <div class=\"content\">\r\n            <p>Dear <strong>"+$"{email}"+"</strong>,</p>\r\n            <p>\r\n                We’re thrilled to welcome you to <strong>Samadoun NP Hospital</strong>. Thank you for registering with us!\r\n                Whether you're here to book appointments, check medical records, or consult our doctors,\r\n                we’re committed to making your experience smooth and secure.\r\n            </p>\r\n            <p>\r\n                To get started, simply log into your dashboard and explore the services available to you.\r\n            </p>\r\n            <p style=\"text-align: center;\">\r\n                <a href=\"https://hospital-project-ruddy.vercel.app/\" class=\"button\">Go to Website</a>\r\n            </p>\r\n            <p>\r\n                If you have any questions, don’t hesitate to contact our support team. We're always here to help.\r\n            </p>\r\n            <p>Warm regards,<br/>The HospitalX Team</p>\r\n        </div>\r\n        <div class=\"footer\">\r\n            &copy; 2025 HospitalX. All rights reserved.\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>\r\n";
            await _emailSender.SendEmailAsync(email, "new user", body);
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
