using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace miminal_api.Dominio.Entidades;

public class veiculo
{
  
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //identado
    public int Id { get; set; } = default;
    [Required]
    [StringLength(255)]//tamanho de caracteres
    public string Marca { get; set; } = default!;
      [StringLength(50)]
    public int Ano { get; set; } = default!;
      [StringLength(10)]
       public string Nome { get; set; } = default!;
}