using BigBangAss2.ErrorMessage;
using BigBangAss2.Exceptions;
using BigBangAss2.Interfaces;
using BigBangAss2.Models;
using BigBangAss2.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BigBangAss2.Controllers
{
    [Route("api/[controller]/action")]
    [ApiController]
    [EnableCors("ReactCors")]
    public class UserController : ControllerBase
    {
        private readonly IManageService _service;
        Error error;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _UService;

        public UserController(IManageService service,ILogger<UserController> logger,IUserService UService)
        {
            _service = service;
            _logger = logger;
            _UService = UService;
            error = new Error();
        }
        [HttpPost("Doctor Register")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<Doctor?>> DoctorRegisteration(DoctorRegDTO dto)
        {
            try
            {
                var doctor = await _service.RegisterDoctor(dto);
                if (doctor != null)
                    return Created("Doctor Added", doctor);
                error.ID = 400;
                error.Message = new Messages().messages[0];
            }
            catch (RepeatationException)
            {
                error.ID = 403;
                error.Message = new Messages().messages[1];
                return BadRequest(error);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }
        [HttpPost("Patient Register")]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<Patient?>> PatientRegisteration(PatientRegDTO dto)
        {
            try
            {
                var patient = await _service.RegisterPatient(dto);
                if (patient != null)
                    return Created("Patient Added", patient);
                error.ID = 400;
                error.Message = new Messages().messages[0];
            }
            catch (RepeatationException)
            {
                error.ID = 403;
                error.Message = new Messages().messages[1];
                return BadRequest(error);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<UserDTO?>> Login(UserDTO dto)
        {
            try
            {
                var user = await _UService.Login(dto);
                if (user != null)
                    return Created("Login Successful", user);
                error.ID = 400;
                error.Message = new Messages().messages[4];
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("Doctors")]
        [ProducesResponseType(typeof(ICollection<Doctor?>), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<ICollection<Doctor?>>> GetDoctors(StatusDTO dto)
        {
            try
            {
                var doctors = await _service.GetDoctors(dto.state);
                if (doctors != null)
                    return Created("Successful", doctors);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[4];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("AllDoctors")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ICollection<Doctor?>), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<ICollection<Doctor?>>> GetAllDoctors()
        {
            try
            {
                var doctors = await _service.GetAllDoctors();
                if (doctors != null)
                    return Created("Fetched", doctors);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[4];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("AllPatients")]
        [Authorize(Roles ="Admin")]
        [ProducesResponseType(typeof(ICollection<Patient?>), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<ICollection<Patient?>>> GetAllPatients()
        {
            try
            {
                var patients = await _service.GetAllPatients();
                if (patients != null)
                    return Created("Fetched", patients);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[4];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("Approve Doctor")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<Doctor?>> ApproveDoctor(UserIdDTO dto)
        {
            try
            {
                var doctor = await _service.ApproveDoctor(dto);
                if (doctor != null)
                    return Created("Successful", doctor);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }

        [HttpPost("Deny Doctor")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<Doctor?>> DenyDoctor(UserIdDTO dto)
        {
            try
            {
                var doctor = await _service.DenyDoctor(dto);
                if (doctor != null)
                    return Created("Successful", doctor);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }
        [HttpPost("Get Doctor")]
        [Authorize(Roles = "Doctor")]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]//Success Response
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//Failure Response
        public async Task<ActionResult<Doctor?>> GetDoctor(UserIdDTO dto)
        {
            try
            {
                var doctor = await _service.GetDoctor(dto);
                if (doctor != null)
                    return Created("Successful", doctor);
            }
            catch (Exception)
            {
                error.ID = 400;
                error.Message = new Messages().messages[2];
                _logger.LogError(error.Message);
            }
            return BadRequest(error);
        }
    }
}
