using ScienceAdviser.IModel.Repositories;

namespace ScienceAdviser.IViewModel.Callers
{
    public interface IDetailGroupSelector
    {
        string SelectDetailGroup(IRulesRepository repository);
    }
}
