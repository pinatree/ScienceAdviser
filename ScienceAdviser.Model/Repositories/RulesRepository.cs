/*
    Класс, выдающий данные из Excel файла в удобном виде. Детали считывания скрыты в классе RulesWithDetailReader,
    здесь буфферизация и выдача записей по критериям.
 */

using System.Linq;
using System.Collections.Generic;

using ScienceAdviser.ExcelModel.DataReaders;
using ScienceAdviser.IModel.DataTypes;
using ScienceAdviser.IModel.Repositories;

namespace ScienceAdviser.ExcelModel.Repositories
{
    public class RulesRepository : IRulesRepository
    {
        //Буффер
        private List<RuleWithDetail> _rules;
        
        public RulesRepository(string fileLocation)
        {
            RulesWithDetailReader excelReader = new RulesWithDetailReader(fileLocation, true);
            List<ExcelRuleWithDetailRecord> readedRecords = excelReader.ReadRecordsFromPath();
            _rules = excelReader.GetRuleWithDetailsFromRecords(readedRecords);
        }

        //Доступные группы деталей
        public IEnumerable<string> GetAvailableDetailGroups() =>
            _rules.
            Select(rule => rule.Condition.DetailGroup).
            Distinct();

        //Доступные подгруппы деталей (внутри некой группы)
        public IEnumerable<string> GetAvailableDetailSubgroups(string detailGroup) =>
            _rules.
            Where(rule => rule.Condition.DetailGroup == detailGroup).
            Select(rule => rule.Condition.DetailSubGroup).
            Distinct();

        //Доступные детали (внутри некой группы и подгруппы)
        public IEnumerable<string> GetAvailableDetails(string detailGroup, string detailSubgroup) =>
            _rules.
            Where(rule =>
                rule.Condition.DetailGroup == detailGroup &&
                rule.Condition.DetailSubGroup == detailSubgroup).
            Select(rule => rule.Condition.Detail).
            Distinct();

        //Получение списка правил, доступных для данной детали. В правиле хранится исходная деталь,
        //вероятность поломки другой детали и достоверность этого правила
        public IEnumerable<RuleWithDetail> GetAssociations(DetailDefect defect) =>
            _rules.
            Where(rule =>
                rule.Condition.Detail == defect.Detail &&
                rule.Condition.DetailSubGroup == defect.DetailSubGroup &&
                rule.Condition.DetailGroup == defect.DetailGroup);

    }
}
