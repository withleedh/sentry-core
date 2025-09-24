using Microsoft.AspNetCore.Mvc;
using sentry_core.DTOs;
using sentry_core.Services;

namespace sentry_core;

using Sentry; 

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    // 생성자 주입을 통해 서비스 인스턴스를 받습니다.
    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        try
        {
            var result = await _ordersService.CreateAsync(createOrderDto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // 🧑‍🔬 Sentry Scope를 이용해 이번 예외에만 적용될 정보를 추가합니다.
            SentrySdk.ConfigureScope(scope =>
            {
                // 🏷️ Tag: 검색과 필터링을 위해 사용합니다.
                var userPlan = createOrderDto.UserId == "user1" ? "premium" : "free";
                scope.SetTag("user_plan", userPlan);
                
                scope.Contexts["Order Payload"] = createOrderDto;
                
                SentrySdk.CaptureException(ex);
            });
            return StatusCode(500, "Failed to process order.");
        }
    }
}