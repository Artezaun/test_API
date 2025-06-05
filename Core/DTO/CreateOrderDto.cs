using Core.Models;
using Core.Enums;
namespace Core.DTO
{
    public class CreateOrderDto
    {
        // Сумма заказа
        public decimal Amount { get; set; }

        // Статус заказа
        public OrderStatus Status { get; set; } = OrderStatus.notprocessed;

        // Дата и время заказа
        public DateTime OrderDatetime { get; set; }

        // Идентификатор клиента (внешний ключ)
        public int client_id { get; set; }
    }
}
