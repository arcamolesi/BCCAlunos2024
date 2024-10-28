using BCCAlunos2024.Models;
using BCCAlunos2024.Models.Consultas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;


namespace BCCAlunos2024.Controllers
{
    public class ConsultasController : Controller
    {

        private readonly Contexto contexto;

        public ConsultasController(Contexto context)
        {
            contexto = context;
        }


        public IActionResult AtendimentosPorTipo()
        {
            var lista = contexto.Atendimentos
                .Include(a => a.aluno).Include(s => s.sala)
                .OrderBy(t => t.tipo).ThenBy(s => s.sala.descricao)
                .ThenByDescending(a => a.aluno.nome)
                .ToList();

            return View(lista);
        }


        public IActionResult Alunos()
        {
            var alunos = contexto.Alunos.Include(c => c.curso)
                .OrderBy(o => o.curso.descricao)
                .ThenByDescending(p => p.periodo)
                .ThenBy(o1 => o1.aniversario)
                .ToList();
            return View(alunos);
        }

        public IActionResult FiltrarAluno()
        {
            return View();
        }

        public IActionResult ResFiltrarAluno(int? id, string nome, DateTime? dataIni, DateTime? dataFim)
        {
            List<Aluno> listaAlunos = new List<Aluno>();

            if (dataIni != null && dataFim != null) //filtra por período de data
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(dt => dt.aniversario >= dataIni && dt.aniversario <= dataFim).OrderBy(n => n.aniversario).ToList();

            else if (id != null) //filtrar por id
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(a => a.id == id).ToList();
            else
                   if (!nome.IsNullOrEmpty()) //filtrar por nome 
                                              //listaAlunos = contexto.Alunos.Include(a => a.curso).Where(n=>n.nome==nome).ToList();
                listaAlunos = contexto.Alunos.Include(a => a.curso).Where(n => n.nome.Contains(nome)).OrderBy(o => o.nome).ToList();

            else
                listaAlunos = contexto.Alunos.Include(a => a.curso).ToList();

            return View(listaAlunos);
        }


        public IActionResult AgruparAtendimentoPorCursoSala()
        {
            IEnumerable<AgruparAtendimentoCursoSala> lstAgAtCursoSala =

                  from item in contexto.Atendimentos
                   .Include(a => a.aluno).Include(c => c.aluno.curso).Include(s => s.sala)
                   .ToList()
                  let curso = item.aluno.curso.descricao
                  let sala = item.sala.descricao
                  group item by new { curso, sala }
                  into grupo
                  orderby grupo.Key.sala, grupo.Key.curso
                  select new AgruparAtendimentoCursoSala
                  {
                      curso = grupo.Key.curso,
                      sala = grupo.Key.sala,
                      quantidade = grupo.Count()
                  };
            return View(lstAgAtCursoSala);
        }


        public IActionResult AgruparAtendimentoPorAnoMes() {

            IEnumerable<AgruparAtendimentoPorAnoMes> lstAtAnoMes  =

          from item in contexto.Atendimentos
             .ToList()
          let ano = item.dataHora.Year
          let mes = item.dataHora.Month

          group item by new { ano, mes }
          into grupo
          orderby grupo.Key.ano, grupo.Key.mes
          select new AgruparAtendimentoPorAnoMes
          {
              ano = grupo.Key.ano,
              mes = grupo.Key.mes,
              quantidade = grupo.Count()
          };
            return View(lstAtAnoMes);

        }


    }
}
