﻿namespace Model
{

    public class Reservation
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ReservationDate { get; set; }
        public float Subtotal { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PendingPayment;
        public string? CommandNumber { get; set; }
        public string? StripeReference { get; set; }
    }
}
