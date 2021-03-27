using ScienceAdviser.IModel.Repositories;

namespace ScienceAdviser.IViewModel.Callers
{
    public interface IDetailSelector
    {
        string SelectDetailGroup(IRulesRepository repository, string group, string subGroup);
    }
}
