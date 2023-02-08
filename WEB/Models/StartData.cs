using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB.Data;

namespace WEB.Models
{
    /// <summary>
    /// Класс с начальными данными.
    /// </summary>
    public class StartData
    {
        /// <summary>
        /// Заполнение базы данных начальными данными.
        /// </summary>
        /// <param name="context">Контекст доступа к данным.</param>
        public static void Initialize(ApplicationContext context)
        {
            if (!context.Departments.Any())
            {
                context.Departments.AddRange(
                    new Department
                    {
                        Name = "Отдел кадров"
                    },
                    new Department
                    {
                        Name = "Бухгалтерия"
                    },
                    new Department
                    {
                        Name = "Группа юридического обеспечения"
                    }
                );
                context.SaveChanges();
                context.Equipments.AddRange(
                    new Equipment
                    {
                        Name = "(нет)",
                        InventoryNumber = "000000",
                        DeliveryYear = 2022
                    },
                    new Equipment
                    {
                        Name = "Принтер Samsung 2160",
                        InventoryNumber = "1234567",
                        DeliveryYear = 2021
                    },
                    new Equipment
                    {
                        Name = "Принтер HP Laser 150nw",
                        InventoryNumber = "1234568",
                        DeliveryYear = 2022
                    },
                    new Equipment
                    {
                        Name = "Плоттер HP Designjet T230",
                        InventoryNumber = "1234569",
                        DeliveryYear = 2019
                    }
                    );
                context.SaveChanges();
                context.Accountings.AddRange(
                    new Accounting
                    {
                        AccountingDate = Convert.ToDateTime("01.01.2022").Date,
                        DepartmentId = 1,
                        EquipmentId = 1
                    },
                    new Accounting
                    {
                        AccountingDate = Convert.ToDateTime("02.03.2022").Date,
                        DepartmentId = 2,
                        EquipmentId = 3
                    },
                   new Accounting
                   {
                       AccountingDate = Convert.ToDateTime("03.02.2022").Date,
                       DepartmentId = 3,
                       EquipmentId = 2
                   });
                context.SaveChanges();
                context.Statuses.AddRange(
                    new Status
                    {
                        Name = "Заявка подана"
                    },
                    new Status
                    {
                        Name = "На выполнении"
                    },
                    new Status
                    {
                        Name = "Закрыта"
                    }
                    );
                context.SaveChanges();
                context.Softs.AddRange(
                    new Soft
                    {
                        Name = "(нет)",
                        Version = "(нет)",
                    },
                    new Soft
                    {
                        Name = "Windows",
                        Version = "10 21H2",
                        Description = "Операционная система"
                    },
                    new Soft
                    {
                        Name = "Microsoft Office",
                        Version = "2021",
                        Description = "Офисный пакет приложений"
                    }
                    );
                context.SaveChanges();
                context.Executors.AddRange(
                    new Executor
                    {
                        SurName = "Иванов",
                        Name = "Иван",
                        Patronymic = "Иванович"
                    },
                    new Executor
                    {
                        SurName = "Петров",
                        Name = "Пётр",
                        Patronymic = "Петрович"
                    },
                    new Executor
                    {
                        SurName = "Сидоров",
                        Name = "Сидор",
                        Patronymic = "Сидорович"
                    }
                    );
                context.SaveChanges();
                context.Repairs.AddRange(
                    new Repair
                    {
                        RepairDate = DateTime.Now,
                        RepairTime = DateTime.Now,
                        RepairChangeDate = DateTime.Now,
                        RepairChangeTime = DateTime.Now,
                        SoftId = 2,
                        ExecutorId = 1,
                        StatusId = 1,
                        EquipmentId = 1,
                        DescriptionProblem = "Сбой активации продукта",
                        RepairUser = "testuser@gmail.com"
                    },
                        new Repair
                        {
                            RepairDate = DateTime.Now,
                            RepairTime = DateTime.Now,
                            RepairChangeDate = DateTime.Now,
                            RepairChangeTime = DateTime.Now,
                            SoftId = 1,
                            ExecutorId = 2,
                            StatusId = 1,
                            EquipmentId = 2,
                            DescriptionProblem = "Не захватывает бумагу при печати",
                            RepairUser= "testuser@gmail.com"
                        }
                        );
                context.SaveChanges();
            }
        }
    }
}
