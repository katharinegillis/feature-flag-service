namespace Console.Common;

public interface IExecutable
{
    public Task<IConsoleActionResult> Execute();
}