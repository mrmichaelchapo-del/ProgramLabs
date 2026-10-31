// MainLoader.cs
// ProgramLabs - Entry Point 🚀
// Raw .cs, no .csproj, pure vibes 😤

using System;
using System.IO;
using System.Diagnostics;

namespace ProgramLabs
{
    public static class MainLoader
    {
        public const string AppName = "ProgramLabs";
        public const string Version = "v0.5";
        public const string VersionPlaceholder = "{version}";

        public static int Main(string[] args)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(Format($"  🧪 {AppName} {VersionPlaceholder} 🚀"));
            Console.WriteLine("=================================");

            // 1. Check .NET SDK 📦
            if (!CheckDotNetSdk())
            {
                Console.WriteLine("❌ .NET SDK not found bro!");
                Console.WriteLine("📦 Install it from https://dotnet.microsoft.com/download");
                return 1;
            }
            Console.WriteLine("✅ .NET SDK found!");

            // 2. Check for required folders/files 📁
            if (!CheckEnvironment())
            {
                Console.WriteLine("❌ Environment check failed!");
                return 1;
            }
            Console.WriteLine("✅ Environment OK!");

            // 3. Boot the app 🔥
            Console.WriteLine("🚀 Launching ProgramLabs...");
            try
            {
                AppBoot.Start(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Crash bro: {ex.Message}");
                return 1;
            }

            Console.WriteLine("👋 ProgramLabs closed. Later bro!");
            return 0;
        }

        // Replaces {version} with the real version ✅
        public static string Format(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Replace(VersionPlaceholder, Version);
        }

        // Checks .NET SDK is installed ✅
        private static bool CheckDotNetSdk()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var proc = Process.Start(psi);
                if (proc == null) return false;

                string output = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                Console.WriteLine($"   🔍 dotnet version: {output.Trim()}");
                return proc.ExitCode == 0 && !string.IsNullOrWhiteSpace(output);
            }
            catch
            {
                return false;
            }
        }

        // Checks folder structure 📁
        private static bool CheckEnvironment()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string[] requiredDirs = { "Nodes", "Projects", "Logs" };
            foreach (var dir in requiredDirs)
            {
                string path = Path.Combine(baseDir, dir);
                if (!Directory.Exists(path))
                {
                    Console.WriteLine($"   📁 Creating missing folder: {dir}");
                    Directory.CreateDirectory(path);
                }
            }

            return true;
        }
    }
}