using ScienceAdviser.IModel.DataTypes;
using System.Collections.Generic;

namespace ScienceAdviser.IModel.Repositories
{
    //Интерфейс репозитория, из которого будем доставать данные во ViewModel. Технические моменты, такие как
    //считывание и работа с файлом Excel предполагается в реализации интерфейса IRulesReader
    public interface IRulesRepository
    {
        //Доступные группы деталей
        IEnumerable<string> GetAvailableDetailGroups();

        //Доступные подгруппы деталей
        IEnumerable<string> GetAvailableDetailSubgroups(string detailGroup);

        //Доступные детали
        IEnumerable<string> GetAvailableDetails(string detailGroup, string detailSubgroup);

        //Список правил, доступных для данной детали. В правиле хранится исходная деталь,
        //вероятность поломки другой детали и достоверность этого правила
        IEnumerable<RuleWithDetail> GetAssociations(DetailDefect defect);
    }
}
