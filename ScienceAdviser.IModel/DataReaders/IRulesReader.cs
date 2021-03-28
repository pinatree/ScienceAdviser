using ScienceAdviser.IModel.DataTypes;
using System.Collections.Generic;

namespace ScienceAdviser.IModel.DataReaders
{
    //В этом классе будем считывать данные из Excel, технические моменты - открытие файла, итд
    public interface IRulesReader
    {
        //Загружаем записи
        List<ExcelRuleWithDetailRecord> ReadRecordsFromPath();

        //Приводим их в удобный для обработки вид
        List<RuleWithDetail> GetRuleWithDetailsFromRecords(List<ExcelRuleWithDetailRecord> ruleRecords);
    }
}
