namespace ScienceAdviser.IModel.DataTypes
{
    //Запись из Excel-файла
    public class ExcelRuleWithDetailRecord
    {
        public int Id { get; set; }

        public int ElementNum { get; set; }

        public DetailDefect Condition { get; set; }

        public DetailDefect Consequence { get; set; }

        public int SupportCount { get; set; }

        public float SupportPerc { get; set; }

        public float Reliability { get; set; }

        public float Lift { get; set; }
    }
}
