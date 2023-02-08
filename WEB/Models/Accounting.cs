using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией об учёте техники.
    /// </summary>
    public class Accounting
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана дата поступления")]
        [Display(Name = "Дата поступления")]
        [DataType(DataType.Date)]
        public DateTime AccountingDate { get; set; }

        [Display(Name = "Название подразделения")]
        public int DepartmentId { get; set; }         // внешний ключ (Department)
        [Display(Name = "Название подразделения")]
        public Department Department { get; set; }    // навигационное свойство (Department)

        [Display(Name = "Название техники")]
        public int EquipmentId { get; set; }        // внешний ключ (Equipment)
        [Display(Name = "Название техники")]
        public Equipment Equipment { get; set; }    // навигационное свойство (Equipment)
    }
}
