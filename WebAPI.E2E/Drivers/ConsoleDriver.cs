using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WebAPI.E2E.Drivers;

public sealed partial class ConsoleDriver
{
    private readonly ProcessStartInfo _psi = new()
    {
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true,
        FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "docker" : "CMD.exe"
    };

    public async Task<bool> CreateFeatureFlag(string id, bool enabled)
    {
        _psi.Arguments =
            $"compose exec featureflagservice console featureflag:create -i {id} -e {(enabled ? "true" : "false")}";

        using var process = Process.Start(_psi);

        if (process == null) return false;

        var error = process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
        {
            Console.WriteLine(error);
        }

        return process.ExitCode == 0;
    }

    public async Task DeleteFeatureFlag(string id)
    {
        _psi.Arguments = $"compose exec featureflagservice console featureflag:delete -i {id}";

        using var process = Process.Start(_psi);

        if (process == null) return;

        await process.WaitForExitAsync();
    }

    public async Task<string> ConfigShowDataSource()
    {
        _psi.Arguments = "compose exec featureflagservice console config:show datasource";

        using var process = Process.Start(_psi);

        if (process == null) return "unknown";

        await process.WaitForExitAsync();

        var output = await process.StandardOutput.ReadToEndAsync();

        return process.ExitCode != 0 ? "unknown" : DatasourceRegex().Match(output).Groups[1].Value;
    }

    [GeneratedRegex("^Datasource \"(.+?)\"$")]
    private static partial Regex DatasourceRegex();
}