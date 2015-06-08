namespace AngularJSAuthentication.API.Services
{
    using System.Collections.Generic;

    using ShowMeNow.API.Models;

    public interface IPlacesService
    {
        IEnumerable<Person> GetAllPeopleByLabel();

        Person GetAPerson(int personId);

        List<Person> GetAllFriends(int personId);

    }
}
