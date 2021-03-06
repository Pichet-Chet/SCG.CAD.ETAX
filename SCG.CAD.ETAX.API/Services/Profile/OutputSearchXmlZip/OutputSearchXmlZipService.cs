namespace SCG.CAD.ETAX.API.Services
{
    public class OutputSearchXmlZipService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.outputSearchXmlZip.ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + getList.Count + "' records. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.outputSearchXmlZip.Where(x => x.OutputSearchXmlZipNo == id).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + id + "' success. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response INSERT(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.outputSearchXmlZip.Add(param);
                    _dbContext.SaveChanges();

                    int identityNo = param.OutputSearchXmlZipNo;

                    resp.STATUS = true;
                    resp.MESSAGE = "Insert success.";
                    resp.OUTPUT_DATA = identityNo;
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response UPDATE(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.outputSearchXmlZip.Where(x => x.OutputSearchXmlZipNo == param.OutputSearchXmlZipNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.OutputSearchXmlZipCompanyCode = param.OutputSearchXmlZipCompanyCode;
                        update.OutputSearchXmlZipFileName = param.OutputSearchXmlZipFileName;
                        update.OutputSearchXmlZipFullPath = param.OutputSearchXmlZipFullPath;
                        update.OutputSearchXmlZipDowloadStatus = param.OutputSearchXmlZipDowloadStatus;
                        update.OutputSearchXmlZipDowloadCount = param.OutputSearchXmlZipDowloadCount;
                        update.OutputSearchXmlZipDowloadLastTime = param.OutputSearchXmlZipDowloadLastTime;
                        update.OutputSearchXmlZipDowloadLastBy = param.OutputSearchXmlZipDowloadLastBy;

                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;

                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Updated Success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't update because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response DELETE(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.outputSearchXmlZip.Find(param.OutputSearchXmlZipNo);

                    if (delete != null)
                    {
                        _dbContext.outputSearchXmlZip.Remove(delete);
                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Delete success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't delete because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Delete faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response SEARCH(string JsonString)
        {
            Response resp = new Response();

            outputSearchXmlModel obj = new outputSearchXmlModel();

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            try
            {
                obj = JsonConvert.DeserializeObject<outputSearchXmlModel>(JsonString);

                DateTime getMinDate = new DateTime();
                DateTime getMaxDate = new DateTime();

                var getDocType = obj.outPutSearchDocType.ToUpper();

                var getStatus = obj.outPutSearchStatus;

                int statusDownload = 99;

                getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchStatus;

                if (!string.IsNullOrEmpty(getStatus))
                {
                    statusDownload = Convert.ToInt32(getStatus);
                }
                else
                {
                    statusDownload = 99;
                }

                var getArrayDate = obj.outPutSearchDate.Split("to");

                if (!string.IsNullOrEmpty(obj.outPutSearchDate))
                {
                    getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                    getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());
                }
                else
                {
                    getMinDate = DateTime.Now.AddDays(-30);
                    getMaxDate = DateTime.Now.AddDays(30);
                }

                if (obj != null)
                {

                    tran = _dbContext.outputSearchXmlZip.Where(

                            x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date &&

                            obj.outPutSearchCompanyCode.Count > 0 ? ( obj.outPutSearchCompanyCode.Contains(x.OutputSearchXmlZipCompanyCode) && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipCompanyCode != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) &&

                            statusDownload == 99 ? ( x.OutputSearchXmlZipDowloadStatus != 1 && x.OutputSearchXmlZipDowloadStatus != 0  && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipDowloadStatus == statusDownload && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) &&

                            getDocType != "ALL" ? (obj.outPutSearchDocType.ToUpper() == x.OutputSearchXmlZipDocType.ToUpper() && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipDocType != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date)

                            ).ToList();

                    if (tran.Count > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data success. ";
                        resp.OUTPUT_DATA = tran;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Data not found";
                    }
                }
                else
                {

                    var getList = _dbContext.outputSearchXmlZip.ToList();

                    if (getList.Count > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data success. ";
                        resp.OUTPUT_DATA = getList;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Data not found";
                    }
                }


            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }



    }
}
