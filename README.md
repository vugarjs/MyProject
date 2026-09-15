# MyProject

SQL Server üzərində departament və işçi idarəetməsini həyata keçirən konsol tətbiqi. Layihə .NET 10, C# 14 və Entity Framework Core istifadə etməklə qatlı arxitektura prinsiplərinə uyğun qurulub.

Tətbiq vasitəsilə:

- Departament yaratmaq, yeniləmək, silmək və siyahıya almaq
- Departamenti ID və ada görə axtarmaq
- İşçi yaratmaq, yeniləmək, silmək və siyahıya almaq
- İşçini ID-yə görə axtarmaq
- Departament üzrə işçi limitinə nəzarət etmək
- Departament və işçi məlumatlarını SQL Server-də saxlamaq

> İstifadəçi interfeysi Azərbaycan dilindədir.

## Texnologiyalar

- **.NET 10** (`net10.0`)
- **C# 14**
- **Entity Framework Core 10.0.12**
- **Microsoft SQL Server**
- **SQL Server provider:** `Microsoft.EntityFrameworkCore.SqlServer` 10.0.12
- **EF Core migrations**
- **Nullable reference types** və implicit usings aktivdir

## Arxitektura

Layihə dörd əsas layihədən ibarətdir:

```text
MyProject.slnx
├── MyProject.Entity
│   └── Models
├── MyProject.DataAccess
│   ├── Configuration
│   ├── Context
│   └── Migrations
├── MyProject.Business
│   ├── Exceptions
│   └── Services
│       ├── Interfaces
│       └── Implementations
└── MyProject.Presentation
	└── Program.cs
```

### MyProject.Entity

Domen modellərini saxlayır:

- `BaseEntity` — `Id` sahəsi
- `AuditEntity` — `Id`, `CreatedDate` və `UpdatedDate` sahələri
- `Department` — ad, açıqlama, limit və işçilər kolleksiyası
- `Employee` — ad, email, `DepartmentId` və departament əlaqəsi

### MyProject.DataAccess

Verilənlər bazasına giriş qatıdır:

- `DepartmentContext` — `Departments` və `Employees` `DbSet`-lərini təqdim edir
- `DepartmentConfigration` — departament sahələrini və əlaqələri konfiqurasiya edir
- `EmployeeConfigration` — işçi sahələrini və əlaqələri konfiqurasiya edir
- `Migrations` — verilənlər bazası sxeminin versiyalaşdırılmış dəyişikliklərini saxlayır

`Department` və `Employee` arasında one-to-many əlaqə mövcuddur. Departament silindikdə həmin departamentə bağlı işçilər də cascade delete vasitəsilə silinir.

### MyProject.Business

Biznes məntiqini və servis müqavilələrini saxlayır:

- `IDepartmentService` / `DepartmentService`
- `IEmployeeService` / `EmployeeService`
- `NotFoundException` domen xətaları üçün nəzərdə tutulmuş exception sinfidir

Servislər asinxron CRUD əməliyyatları həyata keçirir və Entity Framework Core ilə işləyir.

### MyProject.Presentation

`Program.cs` vasitəsilə işləyən interaktiv konsol interfeysidir. Əsas menyudan departament və işçi menyularına keçmək mümkündür.

## İlkin şərtlər

Tətbiqi işlətməzdən əvvəl aşağıdakılar quraşdırılmalıdır:

1. **Visual Studio 2026** və ya .NET 10 SDK
2. **SQL Server Express** və ya uyğun SQL Server instansiyası
3. SQL Server üçün Windows Authentication icazəsi
4. `dotnet` CLI və ya Visual Studio daxilində EF Core migration alətləri

## Verilənlər bazasının sazlanması

`MyProject.DataAccess/Context/DepartmentContext.cs` faylında hazırda aşağıdakı bağlantı sətri istifadə olunur:

```text
Data Source=localhost\SQLEXPRESS;Database=CourseDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30
```

Əgər SQL Server instansiyanız fərqlidirsə, `Data Source` dəyərini dəyişin. Məsələn:

- `localhost\SQLEXPRESS`
- `(localdb)\MSSQLLocalDB`
- `SERVER_NAME\INSTANCE_NAME`

İstehsal mühitində bağlantı sətrini mənbə kodunda saxlamaq əvəzinə konfiqurasiya və ya təhlükəsiz secret storage istifadə etmək tövsiyə olunur.

## Quraşdırma və işə salma

Repository-ni klonlayın:

```bash
git clone https://github.com/vugarjs/MyProject.git
cd MyProject
```

Dependency-ləri bərpa edin və solution-u build edin:

```bash
dotnet restore
dotnet build
```

Mövcud migration-u verilənlər bazasına tətbiq edin:

```bash
dotnet ef database update --project MyProject.DataAccess --startup-project MyProject.Presentation
```

Tətbiqi başladın:

```bash
dotnet run --project MyProject.Presentation
```

Visual Studio istifadə edirsinizsə, `MyProject.Presentation` layihəsini startup project seçib **Start** düyməsinə basa bilərsiniz.

> EF Core CLI tapılmadıqda aşağıdakı əmrlə aləti quraşdıra bilərsiniz:
>
> ```bash
> dotnet tool install --global dotnet-ef --version 10.0.12
> ```

## Konsol menyusu

### Əsas menyu

| Seçim | Əməliyyat |
|---|---|
| `1` | Departamentləri idarə et |
| `2` | İşçiləri idarə et |
| `0` | Tətbiqdən çıx |

### Departament menyusu

| Seçim | Əməliyyat |
|---|---|
| `1` | Yeni departament əlavə et |
| `2` | Departamenti yenilə |
| `3` | Departamenti sil |
| `4` | Bütün departamentləri göstər |
| `5` | ID-yə görə axtar |
| `6` | Ada görə axtar |
| `0` | Əsas menyuya qayıt |

Departament yaradarkən ad, limit və açıqlama daxil edilir. Yeniləmə zamanı hazırkı tətbiq axınında departamentin adı dəyişdirilir.

### İşçi menyusu

| Seçim | Əməliyyat |
|---|---|
| `1` | Yeni işçi əlavə et |
| `2` | İşçini yenilə |
| `3` | İşçini sil |
| `4` | Bütün işçiləri göstər |
| `5` | ID-yə görə axtar |
| `0` | Əsas menyuya qayıt |

İşçi yaradarkən ad, email və mövcud departamentin ID-si daxil edilməlidir.

## Biznes qaydaları

- Departament adı eyni ID-yə malik olmayan başqa departamentdə təkrar istifadə edilə bilməz.
- İşçinin adı və email-i boş (`null`) ola bilməz.
- Yeni işçi əlavə edilərkən onun departamentinin limiti yoxlanılır.
- Departament limiti dolduqda yeni işçi əlavə etmək mümkün deyil.
- Departament və işçi silmə əməliyyatları obyekt tapılmadıqda `false` qaytarır.
- Departament və işçi modellərində `CreatedDate` və nullable `UpdatedDate` audit sahələri mövcuddur.
- Departament-işçi əlaqəsi məcburidir və `DepartmentId` xarici açardır.

## Verilənlər bazası modeli

### Departments

| Sütun | Tip | Qeyd |
|---|---|---|
| `Id` | `int` | Primary key, identity |
| `Name` | `nvarchar` | Məcburi, maksimum 100 simvol konfiqurasiya olunub |
| `Description` | `nvarchar` | Könüllü |
| `Limit` | `int` | Məcburi, default dəyər `0` konfiqurasiya olunub |
| `CreatedDate` | `datetime2` | Audit sahəsi |
| `UpdatedDate` | `datetime2` | Könüllü audit sahəsi |

### Employees

| Sütun | Tip | Qeyd |
|---|---|---|
| `Id` | `int` | Primary key, identity |
| `Name` | `nvarchar` | Məcburi, maksimum 100 simvol konfiqurasiya olunub |
| `Email` | `nvarchar` | Məcburi, maksimum 100 simvol konfiqurasiya olunub |
| `DepartmentId` | `int` | `Departments.Id` xarici açarı |
| `CreatedDate` | `datetime2` | Audit sahəsi |
| `UpdatedDate` | `datetime2` | Könüllü audit sahəsi |

## Migration idarəetməsi

Yeni model dəyişikliklərindən sonra migration yaratmaq üçün:

```bash
dotnet ef migrations add MigrationName --project MyProject.DataAccess --startup-project MyProject.Presentation
```

Migration-u verilənlər bazasına tətbiq etmək üçün:

```bash
dotnet ef database update --project MyProject.DataAccess --startup-project MyProject.Presentation
```

Son migration-u silmək üçün (yalnız migration hələ verilənlər bazasına tətbiq edilməyibsə):

```bash
dotnet ef migrations remove --project MyProject.DataAccess --startup-project MyProject.Presentation
```

## Layihə referensləri

```text
MyProject.Presentation  -> MyProject.Business
MyProject.Business      -> MyProject.DataAccess, MyProject.Entity
MyProject.DataAccess    -> MyProject.Entity
```

Business layihəsində EF Core Design və EF Core paketləri, DataAccess layihəsində isə SQL Server provider və EF Core Tools istifadə olunur.

## Məlum məhdudiyyətlər və inkişaf istiqamətləri

Bu repository-nin hazırkı vəziyyətində aşağıdakı məqamlar nəzərə alınmalıdır:

- `DepartmentContext` hər servis metodunda birbaşa yaradılır; dependency injection və `DbContextOptions` istifadəsi tətbiqi daha test edilə bilən edər.
- Bağlantı sətri kod daxilində yerləşir; onu `appsettings.json`, environment variable və ya secret manager-ə keçirmək məqsədəuyğundur.
- Servis metodlarının qaytarış tipləri nullable nəticələri tam ifadə etmir; tapılmayan obyektlər üçün nullable kontrakt və ya `NotFoundException` istifadə edilə bilər.
- Giriş yoxlamaları əsasən konsol səviyyəsində və sadə `null` yoxlamaları ilə məhdudlaşır. Email formatı, mənfi limit, boş ad və mövcud olmayan departament ID-si üçün əlavə validation faydalı olar.
- Departament adı üçün təkrarlanma yoxlaması tətbiq səviyyəsində aparılır. Paralel sorğular zamanı verilənlər bazasında unique constraint də əlavə etmək daha etibarlı yanaşmadır.
- Hazırda repository-də ayrıca unit və integration test layihəsi görünmür.
- Audit tarixlərinin avtomatik doldurulması üçün `SaveChangesAsync` override-ı və ya uyğun interceptor əlavə edilə bilər.
- `DepartmentConfigration` və `EmployeeConfigration` adlarında `Configuration` sözünün yazılışı standartlaşdırıla bilər.

## Contribution

1. Repository-ni fork edin.
2. Yeni branch yaradın:

   ```bash
   git checkout -b feature/your-change
   ```

3. Dəyişiklikləri edin və build-i yoxlayın:

   ```bash
   dotnet build
   ```

4. Commit yaradın və branch-i push edin.
5. Pull request açın və dəyişikliklərin məqsədini izah edin.

## License

Bu repository-də ayrıca `LICENSE` faylı təqdim edilməyib. Layihəni istifadə etməzdən əvvəl repository sahibindən lisenziya şərtlərini dəqiqləşdirin.
