using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией об исполнителе заявки.
    /// </summary>
    public class Executor
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана фамилия исполнителя")]
        [Display(Name = "Фамилия исполнителя")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Не указано имя исполнителя")]
        [Display(Name = "Имя исполнителя")]
        public string Name { get; set; }

        [Display(Name = "Отчество исполнителя")]
        public string Patronymic { get; set; }

        public List<Repair> Repairs { get; set; }
    }
}
