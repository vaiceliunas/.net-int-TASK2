using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens
{
    public static class Calculator
    {
        // todo: change this method to support cancellation token
        public static async Task<long> CalculateAsync(int n, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                long sum = 0;

                try
                {
                    for (var i = 0; i < n; i++)
                    {
                        // i + 1 is to allow 2147483647 (Max(Int32)) 
                        sum += (i + 1);
                        Thread.Sleep(10);
                        if (token.IsCancellationRequested)
                            token.ThrowIfCancellationRequested();
                    }
                }
                catch (System.Exception)
                {
                    return 0;
                }

                return sum;
            }, token);

        }
    }
}
