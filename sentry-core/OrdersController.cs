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
    
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        try
        {
            return StatusCode(500, "Get Product error called");
        }
        catch (Exception ex) // 그 외 예상치 못한 모든 예외
        { ;
            return StatusCode(500, "An internal server error occurred.");
        }
    }
    [HttpGet("get2/{id}")]
    public IActionResult GetProduct2(int id)
    {
        try
        {
            return StatusCode(500, "get2 called");
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
            return StatusCode(500, "An internal server error occurred.");
        }
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
            SentrySdk.ConfigureScope(scope =>
            {
                var userPlan = createOrderDto.UserId == "user1" ? "premium" : "free";
                scope.SetTag("user_plan", userPlan);
                
                scope.Contexts["Order Payload"] = createOrderDto;
                
                SentrySdk.CaptureException(ex);
            });
            return StatusCode(500, "Failed to process order.");
            
        }
    }
}