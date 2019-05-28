using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteService.API.Exceptions
{
    public class NoteNotFoundExeption:ApplicationException
    {
        public NoteNotFoundExeption(string message) : base(message) { }
    }
}
