namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileBranchRepository : IProfileBranchRepository
    {
        ProfileBranchService service = new ProfileBranchService();


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

        public async Task<Response> INSERT(ProfileBranch param)
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

        public async Task<Response> UPDATE(ProfileBranch param)
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

        public async Task<Response> DELETE(ProfileBranch param)
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

        public async Task<Response> INSERTS(List<ProfileBranch> param)
        {
            Response resp = new Response();

            try
            {
                var result = service.INSERTS(param);

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
