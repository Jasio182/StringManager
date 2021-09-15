﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using StringManager.Services.API.Domain.Requests;
using StringManager.Services.API.Domain.Responses;
using System.Threading.Tasks;

namespace StringManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MyInstrumentsController : ApiControllerBase
    {
        public MyInstrumentsController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public Task<IActionResult> GetMyInstrumentsAsync([FromQuery] GetMyInstrumentsRequest request)
        {
            return HandleResult<GetMyInstrumentsRequest, GetMyInstrumentsResponse>(request);
        }

        [HttpGet]
        [Route("{Id}")]
        public Task<IActionResult> GetMyInstrumentAsync([FromQuery] GetMyInstrumentRequest request)
        {
            return HandleResult<GetMyInstrumentRequest, GetMyInstrumentResponse>(request);
        }

        [HttpPost]
        public Task<IActionResult> AddMyInstrumentAsync([FromBody] AddMyInstrumentRequest request)
        {
            return HandleResult<AddMyInstrumentRequest, AddMyInstrumentResponse>(request);
        }

        [HttpPut]
        public Task<IActionResult> ModifyMyInstrumentAsync([FromBody] ModifyMyInstrumentRequest request)
        {
            return HandleResult<ModifyMyInstrumentRequest, ModifyMyInstrumentResponse>(request);
        }

        [HttpDelete]
        public Task<IActionResult> RemoveMyInstrumentAsync([FromBody] RemoveMyInstrumentRequest request)
        {
            return HandleResult<RemoveMyInstrumentRequest, RemoveMyInstrumentResponse>(request);
        }
    }
}