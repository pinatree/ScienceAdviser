using ScienceAdviser.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.Model
{
    public class DetailRepository
    {
        public int PageSize = 50;

        public PaginatedData<Detail> GetDetailsLikeStr(string comparing) => throw new NotImplementedException();

        public Detail GetDetailWithId(int id) => throw new NotImplementedException();
    }

    public class PaginatedData<T>
    {
        public int CurrentPage;
        public int TotalPages;

        public IEnumerable<T> Selected { get; set; }    
    }
}
