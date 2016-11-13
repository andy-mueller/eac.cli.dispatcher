using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;
using cli.dispatcher.usecase;

namespace cli.dispatcher.test
{
    public class GivenProvidedTemplatesAndProperties
    {
        [Fact]
        public void TemplatesAreInstantiated()
        {
            IEnumerable<CliTemplate> cliTemplates = new List<CliTemplate>{
                new CliTemplate(executable : "program 1", parameter: "-T=%key%"),
                new CliTemplate(executable : "program %key%", parameter: "-T=%key2%%cut%to be removed%cut% %no_cut%K=1234%no_cut%"),
            };
            Properties properties = Properties.of("key=value", "key2=value2", "no_cut=true");

            ProcessExececutorSpy processExecutor = new ProcessExececutorSpy();
            ExecuteMultipleProcessesUseCase uc = new ExecuteMultipleProcessesUseCase(processExecutor);
            IEnumerable<string> cutProps = new List<string>() { "cut", "no_cut"};
            uc.execute(cliTemplates, cutProps, properties);

            IEnumerable<CliRunCmd> expectedInfos = new List<CliRunCmd>{
                new CliRunCmd(executable: "program 1", parameters: "-T=value"),
                new CliRunCmd(executable: "program value", parameters: "-T=value2 K=1234")
            };
            Assert.Equal(expectedInfos, processExecutor.startInfos, new CliRunCmdEqualityComparer());
        }

        class CliRunCmdEqualityComparer : EqualityComparer<CliRunCmd>
        {
            public override bool Equals(CliRunCmd x, CliRunCmd y)
            {
                return x.Parameter.Equals(y.Parameter) && x.Executable.Equals(y.Executable);
            }

            public override int GetHashCode(CliRunCmd x)
            {
                return 0;
            }
        }

        class ProcessExececutorSpy : ProcessOperator
        {
            internal readonly List<CliRunCmd> startInfos = new List<CliRunCmd>();

            public CliRunResult Run(CliRunCmd request)
            {
                this.startInfos.Add(request);
                return new CliRunResult(output: "NONE");
            }
        }
    }
}
