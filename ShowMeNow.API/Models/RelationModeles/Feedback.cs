// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="ui-app.se">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.RelationModeles
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public int positivePunctuation { get; set; }

        public int negativePunctuation { get; set; }

        public string Comments { get; set; }
    }
}