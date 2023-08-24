using Microsoft.AspNetCore.Mvc;
using NextGen.Model.Exceptions;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Web;

namespace NextGen.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;

        protected BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        protected IActionResult GetItemResult(object model)
        {
            return Ok(model);
        }

        protected IActionResult CreatedItemResult<T>(T createdItemId)
        {
            var stringAndClear = $"Id: {createdItemId}";
            return Created(string.Empty, stringAndClear);
        }

        protected IActionResult UpdatedItemResult()
        {
            return NoContent();
        }

        protected IActionResult DeletedItemResult()
        {
            return NoContent();
        }

        protected IActionResult ErrorResult(Exception ex)
        {
            int statusCode = 0;
            List<string> stringList = new List<string>();

            if (ex is ArgumentException argumentException)
            {
                statusCode = 400;
                stringList.Add(argumentException.Message);
            }

            if (statusCode == 500)
            {
                _logger.LogError(ex, "Unhandled exception");
            }
            else
            {
                var str = string.Join("; ", stringList);
                var logMessage = $"{ex.GetType()}: {str}";
                _logger.LogWarning(logMessage);
            }

            var errorViewModel = new ErrorViewModel(stringList);
            return StatusCode(statusCode, errorViewModel);
        }

        protected string GetFullMessage(Exception ex)
        {
            return ex.InnerException == null ? ex.Message : $"{ex.Message} --> {ex.InnerException.Message}";
        }

        public static T ParseRequestQuery<T>(string queryString)
        {
            NameValueCollection dict = HttpUtility.ParseQueryString(queryString);
            T obj;

            try
            {
                obj = JsonSerializer.Deserialize<T>(
                    JsonSerializer.Serialize<Dictionary<string, string>>(
                        dict.Cast<string>().ToDictionary(k => k, v => dict[v])
                    )
                );
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to parse request query string.", ex);
            }

            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
            {
                var errorMessages = validationResults.Select(result => result.ErrorMessage ?? "No particular Exception thrown").ToList();
                throw new InvalidModelException(typeof(T), errorMessages);
            }

            return obj;
        }

        public static async Task<T> ParseRequestBodyAsync<T>(Stream requestBody)
        {
            string value;
            T obj;

            using (StreamReader reader = new StreamReader(requestBody))
            {
                value = await reader.ReadToEndAsync();

                try
                {
                    obj = JsonSerializer.Deserialize<T>(value);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException("Unable to parse request body.", ex);
                }

                ValidationContext validationContext = new ValidationContext(obj);
                List<ValidationResult> validationResults = new List<ValidationResult>();

                if (!Validator.TryValidateObject(obj, validationContext, validationResults, true))
                {
                    var errorMessages = validationResults.Select(result => result.ErrorMessage ?? "No particular Exception thrown").ToList();
                    throw new InvalidModelException(typeof(T), errorMessages);
                }

                return obj;
            }
        }
    }
}
