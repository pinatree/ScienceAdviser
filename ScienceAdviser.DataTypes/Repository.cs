using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScienceAdviser.Model.DataTypes;

namespace ScienceAdviser.Model
{
    public class Repository
    {
        public IQueryable<DetailGroup> DetailGroups { get; set; }

        public IQueryable<DetailSubgroup> DetailSubgroups { get; set; }

        public IQueryable<Detail> Details { get; set; }

        public IQueryable<Association> Associations { get; set; }
    }
}
