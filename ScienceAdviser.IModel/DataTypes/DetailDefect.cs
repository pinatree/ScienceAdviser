namespace ScienceAdviser.IModel.DataTypes
{
    //Дефект детали: группа + подгруппа + собственно, деталь. Деталь сама по себе не гарантирует уникальности записи.
    public class DetailDefect
    {
        public string DetailGroup { get; set; }

        public string DetailSubGroup { get; set; }

        public string Detail { get; set; }
    }
}
