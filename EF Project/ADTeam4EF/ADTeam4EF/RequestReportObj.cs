using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADTeam4EF
{
    public class RequestReportObj
    {
        public string CategoryName { get; set; }
        public string DepartmentName { get; set; }

        public int FirstMonth { get; set; }
        public int SecondMonth { get; set; }
        public int ThirdMonth { get; set; }
    }
}
