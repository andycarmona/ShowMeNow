// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorHandler.cs" company="Uni-app">
//   
// </copyright>
// <summary>
//   Keep track of errors in system
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ShowMeNow.API.Helpers
{
    using System;
    using System.Text;

    /// <summary>
    /// The internal error message handler 
    /// </summary>
    public class ErrorHandler
    {
        private static StringBuilder messageList;

        public static void InitializeMessageList()
        {
            messageList = new StringBuilder();
        }

        public static StringBuilder GetMessageList()
        {
            return messageList;
        }

        public static void AddNewMessages(string messageToAdd, string title)
        {
            messageList.AppendFormat("<<" + title + ">>");
            messageList.Append(messageToAdd);
            messageList.AppendFormat(Environment.NewLine); 
        }
    }
}