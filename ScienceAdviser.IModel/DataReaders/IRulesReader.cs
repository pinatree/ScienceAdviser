using ScienceAdviser.IModel.DataTypes;
using System.Collections.Generic;

namespace ScienceAdviser.IModel.DataReaders
{
    public interface IRulesReader
    {
        List<ExcelRuleWithDetailRecord> ReadRecordsFromPath();

        List<RuleWithDetail> GetRuleWithDetailsFromRecords(List<ExcelRuleWithDetailRecord> ruleRecords);
    }
}
