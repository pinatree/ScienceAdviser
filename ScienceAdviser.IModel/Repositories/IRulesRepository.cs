using ScienceAdviser.IModel.DataTypes;
using System.Collections.Generic;

namespace ScienceAdviser.IModel.Repositories
{
    public interface IRulesRepository
    {
        IEnumerable<string> GetAvailableDetailGroups();
        IEnumerable<string> GetAvailableDetailSubgroups(string detailGroup);
        IEnumerable<string> GetAvailableDetails(string detailGroup, string detailSubgroup);

        IEnumerable<RuleWithDetail> GetAssociations(DetailDefect defect);
    }
}
