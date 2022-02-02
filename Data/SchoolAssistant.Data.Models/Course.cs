namespace SchoolAssistant.Data.Models
{
    using System.Collections.Generic;

    using SchoolAssistant.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Lectures = new HashSet<Lecture>();
            this.Teachers = new HashSet<CourseTeacher>();
            this.Students = new HashSet<CourseStudent>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<CourseTeacher> Teachers { get; set; }

        public virtual ICollection<CourseStudent> Students { get; set; }
    }
}
