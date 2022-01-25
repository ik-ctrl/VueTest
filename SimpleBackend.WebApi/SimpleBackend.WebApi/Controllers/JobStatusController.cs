using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBackend.WebApi.DTO;
using SimpleBackend.WebApi.Helpers;
using SimpleBackend.WebApi.Models.Enums;
using SimpleBackend.WebApi.Models.Jobs;

namespace SimpleBackend.WebApi.Controllers
{
    /// <summary>
    /// Контроллер для проверки статуса задания
    /// </summary>
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/[action]")]
    public class JobStatusController:ControllerBase
    {
        private readonly JobDispatcherService _dispatcher;
        private readonly ResponseGenerator _responseGenerator;
        private readonly ILogger _logger;

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="dispatcher">Дистпетчер задач</param>
        /// <param name="responseGenerator">Генератор ответов</param>
        /// <param name="logger">Журнал логирования</param>
        /// <exception cref="ArgumentNullException">dispatcher=null</exception>
        public JobStatusController(JobDispatcherService dispatcher,ResponseGenerator responseGenerator,ILogger logger=null)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher), "Отсутвует диспетчер задач");
            _responseGenerator = responseGenerator ?? throw new ArgumentNullException(nameof(responseGenerator), "Отсутвует генератор ответов");
            _logger = logger;
        }

        /// <summary>
        /// Запрос записанных задач
        /// </summary>
        /// <returns> Список записанных задач</returns>
        [HttpPost]
        [ProducesResponseType(typeof(JobStatusDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JobStatusDTO), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Check(JobIdDTO jobId)
        {
            try
            {
                var result = await _dispatcher.CheckStatusJobAsync(jobId.Jobid);
                return result switch
                {
                    JobStatusType.Accepted => Ok(_responseGenerator.GenerateAcceptedJobStatusResponse("TestLocation", jobId.Jobid)),
                    JobStatusType.Finished => Ok(_responseGenerator.GenerateFinishedJobStatusResponse("TestLocation", jobId.Jobid)),
                    JobStatusType.NotFound => Ok(_responseGenerator.GenerateNotFoundJobStatusResponse("TestLocation", jobId.Jobid)),
                    _ => throw new ArgumentOutOfRangeException($"Неизвестный статус работы {result}")
                };
            }
            catch (Exception e)
            {
                var errorMessage = $"Не удалось проверить состояние задачи. Причина:{e.Message}";
                _logger?.LogError(e,errorMessage);
                return BadRequest(_responseGenerator.GenerateUnSuccessfulResponse(ErrorCodeType.UnknownError,errorMessage));
            }
        }
    }
}