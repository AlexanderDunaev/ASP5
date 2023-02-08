using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией о технике.
    /// </summary>
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано название техники")]
        [Display(Name = "Название техники")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указан инвентарный номер")]
        [Display(Name = "Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Required(ErrorMessage = "Не указан год поступления")]
        [Display(Name = "Год поступления")]
        [Range(1950, 2030, ErrorMessage = "Значение {0} должно быть между {1} и {2}.")]
        public int DeliveryYear { get; set; } = 2022;

        public List<Accounting> Accountings { get; set; }

        public List<Repair> Repairs { get; set; }
    }
}
