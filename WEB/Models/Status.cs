using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией о статусе заявки на ремонт.
    /// </summary>
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан стаатус выполнения заявки")]
        [Display(Name = "Статус выполнения")]
        public string Name { get; set; }

        public List<Repair> Repairs { get; set; }
    }
}
