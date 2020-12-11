using System;

namespace MenuPlanner.Shared.models
{
    public class Comment
    {
        public Guid Id;
        public Menu Menu;
        public DateTime Date;
        public String Text;
        public User User;
    }
}