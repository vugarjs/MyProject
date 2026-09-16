using Microsoft.Extensions.DependencyInjection;
using MyProject.Business.Services.Implementations;
using MyProject.Business.Services.Interfaces;
using MyProject.DataAccess.Context;
using MyProject.DataAccess.Repositories.Implementations;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models;
using System.Text;



namespace MyProject.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var services = new ServiceCollection(); // Here we are creating a new instance of ServiceCollection, which is a container for registering services and their dependencies.

            services.AddDbContext<DepartmentContext>(); // Registering the DepartmentContext with the service collection. This allows us to use dependency injection to get an instance of DepartmentContext wherever we need it.
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            var serviceProvider = services.BuildServiceProvider(); // Menyular ucun!

            // 3. Servislərin çağırılması
            var departmentService = serviceProvider.GetRequiredService<IDepartmentService>(); // Menyularin icinde servislere erishmek ucun serviceProvider-dan istifade edirik. Bu, IDepartmentService tipində bir servis nümunəsini alır və onu departmentService dəyişəninə təyin edir.
            var employeeService = serviceProvider.GetRequiredService<IEmployeeService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            var DepartmentRepo = serviceProvider.GetRequiredService<DepartmentRepository>();



            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== İDARƏETMƏ SİSTEMİ ===");
                Console.WriteLine("1. Departamentləri idarə et");
                Console.WriteLine("2. İşçiləri idarə et");
                Console.WriteLine("0. Çıxış");
                Console.Write("Seçiminizi edin: ");

                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        await DepartmentMenu(departmentService);
                        break;
                    case "2":
                        await EmployeeMenu(employeeService);
                        break;
                    case "0":
                        Console.WriteLine("Proqramdan çıxılır...");
                        return;
                    default:
                        Console.WriteLine("Yanlış seçim! Davam etmək üçün hər hansı bir düyməyə basın.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static async Task DepartmentMenu(IDepartmentService departmentService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- DEPARTAMENT MENYUSU ---");
                Console.WriteLine("1. Departament əlavə et");
                Console.WriteLine("2. Departamentə düzəliş et");
                Console.WriteLine("3. Departamenti sil");
                Console.WriteLine("4. Bütün departamentlərə bax");
                Console.WriteLine("5. ID-yə görə axtar");
                Console.WriteLine("6. Ada görə axtar");
                Console.WriteLine("0. Ana menyuya qayıt");
                Console.Write("Seçiminizi edin: ");

                string choice = Console.ReadLine()!;
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Departamentin adı: ");
                            string name = Console.ReadLine()!;
                            Console.Write("Departament limiti: ");
                            int.TryParse(Console.ReadLine(), out int limit);

                            Console.WriteLine("Description: ");
                            string description = Console.ReadLine()!;

                            var newDept = new Department
                            {
                                Name = name,
                                Limit = limit,
                                Description = description
                            };

                            await departmentService.AddAsync(newDept);
                            Console.WriteLine("Departament uğurla əlavə edildi!");
                            break;

                        case "2":
                            Console.Write("Düzəliş ediləcək Departamentin ID-si: ");
                            if (int.TryParse(Console.ReadLine(), out int updateId))
                            {
                                var dept = await departmentService.GetByIdAsync(updateId);
                                if (dept != null)
                                {
                                    Console.Write("Yeni ad: ");
                                    dept.Name = Console.ReadLine()!;

                                    departmentService.Update(dept);
                                    Console.WriteLine("Departament uğurla yeniləndi!");
                                }
                                else Console.WriteLine("Bu ID-də departament tapılmadı.");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "3":
                            Console.Write("Silinəcək Departamentin ID-si: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                            {
                                departmentService.Remove(deleteId);
                                Console.WriteLine("Departament silindi!");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "4":
                            Console.WriteLine("--- Departamentlərin Siyahısı ---");
                            var departments = await departmentService.GetAllAsync();
                            foreach (var d in departments)
                            {
                                Console.WriteLine($"ID: {d.Id} | Ad: {d.Name} | Açığlama: {d.Description} | Limit: {d.Limit}");
                            }
                            break;

                        case "5":
                            Console.Write("Axtarılan ID: ");
                            if (int.TryParse(Console.ReadLine(), out int searchId))
                            {
                                var d = await departmentService.GetByIdAsync(searchId);
                                if (d != null) Console.WriteLine($"ID: {d.Id} | Ad: {d.Name}");
                                else Console.WriteLine("Tapılmadı.");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "6":
                            Console.Write("Axtarılan Ad: ");
                            string searchName = Console.ReadLine()!;
                            var foundDepts = await departmentService.GetByNameAsync(searchName);
                            if (foundDepts != null)
                            {
                                Console.WriteLine($"ID: {foundDepts.Id} | Ad: {foundDepts.Name}");
                            }
                            else Console.WriteLine("Bu ada uyğun departament tapılmadı.");
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Yanlış seçim!");
                            break;
                    }
                }
                // Servisdəki fərdi xətaları (throw new Exception) tutmaq üçün try-catch
                catch (Exception ex)
                {
                    Console.WriteLine($"XƏTA: {ex.Message}");
                }

                Console.WriteLine("\nDavam etmək üçün hər hansı bir düyməyə basın...");
                Console.ReadKey();
            }
        }

        static async Task EmployeeMenu(IEmployeeService employeeService)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- İŞÇİ MENYUSU ---");
                Console.WriteLine("1. İşçi əlavə et");
                Console.WriteLine("2. İşçiyə düzəliş et");
                Console.WriteLine("3. İşçini sil");
                Console.WriteLine("4. Bütün işçilərə bax");
                Console.WriteLine("5. ID-yə görə axtar");
                Console.WriteLine("0. Ana menyuya qayıt");
                Console.Write("Seçiminizi edin: ");

                string choice = Console.ReadLine()!;
                Console.WriteLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Write("İşçinin Adı: ");
                            string name = Console.ReadLine()!;
                            Console.Write("Email: ");
                            string email = Console.ReadLine()!;
                            Console.Write("Departament ID: ");
                            int.TryParse(Console.ReadLine(), out int deptId);

                            var newEmployee = new Employee
                            {
                                Name = name,
                                Email = email,
                                DepartmentId = deptId
                            };
                            await employeeService.CreateAsync(newEmployee);
                            Console.WriteLine("İşçi uğurla əlavə edildi!");
                            break;

                        case "2":
                            Console.Write("Düzəliş ediləcək İşçinin ID-si: ");
                            if (int.TryParse(Console.ReadLine(), out int updateId))
                            {
                                var emp = await employeeService.GetByIdAsync(updateId);
                                if (emp != null)
                                {
                                    Console.Write("Yeni Ad: ");
                                    emp.Name = Console.ReadLine()!;
                                    Console.Write("Yeni Email: ");
                                    emp.Email = Console.ReadLine()!;

                                    await employeeService.UpdateAsync(emp);
                                    Console.WriteLine("İşçi uğurla yeniləndi!");
                                }
                                else Console.WriteLine("Bu ID-də işçi tapılmadı.");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "3":
                            Console.Write("Silinəcək İşçinin ID-si: ");
                            if (int.TryParse(Console.ReadLine(), out int deleteId))
                            {
                                employeeService.Delete(deleteId);
                                Console.WriteLine("İşçi silindi!");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "4":
                            Console.WriteLine("--- İşçilərin Siyahısı ---");
                            var employees = await employeeService.GetAllAsync();
                            foreach (var e in employees)
                            {
                                Console.WriteLine($"ID: {e.Id} | Ad: {e.Name} | Email: {e.Email} | DepartmentID: {e.DepartmentId}");
                            }
                            break;

                        case "5":
                            Console.Write("Axtarılan ID: ");
                            if (int.TryParse(Console.ReadLine(), out int searchId))
                            {
                                var e = await employeeService.GetByIdAsync(searchId);
                                if (e != null) Console.WriteLine($"ID: {e.Id} | Ad: {e.Name} | Email: {e.Email}");
                                else Console.WriteLine("Tapılmadı.");
                            }
                            else Console.WriteLine("Düzgün rəqəm daxil edin.");
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Yanlış seçim!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: {ex.Message}");
                }

                Console.WriteLine("\nDavam etmək üçün hər hansı bir düyməyə basın...");
                Console.ReadKey();
            }
        }
    }
}
