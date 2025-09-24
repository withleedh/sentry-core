using sentry_core.DTOs;

namespace sentry_core.Services;

public class OrdersService : IOrdersService
{
    // 가상의 유저 정보 조회 함수
    private object FindUserById(string userId)
    {
        if (userId == "user1")
        {
            return new { Id = "user1", Name = "GIT", Plan = "premium" };
        }
        return new { Id = "user2", Name = "AUTO", Plan = "free" };
    }

    public async Task<object> CreateAsync(CreateOrderDto createOrderDto)
    {
        var user = FindUserById(createOrderDto.UserId);

        Console.WriteLine($"수행합니다.");

        // ... 재고 확인, 데이터베이스 저장 등 로직의 비동기
        await Task.Delay(50); 

        // 결제 연동 실패
        var isPaymentSuccess = false;
        if (!isPaymentSuccess)
        {
            throw new InvalidOperationException("Payment failed");
        }

        return new { success = true, orderId = "order-20250925-uuid" };
    }
}