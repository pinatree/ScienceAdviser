using ScienceAdviser.IModel.Repositories;

namespace ScienceAdviser.IViewModel.Callers
{
    public interface IDetailSubgroupSelector
    {
        string SelectDetailSubgroup(IRulesRepository repository, string group);
    }
}
