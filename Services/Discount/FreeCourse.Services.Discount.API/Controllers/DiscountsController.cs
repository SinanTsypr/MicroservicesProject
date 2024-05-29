using FreeCourse.Services.Discount.API.Services;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountService.GetAll();

            return CreateActionResultInstance(response);
        }

        //api/discount/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);

            return CreateActionResultInstance(discount);
        }

        [HttpGet]
        [Route("[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;

            var discount = await _discountService.GetByCodeAndUserId(code, userId);

            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            var repsonse = await _discountService.Save(discount);

            return CreateActionResultInstance(repsonse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            var repsonse = await _discountService.Update(discount);

            return CreateActionResultInstance(repsonse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var repsonse = await _discountService.Delete(id);

            return CreateActionResultInstance(repsonse);
        }
    }
}
