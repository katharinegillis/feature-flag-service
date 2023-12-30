namespace Console.Common;

public interface IRunnableWithOptions
{
    public void SetOptions(object options);

    public Task<int> Run();
}