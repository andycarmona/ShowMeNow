// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HatesRelationShip.cs" company="uni-app">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.RelationModeles
{
    using Neo4jClient;

    public class HatesRelationship : Relationship<HatesData>, IRelationshipAllowingSourceNode<Person>,
    IRelationshipAllowingTargetNode<Person>
    {
        public static readonly string TypeKey = "HATES";

        public HatesRelationship(NodeReference targetNode, HatesData data)
            : base(targetNode, data)
        { }

        public override string RelationshipTypeKey
        {
            get { return TypeKey; }
        }
    }
}