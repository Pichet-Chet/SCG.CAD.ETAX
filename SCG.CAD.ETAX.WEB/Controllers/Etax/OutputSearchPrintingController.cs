using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class OutputSearchPrintingController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Content()
        {
            return View();
        }

        public IActionResult _Search()
        {
            return View();
        }

        public IActionResult _Modal()
        {
            return View();
        }


        public async Task<JsonResult> Detail(int id)
        {
            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

                result = JsonConvert.SerializeObject(tran[0]);

            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(result);
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return Json(new { data = tran });
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "OutputSearchPrintingNo," +
                            "OutputSearchPrintingCompanyCode," +
                            "OutputSearchPrintingFileName," +
                            "OutputSearchPrintingFullPath," +
                            "OutputSearchPrintingDowloadStatus," +
                            "OutputSearchPrintingDowloadCount," +
                            "OutputSearchPrintingDowloadLastTime," +
                            "OutputSearchPrintingDowloadLastBy," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.OutputSearchPrintingNo}," +
                                $"{item.OutputSearchPrintingCompanyCode}," +
                                $"{item.OutputSearchPrintingFileName}," +
                                $"{item.OutputSearchPrintingFullPath}," +
                                $"{item.OutputSearchPrintingDowloadStatus}," +
                                $"{item.OutputSearchPrintingDowloadCount}," +
                                $"{item.OutputSearchPrintingDowloadLastTime}," +
                                $"{item.OutputSearchPrintingDowloadLastBy}," +
                                $"{item.CreateBy}," +
                                $"{item.CreateDate}," +
                                $"{item.UpdateBy}," +
                                $"{item.UpdateDate}," +
                                $"{item.Isactive}");
                        }

                        resp.STATUS = true;
                    }
                    else
                    {
                        resp.STATUS = false;
                    }
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-OutputSearchPrinting.csv");

        }

        public async Task<JsonResult> Search(string jsonSearchString)
        {

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/Search?JsonString= " + jsonSearchString + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(new { data = tran });

        }


    }
}
