namespace Console.Common;

public interface IExecutable
{
    public Task<int> Execute();
}