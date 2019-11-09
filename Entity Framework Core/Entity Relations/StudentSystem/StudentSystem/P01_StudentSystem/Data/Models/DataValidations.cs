namespace P01_StudentSystem.Data.Models
{
    public static class DataValidations
    {
        public static class Student
        {
            public const int MaxNameLenght = 100;
            public const int PhoneFixedLenght = 10;
        }

        public static class Course
        {
            public const int MaxNameLenght = 80;
        }

        public static class Resource
        {
            public const int MaxNameLenght = 50;
        }
    }
}
