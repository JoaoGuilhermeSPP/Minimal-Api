using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miminal_api.Dominio.Entidades;

public class Administrador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //identado
    public int Id { get; set; } = default;
    [Required]
    [StringLength(255)]//tamanho de caracteres
    public string Email { get; set; } = default;
      [StringLength(50)]
    public string Senha { get; set; } = default;
      [StringLength(10)]
       public string Perfil { get; set; } = default;
}