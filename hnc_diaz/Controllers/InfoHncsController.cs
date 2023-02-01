using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hnc_diaz.Models;
using System.Runtime;
using Microsoft.IdentityModel.Tokens;

namespace hnc_diaz.Controllers
{
    public class InfoHncsController : Controller
    {
        private readonly HncDbContext _context;
        private InfoHnc InfoHnc;

        public InfoHncsController(HncDbContext context)
        {
            _context = context;
        }

        // GET: InfoHncs
        public async Task<IActionResult> Index()
        {
              return View(await _context.InfoHncs.ToListAsync());
        }

        [HttpPost]
        public IActionResult Validacion(string MatriculaInfo, string FechaInfo, string HoraInfo)
        {
            string est;
            
            InfoHnc = new InfoHnc();

            Random rnd = new Random();
            InfoHnc.IdInfo = rnd.Next();
            InfoHnc.MatriculaInfo = MatriculaInfo;
            InfoHnc.FechaInfo = FechaInfo;
            InfoHnc.HoraInfo = HoraInfo;
           
            // Sacar el último carácter de la MatriculaInfo
            char ultimoCaracter = MatriculaInfo[MatriculaInfo.Length - 1];

            // Asignar 1 o 2 con Monday, 3 o 4 con Tuesday, 5 o 6 con Wednesday, 7 u 8 con Thursday y 9 o 0 con Friday
            DayOfWeek diaSemana;
            diaSemana = DayOfWeek.Sunday;
            switch (ultimoCaracter)
            {
                case '1':
                    diaSemana = DayOfWeek.Monday;
                    break;
                case '2':
                    diaSemana = DayOfWeek.Monday;
                    break;
                case '3':
                    diaSemana = DayOfWeek.Tuesday;
                    break;
                case '4':
                    diaSemana = DayOfWeek.Tuesday;
                    break;
                case '5':
                    diaSemana = DayOfWeek.Wednesday;
                    break;
                case '6':
                    diaSemana = DayOfWeek.Wednesday;
                    break;
                case '7':
                    diaSemana = DayOfWeek.Thursday;
                    break;
                case '8':
                    diaSemana = DayOfWeek.Thursday;
                    break;
                case '9':
                    diaSemana = DayOfWeek.Friday;
                    break;
                case '0':
                    diaSemana = DayOfWeek.Friday;
                    break;
                default:
                    diaSemana = DayOfWeek.Saturday;
                    break;
            }

            // Convertir la fecha ingresada en texto a un objeto de tipo DateTime
            DateTime fecha = Convert.ToDateTime(FechaInfo);

            // Verificar si la fecha ingresada es el mismo día de la semana que el último carácter de la MatriculaInfo
            if (fecha.DayOfWeek != diaSemana)
            {
                // ModelState.AddModelError("FechaInfo", "La fecha ingresada es un fin de semana");
                est = "Puede Circular";
                InfoHnc.EstadoInfo = est;
            }
            else
            {
                // Convertir la hora ingresada en texto a un objeto de tipo TimeSpan
                TimeSpan hora = TimeSpan.Parse(HoraInfo);

                // Verificar si la hora ingresada es mayor a 07:00 y menor a 09:00 o mayor a 16:00 y menor a 21:00
                if (!((hora >= new TimeSpan(7, 0, 0) && hora <= new TimeSpan(9, 0, 0)) || (hora >= new TimeSpan(16, 0, 0) && hora <= new TimeSpan(21, 0, 0))))
                {
                    ModelState.AddModelError("HoraInfo", "La hora ingresada no puede circular");
                    est = "Puede Circular";
                    InfoHnc.EstadoInfo = est;
                }
                else
                {          
                    est = "no Puede Circular";
                    InfoHnc.EstadoInfo = est;
                }
               
            }


            // Si no hay errores, aceptar y guardar los datos en la base de datos
            if (ModelState.IsValid)
            {
                // Guardar los datos en la base de datos
                using (var context = new HncDbContext())
                {
                    var info = new InfoHnc
                    {
                        IdInfo = InfoHnc.IdInfo,
                        MatriculaInfo = MatriculaInfo,
                        FechaInfo = FechaInfo,
                        HoraInfo = HoraInfo,
                        EstadoInfo = InfoHnc.EstadoInfo
                    };

                    _context.Add(InfoHnc);
                    
                    _context.SaveChanges();
                }

                ViewBag.Mensaje = "Puede circular";
                return View(InfoHnc);
            }
            else
            {
                ViewBag.Mensaje = "No puede circular";
               
            }

            return View(InfoHnc);
        }

        // GET: InfoHncs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InfoHncs == null)
            {
                return NotFound();
            }

            var infoHnc = await _context.InfoHncs
                .FirstOrDefaultAsync(m => m.IdInfo == id);
            if (infoHnc == null)
            {
                return NotFound();
            }

            return View(infoHnc);
        }

        // GET: InfoHncs/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: InfoHncs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInfo,MatriculaInfo,FechaInfo,HoraInfo,EstadoInfo")] InfoHnc infoHnc)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(infoHnc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(infoHnc);
        }

        // GET: InfoHncs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InfoHncs == null)
            {
                return NotFound();
            }

            var infoHnc = await _context.InfoHncs.FindAsync(id);
            if (infoHnc == null)
            {
                return NotFound();
            }
            return View(infoHnc);
        }

       

        // POST: InfoHncs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInfo,MatriculaInfo,FechaInfo,HoraInfo,EstadoInfo")] InfoHnc infoHnc)
        {
            if (id != infoHnc.IdInfo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(infoHnc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InfoHncExists(infoHnc.IdInfo))
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
            return View(infoHnc);
        }

        // GET: InfoHncs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InfoHncs == null)
            {
                return NotFound();
            }

            var infoHnc = await _context.InfoHncs
                .FirstOrDefaultAsync(m => m.IdInfo == id);
            if (infoHnc == null)
            {
                return NotFound();
            }

            return View(infoHnc);
        }

        // POST: InfoHncs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InfoHncs == null)
            {
                return Problem("Entity set 'HncDbContext.InfoHncs'  is null.");
            }
            var infoHnc = await _context.InfoHncs.FindAsync(id);
            if (infoHnc != null)
            {
                _context.InfoHncs.Remove(infoHnc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InfoHncExists(int id)
        {
          return _context.InfoHncs.Any(e => e.IdInfo == id);
        }

       
    }
}
