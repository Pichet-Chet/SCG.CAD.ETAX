using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR
{
    public class TaxCodeController
    {
        public async Task<List<TaxCode>> List()
        {
            Response resp = new Response();

            List<TaxCode> tran = new List<TaxCode>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());
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
