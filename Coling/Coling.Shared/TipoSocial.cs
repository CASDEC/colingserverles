using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coling.Shared.Models;

[Table("tipoSocial")]
public partial class TipoSocial
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("nombresocial")]
    [StringLength(50)]
    public string Nombresocial { get; set; } = null!;

    [Column("estado")]
    [StringLength(20)]
    public string Estado { get; set; } = null!;

    [InverseProperty("IdtiposocialNavigation")]
    public virtual ICollection<PersonaTipoSocial> PersonatipoSociales { get; set; } = new List<PersonaTipoSocial>();
}
