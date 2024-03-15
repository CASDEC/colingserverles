using Coling.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<Direccion> Direcciones {  get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<PersonaTipoSocial> PersonatipoSociales { get; set; }
        public DbSet<ProfesionAfiliado> ProfesionAfiliados { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<TipoSocial> TipoSociales { get; set; }
    }
}
