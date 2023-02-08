using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией о подразделении.
    /// </summary>
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название подразделения")]
        [Display(Name = "Название подразделения")]
        public string Name { get; set; }

        public List<Accounting> Accountings { get; set; }
    }
}
