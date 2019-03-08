using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace FileWatcherDemo
{
    public class FileWatcherService : IHostedService, IDisposable
    {
        private static readonly FileSystemWatcher Watcher = new FileSystemWatcher();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            RunFileWatcher();
            return Task.CompletedTask;
        }

        private void RunFileWatcher()
        {
            Console.WriteLine("start watching...");
            Watcher.Path = @"D:\Temp\Watcher";
            Watcher.Filter = "*.txt"; // depend on what kind of format do you to process
            Watcher.Created += OnCreated;
            Watcher.Deleted += OnDeleted;
            Watcher.Renamed += OnRenamed;
            Watcher.Changed += OnChanged;
            Watcher.EnableRaisingEvents = true;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Service stop");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine("Watcher Dispose");
            Watcher.Dispose();
        }

        static void OnChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                Watcher.Changed -= OnChanged;
                Watcher.EnableRaisingEvents = false;
                Console.WriteLine($"File Changed. Name: {e.Name}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Watcher.Changed += OnChanged;
                Watcher.EnableRaisingEvents = true;
            }
        }

        private static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"rename {e.OldName} to {e.Name}");
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine($"delete {e.Name}");
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                Watcher.Changed -= OnCreated;
                Watcher.EnableRaisingEvents = false;
                Console.WriteLine($"File Create => Name: {e.Name}");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Watcher.Changed += OnCreated;
                Watcher.EnableRaisingEvents = true;
                Thread.Sleep(5000);
                File.Delete(e.FullPath);
            }
        }

    }
}
