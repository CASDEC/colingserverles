using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coling.Shared.Models;

[Table("personatipoSocial")]
public partial class PersonatipoSocial
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idtiposocial")]
    public int Idtiposocial { get; set; }

    [Column("idpersona")]
    public int Idpersona { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("Idpersona")]
    [InverseProperty("PersonatipoSocials")]
    public virtual Persona IdpersonaNavigation { get; set; } = null!;

    [ForeignKey("Idtiposocial")]
    [InverseProperty("PersonatipoSocials")]
    public virtual TipoSocial IdtiposocialNavigation { get; set; } = null!;
}
