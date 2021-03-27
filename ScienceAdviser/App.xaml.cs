using Autofac;
using ScienceAdviser.ExcelModel.Repositories;
using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Callers;
using ScienceAdviser.IViewModel.Windows;
using ScienceAdviser.View.Selectors;
using ScienceAdviser.ViewModel.Windows;
using System.Windows;

namespace ScienceAdviser
{
    public partial class App : Application
    {
        public App()
        {
            WriteAutofacDependencies();
        }

        private void WriteAutofacDependencies()
        {
            var builder = new ContainerBuilder();

            //Хардкод, разобраться с этим. Вообще, сделать или фабрику, или чтобы передавались аргументы конструктора
            IRulesRepository repository = new RulesRepository(@"C:\Users\pinat\Desktop\Анализ дефектов\RULES_WITH_DETAILS.xlsx");
            var groupSelector = new MyGroupSelector();
            var subgroupSelector = new MySubgroupSelector();
            var detailSelector = new MyDetailSelector();
            IMainWindowViewModel mainVindowVM = new MainWindowViewModel(repository, groupSelector, subgroupSelector, detailSelector);

            //Синглтон, неприемлемо, разобраться с этим
            builder.RegisterInstance<IMainWindowViewModel>(mainVindowVM);

            DI.DIContainer.Container = builder.Build();
        }

        class MyGroupSelector : IDetailGroupSelector
        {
            public string SelectDetailGroup(IRulesRepository repository)
            {
                var window = new DetailGroupSelector(repository);
                window.ShowDialog();
                return window.FoundDetailGroup;
            }
        }

        class MySubgroupSelector : IDetailSubgroupSelector
        {
            public string SelectDetailSubgroup(IRulesRepository repository, string group)
            {
                var window = new DetailSubgroupSelector(repository, group);
                window.ShowDialog();
                return window.FoundDetailSubgroup;
            }
        }

        class MyDetailSelector : IDetailSelector
        {
            public string SelectDetailGroup(IRulesRepository repository, string group, string subGroup)
            {
                var window = new DetailSelector(repository, group, subGroup);
                window.ShowDialog();
                return window.FoundDetail;
            }
        }
    }
}
