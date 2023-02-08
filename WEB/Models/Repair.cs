using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WEB.Models
{
    /// <summary>
    /// Класс с информацией о заявке на ремонт.
    /// </summary>
    public class Repair
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Дата заявки")]
        [DataType(DataType.Date)]
        public DateTime RepairDate { get; set; }

        [Display(Name = "Время заявки")]
        [DataType(DataType.Time)]
        public DateTime RepairTime { get; set; }

        [Display(Name = "Дата изменения заявки")]
        [DataType(DataType.Date)]
        public DateTime RepairChangeDate { get; set; }

        [Display(Name = "Время изменения заявки")]
        [DataType(DataType.Time)]
        public DateTime RepairChangeTime { get; set; }

        [Required(ErrorMessage = "Не указано описание проблемы")]
        [Display(Name = "Описание проблемы")]
        [DataType(DataType.MultilineText)]
        public string DescriptionProblem { get; set; }

        [Display(Name = "Пользователь")]
        public string RepairUser { get; set; }

        [Display(Name = "Статус выполнения")]
        public int StatusId { get; set; }           // внешний ключ (Status)
        [Display(Name = "Статус выполнения")]
        public Status Status { get; set; }          // навигационное свойство (Status)

        [Display(Name = "Название техники")]
        public int EquipmentId { get; set; }        // внешний ключ (Equipment)
        [Display(Name = "Название техники")]
        public Equipment Equipment { get; set; }    // навигационное свойство (Equipment)

        [Display(Name = "Название программного обеспечения")]
        public int SoftId { get; set; }             // внешний ключ (Soft)
        [Display(Name = "Название программного обеспечения")]
        public Soft Soft { get; set; }              // навигационное свойство (Soft)

        [Display(Name = "Фамилия исполнителя")]
        public int? ExecutorId { get; set; }         // внешний ключ (Executor)
        [Display(Name = "Фамилия исполнителя")]
        public Executor Executor { get; set; }      // навигационное свойство (Executor)
    }
}