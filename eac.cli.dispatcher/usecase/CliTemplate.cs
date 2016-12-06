using System;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher.usecase
{
    public class CliTemplate
    {
        private readonly StringTemplate executable;
        private readonly StringTemplate parameter;

        public CliTemplate(string executable, string parameter)
        {
            this.parameter = StringTemplate.of(parameter);
            this.executable = StringTemplate.of(executable);
        }

        class Templates {
            internal StringTemplate executable;
            internal StringTemplate parameter;

            public Templates(StringTemplate executable, StringTemplate parameter)
            {
                this.executable = executable;
                this.parameter = parameter;
            }

            public void Cutout(string cutmark)
            {
                executable = executable.CutOut(cutmark);
                parameter = parameter.CutOut(cutmark);
            }

            internal void Cut(string cutmark)
            {
                executable = executable.Cut(cutmark );
                parameter = parameter.Cut(cutmark );
            }
        }
        public CliRunCmd apply(CompressionToolProperties properties, IEnumerable<string> cutProps)
        {
            Templates templates = new Templates(executable, parameter);
            cutProps.Where(c => !properties.Contains(c)).ToList().ForEach(templates.Cutout);
            cutProps.Where(c =>  properties.Contains(c)).ToList().ForEach(templates.Cut);
            return new CliRunCmd(executable: templates.executable.Replace(properties), parameters: templates.parameter.Replace(properties));
        }

    }
}