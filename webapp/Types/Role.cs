namespace webapp.Types
{
    public enum Role
    {
        // A role that doesn't allow users to make modifications.
        //
        // Creating or managing events is not allowed with this role.
        ReadOnly = 1,
        
        // A basic role.
        // 
        // A user with this role can:
        // - everything a ReadOnly user can
        // - create a new event, with approval
        // - modify events the user is an organizer of
        Basic = 2,
        
        // An elevated permissions role.
        //
        // A user with this role can:
        // - everything a Basic user can
        // - create a new event, without approval
        // - approve new events from basic users
        // - modify any event
        // - manage user groups
        Elevated = 3,
        
        // An administrator role.
        // A user with this role will be able to perform any action.
        Admin = 4,
    }
}