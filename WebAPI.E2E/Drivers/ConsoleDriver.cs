using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace WebAPI.E2E.Drivers;

public sealed partial class ConsoleDriver
{
    private ProcessStartInfo _psi;
    
    public ConsoleDriver()
    {
        _psi = new ProcessStartInfo
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _psi.FileName = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/usr/bin/docker" : "CMD.exe";
    }

    public async Task<bool> CreateFeatureFlag(string id, bool enabled)
    {
        _psi.Arguments = $"compose exec featureflagservice console featureflag:create -i {id} -e {(enabled ? "true" : "false")}";

        using var process = Process.Start(_psi);

        if (process == null)
        {
            return false;
        }
        
        await process.WaitForExitAsync();

        return process.ExitCode == 0;
    }

    public async Task<bool> DeleteFeatureFlag(string id)
    {
        _psi.Arguments = $"compose exec featureflagservice console featureflag:delete -i {id}";

        using var process = Process.Start(_psi);

        if (process == null)
        {
            return false;
        }

        await process.WaitForExitAsync();

        return process.ExitCode == 0;
    }

    public async Task<string> ConfigShowDataSource()
    {
        _psi.Arguments = "compose exec featureflagservice console config:show datasource";

        using var process = Process.Start(_psi);

        if (process == null)
        {
            return "unknown";
        }

        await process.WaitForExitAsync();

        var output = await process.StandardOutput.ReadToEndAsync();

        return process.ExitCode != 0 ? "unknown" : DatasourceRegex().Match(output).Groups[1].Value;
    }

    [GeneratedRegex("^Datasource \"(.+?)\"$")]
    private static partial Regex DatasourceRegex();
}
