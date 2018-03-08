namespace backend.Types
{
    public enum Type
    {
        // A user type for a student.
        Student = 1,

        // A user type for a teacher.
        Teacher = 2,

        // A user type for an employee, that is not a teacher.
        Employee = 3,

        // A user type for other user types, that can't be defined by the other available types.
        Other = 0,
    }
}
