using System.Collections.Generic;
using Microsoft.ServiceFabric.Data;

namespace PhotoAward.PhotoManagement.Tests.Mocks
{
   /// <summary>
   /// 
   /// </summary>
   /// <typeparam name="T"></typeparam>
    internal class MockAsyncEnumerable<T> : IAsyncEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;

        public MockAsyncEnumerable(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new MockAsyncEnumerator<T>(enumerable.GetEnumerator());
        }
    }
}