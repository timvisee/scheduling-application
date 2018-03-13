using System;
using System.Collections.Generic;
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
            // Create a user set
            HashSet<User> users = new HashSet<User>();

            // If this is an user instance, return it as a list
            if(this is User) {
                users.Add((User) this);
                return users;
            }

            // If not a user, the model must be a group instance
            if(!(this is Group))
                throw new Exception(
                    "invalid people type, must be a user or group"
                );

            // Get the group instance, build a user list
            Group group = (Group) this;

            // Loop through all group people, fold it's users
            foreach(var people in group.Peoples)
                users.UnionWith(
                    people.GetUsers()
                );

            return users;
        }
    }
}
