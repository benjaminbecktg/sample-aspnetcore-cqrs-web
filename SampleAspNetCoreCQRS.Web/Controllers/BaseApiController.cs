using Microsoft.AspNetCore.Mvc;

namespace SampleAspNetCoreCQRS.Web.Controllers
{
    public class BaseApiController : ControllerBase
    {
        public BaseApiController(IContext context)
        {
            Context = context;
        }

        protected IContext Context { get; set; }

        [NonAction]
        protected async Task<IActionResult> ExecuteActionAsync<TResult>(Func<Task<TResult>> action)
        {
            TResult result = default(TResult);

            try
            {
                result = await action().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }
    }
}
