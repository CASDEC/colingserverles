using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coling.Shared.Models;

[Table("direccion")]
public partial class Direccion
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idpersona")]
    public int Idpersona { get; set; }

    [Column("descripcion")]
    public string Descripcion { get; set; } = null!;

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("Idpersona")]
    [InverseProperty("Direcciones")]
    public virtual Persona IdpersonaNavigation { get; set; } = null!;
}
