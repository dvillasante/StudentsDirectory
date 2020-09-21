using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentsManager
    {
        Task<Student> Create(Student student);

        Task<Student> Update(Student student);

        Student Get(int id);

        void LoadTestData();

    }
}
