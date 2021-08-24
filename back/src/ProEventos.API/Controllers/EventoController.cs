using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public IEnumerable<Evento> _evento = new Evento[] {
                new Evento() {
                    EventoId = 1,
                    Tema = "dotnet 5",
                    Local = "pinhão",
                    Lote = "primeiro lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString(),
                    ImagemUrl = "foto.png"
                },
                new Evento() {
                    EventoId = 2,
                    Tema = "dotnet 5",
                    Local = "pinhão2",
                    Lote = "primeiro lote",
                    QtdPessoas = 251,
                    DataEvento = DateTime.Now.AddDays(3).ToString(),
                    ImagemUrl = "foto2.png"
                }
            };

        public EventoController()
        {
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
            return _evento.Where(evento => evento.EventoId == id);
        }

        [HttpPost]
        public string Post()
        {
            return "EXEMPLO de POST";
        }

        [HttpPut("{id}")]
        public string put(int id)
        {
            return $"EXEMPLO de PUT id = {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"EXEMPLO de Delete id = {id}";
        }
    }
}
