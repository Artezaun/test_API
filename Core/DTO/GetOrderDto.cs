using Core.Models;
using Core.Enums;

namespace Core.DTO
{
    public class GetOrderDto
    {

        public GetOrderDto(Order order)
        {
            id = order.id;
            Amount = order.Amount;
            Status = order.Status;
            OrderDatetime = order.OrderDatetime;
            client_id = order.client_id;
        }

        public GetOrderDto(int id, decimal Amount, OrderStatus Status, DateTime OrderDatetime, int client_id)
        {
            this.id = id;
            this.Amount = Amount;
            this.Status = Status;
            this.OrderDatetime = OrderDatetime;
            this.client_id = client_id;
        }

        public int id { get; set; }

        public decimal Amount { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.notprocessed;

        public DateTime OrderDatetime { get; set; }

        // Внешний ключ
        public int client_id { get; set; }
    }
}
