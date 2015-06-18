namespace ShowMeNow.API.Models.RelationModeles
{
    using Neo4jClient;

    public class KnowsRelationship : Relationship, IRelationshipAllowingSourceNode<Person>,
       IRelationshipAllowingTargetNode<Person>
    {
        public static readonly string TypeKey = "KNOWS";

        public KnowsRelationship(NodeReference targetNode)
            : base(targetNode)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}