using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletTransafer_Assessment.Model;
using WalletTransafer_Assessment.Services;

namespace WalletTransafer_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }


        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
        {
            var response = await _walletService.TransferAsync(request);
            if (!response.Success)
                return BadRequest(response);
            
            
            return Ok(response);
        }
    }
}
