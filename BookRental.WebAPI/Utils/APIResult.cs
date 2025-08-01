using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Utils
{
    public static class APIResult
    {
        public static ActionResult Ok(object data)
        {
            return new OkObjectResult(data);
        }

        public static ActionResult Created(object data, string message)
        {
            return new ObjectResult(new { message, data })
            {
                StatusCode = 201
            };
        }

        public static ActionResult NoContent() => new NoContentResult();

        public static ActionResult BadRequest(string message)
        {
            return new BadRequestObjectResult(message);
        }

        public static ActionResult NotFound(string message)
        {
            return new NotFoundObjectResult(message);
        }

        public static ActionResult Error(string message)
        {
            return new ObjectResult(message)
            {
                StatusCode = 500
            };
        }
    }
}
