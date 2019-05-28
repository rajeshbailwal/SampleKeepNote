using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderService.API.Exceptions
{
    public class ReminderNotCreatedException:ApplicationException
    {
        public ReminderNotCreatedException() { }
        public ReminderNotCreatedException(string message) : base(message) { }
    }
}
