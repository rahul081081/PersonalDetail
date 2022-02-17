using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace PersonalDetail.Api.Controllers
{
    [ApiController]
    public class PersonalDetailController : ControllerBase
    {
        private readonly ILogger<PersonalDetailController> _logger;

        public PersonalDetailController(ILogger<PersonalDetailController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/personaldetail")]
        public async Task<ActionResult<Web.Models.PersonalDetail>> Get()
        {
            try
            {
                string json = System.IO.File.ReadAllText(Web.Models.Constant.Filepath);
                var result = JsonConvert.DeserializeObject<Web.Models.PersonalDetail>(json);

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/personaldetail/save")]
        public async Task<ActionResult<bool>> Post([FromBody] Web.Models.PersonalDetail postData)
        {
            try
            {
                System.IO.File.WriteAllText(Web.Models.Constant.Filepath, JsonConvert.SerializeObject(postData));

                return true;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
