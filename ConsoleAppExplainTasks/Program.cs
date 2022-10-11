// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Random random = new Random();

void Work(int workerNumber, int duration)
{
    Console.WriteLine($"{workerNumber} started work at thread {Thread.CurrentThread.ManagedThreadId}");
    Thread.Sleep(duration);
    Console.WriteLine($"{workerNumber} finished work");
}

Task CreateAndRunWorkerTask(int workerNumber, int duration, CancellationToken cancellationToken)
{
    return Task.Run(() => { Work(workerNumber, duration); }, cancellationToken);
}

Stopwatch stopwatch = new Stopwatch();

Console.WriteLine($"Main started at thread {Thread.CurrentThread.ManagedThreadId}");

stopwatch.Start();

CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

Task t1 = CreateAndRunWorkerTask(1, 1000, cancellationTokenSource.Token);
Task t2 = CreateAndRunWorkerTask(2, 1500, cancellationTokenSource.Token);
//cancellationTokenSource.Cancel();
Task t3 = CreateAndRunWorkerTask(3, 3500, cancellationTokenSource.Token);

// Task t1 = Task.Run(() => { Work(1, 1000); }); //1000
// Task t2 = Task.Run(() => { Work(2, 1500); }); //1500
// Task t3 = Task.Run(() => { Work(3, 3500); }); //3500

// Work(1,1000);
// Work(2,1500);
// Work(3,3500);

// Task.WaitAll(t1, t2, t3);

stopwatch.Stop();

long duration = stopwatch.ElapsedMilliseconds;
Console.WriteLine($"all works finished, duration = {duration}");

Console.WriteLine($"Main finished at thread {Thread.CurrentThread.ManagedThreadId}");