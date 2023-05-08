using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementAPI.ViewModels.Response
{
    public class CountryResponse
    {
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public string CountryName { get; set; }
    }
}
