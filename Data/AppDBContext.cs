using HocTiengTrung.Models;
using Microsoft.EntityFrameworkCore;

namespace HocTiengTrung.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaiHoc> BaiHocs { get; set; }

        public DbSet<CauHoiGhepTu> CauHoiGhepTus { get; set; }
    }
}