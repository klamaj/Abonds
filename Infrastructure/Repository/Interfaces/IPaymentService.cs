namespace Infrastructure.Repository.Interfaces
{
    public interface IPaymentService
    {
        Task CreatePaymentIntent();
    }
}