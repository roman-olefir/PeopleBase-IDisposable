using System;

namespace People_Base
{
    public class People
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age => BirthDay.GetAge();
        public Car Car { get; set; }
    }
}
