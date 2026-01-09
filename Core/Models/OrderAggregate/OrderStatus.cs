using System.Runtime.Serialization;

namespace Core.Models.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value="Pending")]
        Pending,
        [EnumMember(Value="PaymentReceived")]
        PaymentReceived,
        [EnumMember(Value ="PaymentFailed")]
        PaymentFailed
    }
}