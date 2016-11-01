using cli.dispatcher.usecase;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System;

namespace cli.dispatcher.test
{
    public class ProgramTest
    {
        [Fact]
        public void GivenTestParameter_ProgramGivesTestOutput()
        {
            var args = new List<string>(){"--test"};
            StringWriter tw = new StringWriter();
            Program program = new Program(args, tw, new ProcessOperatorSpy());

            program.run();

            Assert.StartsWith("testing...\r\n", tw.ToString());
        }
        [Fact]
        public void GiveConfigurationAndPoperties_DispatchesTo()
        {
            var args = new List<string>() { "key=1", "key2=2" };
            StringWriter tw = new StringWriter();
            ProcessOperatorSpy operatorSpy = new ProcessOperatorSpy();
            Program program = new Program(args, tw, operatorSpy);

            program.run();

            Assert.Collection(
                operatorSpy.calledWith,
                item => Assert.Equal(item, new CliRunCmd("program 1", "1 parameters"), new CmdComparer()),
                item => Assert.Equal(item, new CliRunCmd("program 2", "2 parameters"), new CmdComparer())
            );
        }
        class CmdComparer : IEqualityComparer<CliRunCmd>
        {
            public bool Equals(CliRunCmd x, CliRunCmd y)
            {
                return Object.Equals(x.Executable, y.Executable) && Object.Equals(x.Parameter, y.Parameter);
            }

            public int GetHashCode(CliRunCmd obj)
            {
                throw new NotImplementedException();
            }
        };
       

        private class ProcessOperatorSpy : ProcessOperator
        {
            internal List<CliRunCmd> calledWith = new List<CliRunCmd>();
            public CliRunResult Run(CliRunCmd request)
            {
                calledWith.Add(request);
                return null;
            }
        }
    }
}
