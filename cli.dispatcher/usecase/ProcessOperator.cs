namespace cli.dispatcher.usecase
{

    public interface ProcessOperator
    {
        CliRunResult Run(CliRunCmd request);
    }
}
