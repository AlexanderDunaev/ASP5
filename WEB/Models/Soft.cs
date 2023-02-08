using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией о программном обеспечении.
    /// </summary>
    public class Soft
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название программного обеспечения")]
        [Display(Name = "Название программного обеспечения")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана версия программного обеспечения")]
        [Display(Name = "Версия программного обеспечения")]
        public string Version { get; set; }

        [Display(Name = "Описание программного обеспечения")]
        public string Description { get; set; }

        public List<Repair> Repairs { get; set; }
    }
}
