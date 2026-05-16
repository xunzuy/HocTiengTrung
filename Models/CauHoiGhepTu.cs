namespace HocTiengTrung.Models
{
    public class CauHoiGhepTu
    {
        public int Id { get; set; }

        public string? CotBenTrai { get; set; }

        public string? CotBenPhai { get; set; }

        public int BaiHocId { get; set; }

        public BaiHoc? BaiHoc { get; set; }
    }
}