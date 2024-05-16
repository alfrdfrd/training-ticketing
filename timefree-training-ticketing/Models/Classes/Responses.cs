using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Models.Classes
{
    public class Responses
    {
        public int ResponseCode { get; set; }
        public string? ResponseLabel { get; set; }
        public string? ResponseMessage { get; set; }
    }

    public class UserResponse : Responses
    {
        public user User { get; set; }
    }

    public class TicketResponse : Responses
    {
        public ticket Ticket { get; set; }
    }

    public class OrderResponse : Responses
    {
        public order Order { get; set; }
    }
}
