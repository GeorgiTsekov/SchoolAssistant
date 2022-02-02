namespace SchoolAssistant.Data.Models
{
    using System.Collections.Generic;

    using SchoolAssistant.Data.Common.Models;

    public class Department : BaseDeletableModel<int>
    {
        public Department()
        {
            this.Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
