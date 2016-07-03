using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using cli.dispatcher;
using System.Linq;

namespace cli.dispatcher.test
{
    public class GivenProvidedTemplatesAndProperties
    {
        [Fact]
        public void templatesAreInstantiated() 
        {
            IEnumerable<CliTemplate> cliTemplates = new List<CliTemplate>{
                new CliTemplate(executable : "program 1", parameter: "-T=%key%"),
                new CliTemplate(executable : "program %key%", parameter: "-T=%key2%"),
            };
            Properties properties = Properties.of("key=value", "key2=value2");

            ProcessExececutorSpy processExecutor = new ProcessExececutorSpy();
            ExecuteMultipleProcessesUseCase uc = new ExecuteMultipleProcessesUseCase(processExecutor);
            uc.execute(cliTemplates, properties);

            IEnumerable<ProcessStartInfo> expectedInfos = new List<ProcessStartInfo>{
                new ProcessStartInfo(fileName: "program 1", arguments: "-T=value"),
                new ProcessStartInfo(fileName: "program value", arguments: "-T=value2")
            };
            Assert.Equal(expectedInfos, processExecutor.startInfos, new ProcessStartInfoEqualityComparer());
        }

        class ProcessStartInfoEqualityComparer : EqualityComparer<ProcessStartInfo>
        {
            public override bool Equals(ProcessStartInfo x, ProcessStartInfo y)
            {
                return x.Arguments.Equals(y.Arguments) && x.FileName.Equals(y.FileName);
            }

            public override int GetHashCode(ProcessStartInfo EqualityComparer)
            {
                throw new NotImplementedException();
            }
        }

        class ProcessExececutorSpy : ProcessExececutor
        {
            internal readonly List<ProcessStartInfo> startInfos = new List<ProcessStartInfo>();

            public void run(IEnumerable<ProcessStartInfo> startInfos)
            {
                this.startInfos.AddRange(startInfos);
            }
        }

        
    }

    public class ExecuteMultipleProcessesUseCase
    {
        private readonly ProcessExececutor processExecutor;

        public ExecuteMultipleProcessesUseCase(ProcessExececutor processExecutor)
        {
            this.processExecutor = processExecutor;
        }

        internal void execute(IEnumerable<CliTemplate> cliTemplates, Properties properties)
        {
            var startInfos = cliTemplates.Select(cli=>cli.apply(properties)).Select(inst=>new ProcessStartInfo(fileName: inst.Executable, arguments: inst.Parameter));

            processExecutor.run(startInfos);
        }
    }
    public interface ProcessExececutor
    {
        void run(IEnumerable<ProcessStartInfo> startInfos);
    }

}
