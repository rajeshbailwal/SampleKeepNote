using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReminderService.API.Exceptions
{
    public class ReminderNotFoundException:ApplicationException
    {
        public ReminderNotFoundException() { }
        public ReminderNotFoundException(string message) : base(message) { }
    }
}
