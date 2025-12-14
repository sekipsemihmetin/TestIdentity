using TestIdentity.Aplication.DTOs.TestDTOs;
using TestIdentity.Domain.Entities;
using TestIdentity.Domain.Utilities.Concretes;
using TestIdentity.Domain.Utilities.Interfaces;
using TestIdentity.Infrastructure.Repositories.TestRepositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Aplication.Services.TestServices
{
    public class TestService : ITestService
    {
        private readonly ITestRepository _testRepository;
        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public async Task<IResult> CreateAsync(TestCreateDTO testCreateDTO)
        {
            var isExisting = await _testRepository.AnyAsync(x => x.Name.ToLower() == testCreateDTO.Name.ToLower());
            if (isExisting)
            {
                return new ErrorResult("Aynı isimde eklenemez");
            }
            var newTest = testCreateDTO.Adapt<Test>();
            await _testRepository.AddAsnyc(newTest);
            await _testRepository.SaveChangeAsync();
            return new SuccessResult("Ekleme Başarılı");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            // ID'ye göre test verisini bul / Find test data by ID
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                return new ErrorResult("Silinecek veri bulunamadı");
            }

            // Test verisini sil / Delete test data
            await _testRepository.DeleteAsync(test);
            await _testRepository.SaveChangeAsync();
            return new SuccessResult("Silme işlemi başarılı");
        }

        public async Task<IDataResult<List<TestListDTO>>> GetAllAsync()
        {
            var tests = await _testRepository.GetAllAsync();
            var testListDTO = tests.Adapt<List<TestListDTO>>();
            if (tests.Count() <= 0)
            {
                return new ErrorDataResult<List<TestListDTO>>(tests.Adapt<List<TestListDTO>>(), "Listelenecek veri yok");
            }
            return new SuccessDataResult<List<TestListDTO>>(testListDTO, "Listeleme Başarılı");
        }

        public async Task<IDataResult<TestDTO>> GetByIdAsync(Guid id)
        {
            // ID'ye göre test verisini bul / Find test data by ID
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                return new ErrorDataResult<TestDTO>(null, "Veri bulunamadı");
            }

            var testDTO = test.Adapt<TestDTO>();
            return new SuccessDataResult<TestDTO>(testDTO, "Veri getirme başarılı");
        }

        public async Task<IResult> UpdateAsync(TestUpdateDTO testUpdateDTO)
        {
            // ID'ye göre test verisini bul / Find test data by ID
            var test = await _testRepository.GetByIdAsync(testUpdateDTO.Id);
            if (test == null)
            {
                return new ErrorResult("Güncellenecek veri bulunamadı");
            }

            // Aynı isimde başka bir kayıt var mı kontrol et / Check if another record with the same name exists
            var isExisting = await _testRepository.AnyAsync(x => x.Name.ToLower() == testUpdateDTO.Name.ToLower() && x.Id != testUpdateDTO.Id);
            if (isExisting)
            {
                return new ErrorResult("Aynı isimde başka bir kayıt mevcut");
            }

            // Test verisini güncelle / Update test data
            test.Name = testUpdateDTO.Name;
            await _testRepository.UpdateAsync(test);
            await _testRepository.SaveChangeAsync();
            return new SuccessResult("Güncelleme işlemi başarılı");
        }
    }
}
