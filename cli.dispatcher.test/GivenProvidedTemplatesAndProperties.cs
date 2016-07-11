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

            IEnumerable<CliRequest> expectedInfos = new List<CliRequest>{
                new CliRequest(executable: "program 1", parameter: "-T=value"),
                new CliRequest(executable: "program value", parameter: "-T=value2")
            };
            Assert.Equal(expectedInfos, processExecutor.startInfos, new CliRequestEqualityComparer());
        }

        class CliRequestEqualityComparer : EqualityComparer<CliRequest>
        {
            public override bool Equals(CliRequest x, CliRequest y)
            {
                return x.Parameter.Equals(y.Parameter) && x.Executable.Equals(y.Executable);
            }

            public override int GetHashCode(CliRequest x)
            {
                return 0;
            }
        }

        class ProcessExececutorSpy : ProcessOperator
        {
            internal readonly List<CliRequest> startInfos = new List<CliRequest>();

            public void run(CliRequest request)
            {
                this.startInfos.Add(request);
            }
        }
    }
}
