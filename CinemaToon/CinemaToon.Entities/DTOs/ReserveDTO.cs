namespace CinemaToon.Entities.DTOs
{
    public class ReserveDTO
    {
        public int FunctionId { get; set; }

        public int MovieId { get; set; }

        public string User { get; set; }

        public int NumberOfTickets { get; set; }

        public double Total { get; set; }

        public string OriginalMovieTitle { get; set; }

        public double PricePerTicket { get; set; }

    }
}
