using ScienceAdviser.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.Model
{
    public class AssociationRepository
    {
        public int PageSize = 50;

        public PaginatedData<Association> GetDetailsLikeStr(Detail detail) => throw new NotImplementedException();

        public Association GetDetailWithId(Detail detail) => throw new NotImplementedException();
    }
}
