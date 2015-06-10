namespace ShowMeNow.API.Models.RelationModeles
{
    using Neo4jClient;

    public class HatesData
    {
        public string Reason { get; set; }

        public HatesData()
        { }

        public HatesData(string reason)
        {
            this.Reason = reason;
        }
    }
}