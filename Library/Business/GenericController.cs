using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Library.Extensions;
using Library.Models.Dto;
using Library.Utilities.QueryParameters;
using Library.Utilities.Results;
using Library.Utilities.Results.Abstract; 
using Microsoft.AspNetCore.Mvc;

namespace Library.Business
{
    [
        ApiController,
        Produces(MediaTypeNames.Application.Json),
        Route("api/v1/[controller]")
    ]
    public abstract class GenericController<TCreate, TUpdate, TKey, TService> : ControllerBase
        where TCreate : class, IDto, new()
        where TUpdate : class, IDto, new()
        where TKey : IEquatable<TKey>
        where TService : IServiceBase<TCreate, TUpdate, TKey>
    {
        protected readonly TService service;

        protected GenericController(TService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IPaginationResult<>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Unauthorized)]
        public virtual IActionResult List([FromQuery] QueryParameter parameter)
        {
            var result = service?.List(parameter);
            return this.GetResult(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> ShowItem(TKey id)
        {
            var result = await service?.ShowItem(id);
            return this.GetResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Create([FromBody] TCreate entity)
        {
            var result = await service?.Create(entity);
            return this.GetResult(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Update(TKey id, [FromBody] TUpdate entity)
        {
            var result = await service?.Update(id, entity);
            return this.GetResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int) HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(IResult), (int) HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> Delete(TKey id)
        {
            var result = await service?.Delete(id);
            return this.GetResult(result);
        }
    }
}