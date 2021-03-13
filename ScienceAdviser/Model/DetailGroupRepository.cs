using ScienceAdviser.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.Model
{
    public class DetailGroupRepository
    {
        public int PageSize = 50;

        public PaginatedData<DetailGroup> GetDetailsLikeStr(string comparing, int groupId) => throw new NotImplementedException();

        public DetailGroup GetDetailGroupWithId(int selfId, int groupId) => throw new NotImplementedException();
    }
}
