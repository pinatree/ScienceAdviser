namespace ScienceAdviser.IModel.DataTypes
{
    //Правило, считанное из файла Excel
    public class RuleWithDetail
    {
        //Сломавшаяся деталь
        public DetailDefect Condition { get; set; }

        //Деталь, которая может сломаться
        public DetailDefect Consequence { get; set; }

        //Шанс, что она сломается
        public float Reliability { get; set; }

        //На основе скольки записей сделан такой вывод (на самом деле, не уверен, что именно отображает этот пункт,
        //в программе пока нет вариантов использования)
        public float SupportCount { get; set; }
    }

}
