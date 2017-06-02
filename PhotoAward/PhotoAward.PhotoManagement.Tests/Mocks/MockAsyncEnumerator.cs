using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.PhotoManagement.Tests.Mocks
{
    /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MockAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public MockAsyncEnumerator(IEnumerator<T> enumerator)
        {
            this._enumerator = enumerator;
        }

        public T Current
        {
            get { return _enumerator.Current; }
        }

        public void Dispose()
        {
            _enumerator.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_enumerator.MoveNext());
        }

        public void Reset()
        {
            _enumerator.Reset();
        }
    }
}