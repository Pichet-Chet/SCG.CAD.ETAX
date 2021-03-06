using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using Microsoft.Extensions.Configuration;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityConfigGlobalController
    {
        public async Task<List<ConfigGlobal>> List()
        {
            Response resp = new Response();

            List<ConfigGlobal> tran = new List<ConfigGlobal>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigGlobal>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return tran;
        }
    }
}
