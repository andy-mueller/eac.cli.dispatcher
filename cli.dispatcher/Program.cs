using System.Collections.Generic;
using System.Configuration;

namespace cli.dispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program(new List<string>(args));
        }
        private readonly IList<string> list;
                
        public Program(IList<string> list)
        {
            this.list = list;
        }

       
    }
}
