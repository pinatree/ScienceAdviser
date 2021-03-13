using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAdviser.Model;
using ScienceAdviser.Model.DataTypes;

namespace ScienceAdviser.EF_Model
{
    public class EFRepository : Repository
    {
        public override IQueryable<DetailGroup> DetailGroups { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IQueryable<DetailSubgroup> DetailSubgroups { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IQueryable<Detail> Details { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IQueryable<Association> Associations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
