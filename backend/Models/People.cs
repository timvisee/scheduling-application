using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("people")]
    public abstract class People
    {
        [Key]
        public int PeopleId { get; set; }

        /// <summary>
        /// Get a set of users represented by this people model.
        /// </summary>
        public HashSet<User> GetUsers()
        {
            // If this is an user instance, return it as a list
            if(this is User)
                return new HashSet<User>() {
                    this
                };

            // If not a user, the model must be a group instance
            if(!(this is Group))
                throw new Exception(
                    "invalid people type, must be a user or group"
                );

            // Get the group instance, build a user list
            Group group = (Group) this;
            HashSet<User> users = new HashSet<User>();

            // Loop through all group people, fold it's users
            foreach(var people in group.Peoples)
                users.AddRange(
                    people.GetUsers()
                );
            
            return users;
        }
    }
}
