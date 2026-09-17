using MyProject.Business.Services.Interfaces;
using MyProject.DataAccess.Repositories.Interfaces;
using MyProject.Entity.Models;

namespace MyProject.Business.Services.Implementations;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task AddAsync(Department entity)
    {
        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new Exception("Name boş ola bilməz!");
        }

        await _departmentRepository.AddAsync(entity);
    }

    public async Task DeleteAsync(int? id)
    {
        if (id is null)
        {
            throw new Exception("ID boş ola bilməz!");
        }

        var department = await _departmentRepository.GetAsync(d => d.Id == id.Value);

        if (department is null)
        {
            throw new Exception("Departament tapılmadı!");
        }
        var entity = await _departmentRepository.GetAsync(d => d.Id == id.Value);
        // Remove metodu sinxrondur (await istifadə olunmur)
        _departmentRepository.Remove(entity!);
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _departmentRepository.GetAllAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _departmentRepository.GetAsync(d => d.Id == id);
    }

    public Task<Department?> GetByNameAsync(string name)
    {
        return _departmentRepository.GetByNameAsync(name);
    }

    public void Remove(int? id)
    {
        if (id is null)
        {
            throw new Exception("ID boş ola bilməz!");
        }
        var entity = _departmentRepository.GetAsync(d => d.Id == id.Value).Result;

        _departmentRepository.Remove(entity!);
    }

    public async Task<Department> UpdateAsync(Department entity)
    {
        if (string.IsNullOrEmpty(entity.Name))
        {
            throw new Exception("Name boş ola bilməz!");
        }

        // Update metodu sinxrondur (await istifadə olunmur)
        _departmentRepository.Update(entity);
        return entity;
    }
}