namespace ApiStaver.Models
{
    public class ComputerStat
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double CpuUsage { get; set; }
        public double RamUsage { get; set; }
        public double DiskUsage { get; set; }
        public double Temperature { get; set; }
    }
}
