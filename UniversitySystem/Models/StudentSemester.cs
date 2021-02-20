namespace UniversitySystem.Models
{
    public class StudentSemester
    {
        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int SemesterId { get; set; }

        public Semester Semester { get; set; }
    }
}