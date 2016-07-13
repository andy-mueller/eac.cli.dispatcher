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

            IEnumerable<CliRunCmd> expectedInfos = new List<CliRunCmd>{
                new CliRunCmd(executable: "program 1", parameters: "-T=value"),
                new CliRunCmd(executable: "program value", parameters: "-T=value2")
            };
            Assert.Equal(expectedInfos, processExecutor.startInfos, new CliRequestEqualityComparer());
        }

        class CliRequestEqualityComparer : EqualityComparer<CliRunCmd>
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

            public CliRunResult run(CliRunCmd request)
            {
                this.startInfos.Add(request);
                return new CliRunResult(output: "NONE");
            }
        }
    }
}
