using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HocTiengTrung.Data;
using HocTiengTrung.Models;

namespace HocTiengTrung.Controllers
{
    public class GhepTuController : Controller
    {
        private readonly AppDbContext _context;

        public GhepTuController(AppDbContext context)
        {
            _context = context;
        }

        // GET: GhepTu
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.CauHoiGhepTus.Include(c => c.BaiHoc);
            return View(await appDbContext.ToListAsync());
        }

        // GET: GhepTu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoiGhepTu = await _context.CauHoiGhepTus
                .Include(c => c.BaiHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cauHoiGhepTu == null)
            {
                return NotFound();
            }

            return View(cauHoiGhepTu);
        }

        // GET: GhepTu/Create
        public IActionResult Create()
        {
            ViewData["BaiHocId"] = new SelectList(_context.BaiHocs, "Id", "Id");
            return View();
        }

        // POST: GhepTu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CotBenTrai,CotBenPhai,BaiHocId")] CauHoiGhepTu cauHoiGhepTu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cauHoiGhepTu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaiHocId"] = new SelectList(_context.BaiHocs, "Id", "Id", cauHoiGhepTu.BaiHocId);
            return View(cauHoiGhepTu);
        }

        // GET: GhepTu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoiGhepTu = await _context.CauHoiGhepTus.FindAsync(id);
            if (cauHoiGhepTu == null)
            {
                return NotFound();
            }
            ViewData["BaiHocId"] = new SelectList(_context.BaiHocs, "Id", "Id", cauHoiGhepTu.BaiHocId);
            return View(cauHoiGhepTu);
        }

        // POST: GhepTu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CotBenTrai,CotBenPhai,BaiHocId")] CauHoiGhepTu cauHoiGhepTu)
        {
            if (id != cauHoiGhepTu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cauHoiGhepTu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauHoiGhepTuExists(cauHoiGhepTu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BaiHocId"] = new SelectList(_context.BaiHocs, "Id", "Id", cauHoiGhepTu.BaiHocId);
            return View(cauHoiGhepTu);
        }

        // GET: GhepTu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cauHoiGhepTu = await _context.CauHoiGhepTus
                .Include(c => c.BaiHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cauHoiGhepTu == null)
            {
                return NotFound();
            }

            return View(cauHoiGhepTu);
        }

        // POST: GhepTu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cauHoiGhepTu = await _context.CauHoiGhepTus.FindAsync(id);
            if (cauHoiGhepTu != null)
            {
                _context.CauHoiGhepTus.Remove(cauHoiGhepTu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Danh sách bài học
        public IActionResult DanhSachBai()
        {
            var danhSach =
                _context.BaiHocs.ToList();

            return View(danhSach);
        }

        // Làm bài kéo thả
        public IActionResult LamBai(int id)
        {
            var cauHoi = _context.CauHoiGhepTus
                .Where(x => x.BaiHocId == id)
                .ToList();

            // Trộn đáp án
            var dapAn = cauHoi
                .Select(x => x.CotBenPhai)
                .OrderBy(x => Guid.NewGuid())
                .ToList();

            ViewBag.DapAn = dapAn;

            return View(cauHoi);
        }

        private bool CauHoiGhepTuExists(int id)
        {
            return _context.CauHoiGhepTus.Any(e => e.Id == id);
        }
    }
}
