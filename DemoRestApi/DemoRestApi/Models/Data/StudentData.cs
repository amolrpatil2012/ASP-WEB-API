namespace DemoRestApi.Models.Data
{
    public static class StudentData
    {
        public static List<StudentDTO> studentList = new List<StudentDTO>() {
                new StudentDTO { Id = 1, Name = "Amol" },
                new StudentDTO { Id = 2, Name = "Sachin" }
        };
    }
}
