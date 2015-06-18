// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feedback.cs" company="ui-app.se">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Models.Dto
{
    public class FeedbackDto
    {
        public int FeedbackId { get; set; }

        public int positivePunctuation { get; set; }

        public int negativePunctuation { get; set; }

        public string Comments { get; set; }
    }
}