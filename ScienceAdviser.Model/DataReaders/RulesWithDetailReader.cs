/*
    Класс для считывания данных из файла формата Excel в структуры типа RuleWithDetail (соотв. колонкам Excel-файла).
    Структуру типа RuleWithDetail можно привести к типу RuleWithDetail, убрав ненужные данные.
 */

using System;
using System.IO;
using System.Collections.Generic;

//Сторонняя библиотека для работы с Excel, версия для некоммерческого использования
using OfficeOpenXml;

using ScienceAdviser.IModel.DataTypes;

namespace ScienceAdviser.ExcelModel.DataReaders
{
    public class RulesWithDetailReader
    {
        //Данные о детали хранятся в одной строке, раздеоенной неким символом.
        const string DEFAULT_PARSER = "-!$-";
        public string Parser { get; }

        //Расположение файла
        public string FileLocation { get; }

        //Если есть заголовок, то первую строку пропускаем
        public bool WithHeader { get; }

        public RulesWithDetailReader(string readPath, bool withHeader, string parser = DEFAULT_PARSER)
        {
            this.FileLocation = readPath;
            this.WithHeader = withHeader;
            this.Parser = parser;
        }

        //Получаем записи в "сыром" виде
        public List<ExcelRuleWithDetailRecord> ReadRecordsFromPath()
        {
            if(File.Exists(FileLocation) == false)
                throw new FileNotFoundException();

            FileStream fs = new FileStream(FileLocation, FileMode.Open, FileAccess.Read);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage(fs);

            //Берем первый лист
            var workSheet = package.Workbook.Worksheets[0];

            List<ExcelRuleWithDetailRecord> result = new List<ExcelRuleWithDetailRecord>(workSheet.Dimension.End.Row - 1);

            int start = WithHeader ? 2 : 1;

            //Проходимся по всем строкам
            for (var rowNumber = start; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                string idStr = workSheet.Cells[rowNumber, 1].Value.ToString().Trim();
                int id;
                if (int.TryParse(idStr, out id) == false)
                    throw new Exception();

                string elementNumStr = workSheet.Cells[rowNumber, 2].Value.ToString().Trim();
                int elementNum;
                if (int.TryParse(elementNumStr, out elementNum) == false)
                    throw new Exception();

                string conditionStr = workSheet.Cells[rowNumber, 3].Value.ToString().Trim();
                DetailDefect condition = RulesWithDetailHelper.DetailInfoFromStr(conditionStr, Parser);

                string consequenceStr = workSheet.Cells[rowNumber, 4].Value.ToString().Trim();
                DetailDefect consequence = RulesWithDetailHelper.DetailInfoFromStr(consequenceStr, Parser);

                string supportCountStr = workSheet.Cells[rowNumber, 5].Value.ToString().Trim();
                int supportCount;
                if (int.TryParse(supportCountStr, out supportCount) == false)
                    throw new Exception();

                string supportPercStr = workSheet.Cells[rowNumber, 6].Value.ToString().Trim();
                float supportPerc;
                if (float.TryParse(supportPercStr, out supportPerc) == false)
                    throw new Exception();

                string reliabilityStr = workSheet.Cells[rowNumber, 7].Value.ToString().Trim();
                float reliability;
                if (float.TryParse(reliabilityStr, out reliability) == false)
                    throw new Exception();

                string liftStr = workSheet.Cells[rowNumber, 1].Value.ToString().Trim();
                float lift;
                if (float.TryParse(liftStr, out lift) == false)
                    throw new Exception();

                ExcelRuleWithDetailRecord newRecord = new ExcelRuleWithDetailRecord()
                {
                    Id = id,
                    ElementNum = elementNum,
                    Condition = condition,
                    Consequence = consequence,
                    SupportCount = supportCount,
                    SupportPerc = supportPerc,
                    Reliability = reliability,
                    Lift = lift
                };

                result.Add(newRecord);
            }

            return result;
        }

        //Конвертируем записи у удобный для использования формат
        public List<RuleWithDetail> GetRuleWithDetailsFromRecords(List<ExcelRuleWithDetailRecord> ruleRecords)
        {
            List<RuleWithDetail> result = new List<RuleWithDetail>(ruleRecords.Count);

            foreach(ExcelRuleWithDetailRecord rule in ruleRecords)
            {
                RuleWithDetail newRule = RulesWithDetailHelper.GetRuleFromExcelRecord(rule);

                result.Add(newRule);
            }

            return result;
        }
    }

    //Helper с конвертерами
    public static class RulesWithDetailHelper
    {
        public static DetailDefect DetailInfoFromStr(string source, string parser)
        {
            string[] splitted = source.Split(new String[] { parser }, StringSplitOptions.None);

            DetailDefect detailInfo = new DetailDefect()
            {
                DetailGroup = splitted[0].Trim(),
                DetailSubGroup = splitted[1].Trim(),
                Detail = splitted[2].Trim(),
            };

            return detailInfo;
        }

        public static RuleWithDetail GetRuleFromExcelRecord(ExcelRuleWithDetailRecord rule)
        {
            return new RuleWithDetail()
            {
                Condition = rule.Condition,
                Reliability = rule.Reliability,
                Consequence = rule.Consequence,
                SupportCount = rule.SupportCount
            };
        }
    }
}
