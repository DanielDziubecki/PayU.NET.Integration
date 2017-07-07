namespace PayU.Client.Services
{
    public interface IPaymentService
    {
        void PayForOrder(OrderDto order);
    }
}