using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigXmlSignController : ControllerBase
    {
        private readonly IConfigXmlSignRepository repo;

        public ConfigXmlSignController()
        {
            repo = new ConfigXmlSignRepository();
        }


        [HttpGet]
        [Route("GetListAll")]
        public IActionResult GetListAll()
        {
            var result = repo.GET_LIST().Result;

            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetDetail(int id)
        {
            var result = repo.GET_DETAIL(id).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert(ConfigXmlSign param)
        {
            var result = repo.INSERT(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(ConfigXmlSign param)
        {
            var result = repo.UPDATE(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(ConfigXmlSign param)
        {
            var result = repo.DELETE(param).Result;

            return Ok(result);
        }



        [HttpPost]
        [Route("UpdateOneTime")]
        public IActionResult UpdateOneTime(ConfigXmlSign param)
        {
            var result = repo.UPDATE_ONETIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateAnyTime")]
        public IActionResult UpdateAnyTime(ConfigXmlSign param)
        {
            var result = repo.UPDATE_ANYTIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteOneTime")]
        public IActionResult DeleteOneTime(DeleteOnetime param)
        {
            var result = repo.DELETE_ONETIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteAnyTime")]
        public IActionResult DeleteAnyTime(DeleteAnytime param)
        {
            var result = repo.DELETE_ANYTIME(param).Result;

            return Ok(result);
        }

        [HttpPost]
        [Route("UpdateNextTime")]
        public IActionResult UpdateNextTime(ConfigNextTime param)
        {
            var result = repo.UPDATE_NEXTTIME(param).Result;

            return Ok(result);
        }

    }
}
