namespace ScienceAdviser.IModel.DataTypes
{
    public class RuleWithDetail
    {
        public DetailDefect Condition { get; set; }

        public DetailDefect Consequence { get; set; }

        public float Reliability { get; set; }

        public float SupportCount { get; set; }
    }

}
