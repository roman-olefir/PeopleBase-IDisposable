using System;
using System.Collections.Generic;

namespace People_Base
{
    public class People
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age => BirthDay.GetAge();
        public List<Car> Cars { get; set; }
    }
}
