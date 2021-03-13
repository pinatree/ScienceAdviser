using ScienceAdviser.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.Model
{
    public class DetailSubgroupRepository
    {
        public int PageSize = 50;

        public PaginatedData<DetailSubgroup> GetDetailsLikeStr(string comparing, int subGroupId, int groupId) => throw new NotImplementedException();

        public DetailSubgroup GetDetailSubgroupWithId(int selfId, int subGroupId, int groupId) => throw new NotImplementedException();
    }
}
