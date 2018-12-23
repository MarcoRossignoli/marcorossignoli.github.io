using System;
using System.Threading;
using System.Threading.Tasks;
using Keys.Application.Services.Contracts;
using Keys.Data.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Keys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeyController : ControllerBase
    {
        private readonly IKeyProvider _keyProvider;

        public KeyController(IKeyProvider keyProvider)
        {
            _keyProvider = keyProvider;
        }

        [HttpGet("uuid/{uuid:guid}/type/{type:keyType}")]
        public async Task<IActionResult> Get(Guid uuid, KeyType type, CancellationToken cancellationToken)
        {
            return Ok(await _keyProvider.GetByUuidAndTypeAsync(uuid, type, cancellationToken));
        }
    }
}