using ScienceAdviser.ExcelModel.DataReaders;
using ScienceAdviser.IModel.DataTypes;
using ScienceAdviser.IModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScienceAdviser.ExcelModel.Repositories
{
    public class RulesRepository : IRulesRepository
    {
        private List<RuleWithDetail> _rules;
        
        public RulesRepository(string fileLocation)
        {
            RulesWithDetailReader excelReader = new RulesWithDetailReader(fileLocation, true);
            List<ExcelRuleWithDetailRecord> readedRecords = excelReader.ReadRecordsFromPath();
            _rules = excelReader.GetRuleWithDetailsFromRecords(readedRecords);
        }

        public IEnumerable<string> GetAvailableDetailGroups() =>
            _rules.
            Select(rule => rule.Condition.DetailGroup).
            Distinct();

        public IEnumerable<string> GetAvailableDetailSubgroups(string detailGroup) =>
            _rules.
            Where(rule => rule.Condition.DetailGroup == detailGroup).
            Select(rule => rule.Condition.DetailSubGroup).
            Distinct();

        public IEnumerable<string> GetAvailableDetails(string detailGroup, string detailSubgroup) =>
            _rules.
            Where(rule =>
                rule.Condition.DetailGroup == detailGroup &&
                rule.Condition.DetailSubGroup == detailSubgroup).
            Select(rule => rule.Condition.Detail).
            Distinct();

        public IEnumerable<RuleWithDetail> GetAssociations(DetailDefect defect) =>
            _rules.
            Where(rule =>
                rule.Condition.Detail == defect.Detail &&
                rule.Condition.DetailSubGroup == defect.DetailSubGroup &&
                rule.Condition.DetailGroup == defect.DetailGroup);

    }
}
