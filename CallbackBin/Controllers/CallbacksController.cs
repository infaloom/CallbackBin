using CallbackBin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net;

namespace CallbackBin.Controllers
{
    [Route("api/callbacks")]
    [ApiController]
    public class CallbacksController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public CallbacksController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("")]
        public IActionResult Upsert(Guid guid, [FromBody] object body, int expiryInMilliseconds = 60 * 60 * 1000)
        {
            var storeItem = new CallbackStoreModel()
            {
                Headers = Request.Headers,
                Body = body
            };
            _memoryCache.Set(guid, storeItem, TimeSpan.FromMilliseconds(expiryInMilliseconds));
            return StatusCode((int)HttpStatusCode.Created, storeItem);
        }

        [HttpGet]
        public IActionResult Read(Guid guid)
        {
            var request = _memoryCache.Get<CallbackStoreModel>(guid);
            if (request == null)
            {
                return NotFound();
            }
            return Ok(request);
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            _memoryCache.Remove(guid);
            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}