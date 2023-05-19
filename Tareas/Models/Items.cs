using System.ComponentModel.DataAnnotations;

namespace Tareas.Models
{
    public class Items
    {
        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string Responsible { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int IsComplete { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Estate {get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Dia { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Mes { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "Este campo no se puede dejar vacío")]
        public string IdUser { get; set; }


    }
}
