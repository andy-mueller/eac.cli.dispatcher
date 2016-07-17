using System.Collections.Generic;
using System.IO;
using Xunit;

namespace cli.dispatcher.test
{
    public class ProgramTest
    {
        [Fact]
        public void GivenTestParameter_ProgramGivesTestOutput()
        {
            var args = new List<string>(){"--test"};
            StringWriter tw = new StringWriter();
            Program program = new Program(args, tw);

            program.run();

            Assert.Equal("testing...\r\n", tw.ToString());
        }
    }
}
