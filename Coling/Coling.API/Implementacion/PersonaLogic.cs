using Coling.API.Afiliados.Contratos;
using Coling.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class PersonaLogic : IPersonaLogic
    {
        private readonly Contexto contexto;

        public PersonaLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarPersona(Persona persona)
        {
            contexto.Personas.Add(persona);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarPersona(Persona persona, int id)
        {
            Persona pers = await contexto.Personas.FindAsync(id);
            if (pers == null)
            {
                return false;
            }
            contexto.Entry(pers).CurrentValues.SetValues(persona);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPersona(int id)
        {
            var persona = await contexto.Personas.FindAsync(id);
            if (persona == null)
            {
                return false;
            }
            contexto.Personas.Remove(persona);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Persona>> ListarPersonas()
        {
            return await contexto.Personas.ToListAsync();
        }

        public async Task<Persona> ObtenerPersonaById(int id)
        {
            return await contexto.Personas.FindAsync(id);
        }
    }

}
