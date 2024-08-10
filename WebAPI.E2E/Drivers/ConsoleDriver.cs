using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WebAPI.E2E.Drivers;

public sealed class ConsoleDriver
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
        Console.WriteLine(_psi.Arguments);

        using var process = Process.Start(_psi);

        if (process == null)
        {
            return false;
        }
        
        await process.WaitForExitAsync();

        var output = await process.StandardOutput.ReadToEndAsync();
        Console.WriteLine(output);
        var error = await process.StandardError.ReadToEndAsync();
        Console.WriteLine(error);
        Console.WriteLine(process.ExitCode);

        return process.ExitCode == 0;
        
        // TODO: Convert this into a repository kind of class that makes the ids unique for each test and keeps track of the id the test uses vs. the id the repository uses so tests can run in parallel
    }
}
