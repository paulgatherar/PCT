using System;
using System.Linq;

namespace SPA
{
    public class NameReverseService : INameReverseService
    {
        public string Reverse(string name)
        {
            return new string(name.Reverse().ToArray());
        }
    }
}
