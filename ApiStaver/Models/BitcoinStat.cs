namespace ApiStaver.Models
{
    public class BitcoinStat
    {
        public int Id { get; set; }
        public int BlockNumber { get; set; }
        public int InPeer { get; set; }
        public int OutPeer { get; set; }
        public double? Difficulty { get; set; }
    }
}
