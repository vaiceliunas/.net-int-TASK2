/*
 * Изучите код данного приложения для расчета суммы целых чисел от 0 до N, а затем
 * измените код приложения таким образом, чтобы выполнялись следующие требования:
 * 1. Расчет должен производиться асинхронно.
 * 2. N задается пользователем из консоли. Пользователь вправе внести новую границу в процессе вычислений,
 * что должно привести к перезапуску расчета.
 * 3. При перезапуске расчета приложение должно продолжить работу без каких-либо сбоев.
 */

using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    class Program
    {
        private static Task _calculationTask;
        private static void Main()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
            Console.WriteLine("Calculating the sum of integers from 0 to N.");
            Console.WriteLine("Use 'q' key to exit...");
            Console.WriteLine();

            Console.WriteLine("Enter N: ");

            var input = Console.ReadLine();
            while (input != null && input.Trim().ToUpper() != "Q")
            {
                if (int.TryParse(input, out var n))
                {
                    if (_calculationTask != null &&
                        !_calculationTask.IsCompleted)
                    {
                        tokenSource.Cancel();
                        tokenSource = new CancellationTokenSource();
                        token = tokenSource.Token;
                    }
                    _calculationTask = CalculateSumAsync(n, token);
                }
                else
                {
                    Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                    Console.WriteLine("Enter N: ");
                }
                input = Console.ReadLine();
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        private static async Task CalculateSumAsync(int n, CancellationToken token)
        {
            var sumTask = Calculator.CalculateAsync(n, token);
            Console.WriteLine($"The task for {n} started... Enter N to cancel the request:");
            var sum = await sumTask;

            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Sum for {n} cancelled...");
            }else if (sumTask.IsCompletedSuccessfully)
            {
                Console.WriteLine($"Sum for {n} = {sum}.");
                Console.WriteLine();
                Console.WriteLine("Enter N: ");
            }


        }
    }
}