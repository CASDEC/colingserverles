using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coling.Shared.Models;

[Table("profesionafiliado")]
public partial class ProfesionAfiliado
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idafiliado")]
    public int Idafiliado { get; set; }

    [Column("idprofesion")]
    public int Idprofesion { get; set; }

    [Column("fechaasignacion", TypeName = "datetime")]
    public DateTime Fechaasignacion { get; set; }

    [Column("nrosellosib")]
    [StringLength(25)]
    public string Nrosellosib { get; set; } = null!;

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("Idafiliado")]
    [InverseProperty("Profesionafiliados")]
    public virtual Afiliado IdafiliadoNavigation { get; set; } = null!;

}
