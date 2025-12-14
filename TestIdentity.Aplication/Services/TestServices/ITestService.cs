using TestIdentity.Aplication.DTOs.TestDTOs;
using TestIdentity.Domain.Utilities.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIdentity.Aplication.Services.TestServices
{
    public interface ITestService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       Task<IDataResult<List<TestListDTO>>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCreateDTO"></param>
        /// <returns></returns>
       Task<IResult> CreateAsync(TestCreateDTO testCreateDTO);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testUpdateDTO"></param>
        /// <returns></returns>
       Task<IResult> UpdateAsync(TestUpdateDTO testUpdateDTO);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       Task<IDataResult<TestDTO>> GetByIdAsync(Guid id);
    }
}
