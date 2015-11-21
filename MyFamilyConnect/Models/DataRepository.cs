using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFamilyConnect.Models
{
    public class DataRepository
    {
        private DataContext context;
        public DataRepository(DataContext _context)
        {
            context = _context;
        }
    }
}
