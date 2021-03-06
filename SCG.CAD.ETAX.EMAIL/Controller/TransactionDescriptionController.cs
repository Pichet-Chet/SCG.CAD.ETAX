using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using System.Text;

namespace SCG.CAD.ETAX.EMAIL.Controller
{
    public class TransactionDescriptionController
    {
        public async Task<Response> UpdateList(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/UpdateList", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }

        public async Task<List<TransactionDescription>> List()
        {
            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
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
