using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScienceAdviser.Model.DataReaders.Excel.WithDetail.DataTypes;
using ScienceAdviser.Model.DataReaders.Excel.WithDetailDataReader;

namespace ScienceAdviser.Model.Repositories
{
    public class RulesForDetailRepository
    {
        private List<RuleWithDetail> _loadedRules;

        public RulesForDetailRepository(string path, bool with_header, string parser)
        {
            Init(path, with_header, parser);
        }

        public List<RuleWithDetail> GetAllRules() => _loadedRules;

        public List<RuleWithDetail> GetAssociations(DetailDefect comparison)
        {
            return _loadedRules.
                Where(rule =>
                    (rule.Condition.DetailGroup == comparison.DetailGroup) &&
                    (rule.Condition.DetailSubGroup == comparison.DetailSubGroup) &&
                    (rule.Condition.Detail == comparison.Detail)).
                    ToList();
        }

        public List<string> GetAvailableDetailGroups()
        {
            return _loadedRules.
                Select(rule => rule.Condition.DetailGroup).
                Distinct().ToList();
        }

        public List<string> GetAvailableDetailSubgroups(string detailGroup)
        {
            var x = _loadedRules.
                Where(rule => rule.Condition.DetailGroup == detailGroup).
                Select(rule => rule.Condition.DetailSubGroup).
                Distinct().
                ToList();

            return _loadedRules.
                Where(rule => rule.Condition.DetailGroup == detailGroup).
                Select(rule => rule.Condition.DetailSubGroup).
                Distinct().
                ToList();
        }

        public List<string> GetAvailableDetails(string detailGroup, string detailSubgroup)
        {
            return _loadedRules.
                Where(rule =>
                    (rule.Condition.DetailGroup == detailGroup) &&
                    (rule.Condition.DetailSubGroup == detailSubgroup)).
                Select(rule => rule.Condition.Detail).
                Distinct().
                ToList();
        }

        private void Init(string path, bool with_header, string parser)
        {
            RulesWithDetailReader reader = new RulesWithDetailReader(path, with_header, parser);
            List<ExcelRuleWithDetailRecord> excelFileRecords;

            excelFileRecords = reader.ReadRecordsFromPath();

            _loadedRules = reader.GetRuleWithDetailsFromRecords(excelFileRecords);
        }
    }
}
