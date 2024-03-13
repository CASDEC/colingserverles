﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Coling.Shared.Models;

[Table("telefono")]
public partial class Telefono
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idpersona")]
    public int Idpersona { get; set; }

    [Column("nrotelefono")]
    [StringLength(60)]
    public string Nrotelefono { get; set; } = null!;

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("Idpersona")]
    [InverseProperty("Telefonos")]
    public virtual Persona IdpersonaNavigation { get; set; } = null!;
}