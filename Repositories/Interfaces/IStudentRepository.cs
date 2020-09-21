using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task<bool> Create(Student student);

        Task<bool> Update(Student student);
        Student Get(int search);
    }
}
