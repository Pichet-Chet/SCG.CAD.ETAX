namespace SCG.CAD.ETAX.API.Repositories
{
    public class ConfigMftsIndexGenerationSettingOutputRepository : IConfigMftsIndexGenerationSettingOutputRepository
    {
        ConfigMftsIndexGenerationSettingOutputService service = new ConfigMftsIndexGenerationSettingOutputService();

        public async Task<Response> GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL(id);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> GET_LIST()
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_LIST();

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> INSERT(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();

            try
            {
                var result = service.INSERT(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> UPDATE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> DELETE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();

            try
            {
                var result = service.DELETE(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> UPDATE_ONETIME(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE_ONETIME(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> UPDATE_ANYTIME(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE_ANYTIME(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> DELETE_ONETIME(DeleteOnetime param)
        {
            Response resp = new Response();

            try
            {
                var result = service.DELETE_ONETIME(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> DELETE_ANYTIME(DeleteAnytime param)
        {
            Response resp = new Response();

            try
            {
                var result = service.DELETE_ANYTIME(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }

        public async Task<Response> UPDATE_NEXTTIME(ConfigNextTime param)


        {
            Response resp = new Response();

            try
            {
                var result = service.UPDATE_NEXTTIME(param);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }


    }
}
