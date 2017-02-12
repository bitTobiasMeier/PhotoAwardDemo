using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Fabric;
using System.Fabric.Description;
using System.Fabric.Health;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Data.Notifications;

namespace PhotoAward.PhotoManagement.Tests.Mocks
{
    public class MockCodePackageActivationContext : ICodePackageActivationContext
    {
        public string ApplicationName
        {
            get { throw new NotImplementedException(); }
        }

        public string ApplicationTypeName
        {
            get { throw new NotImplementedException(); }
        }

        public string CodePackageName
        {
            get { throw new NotImplementedException(); }
        }

        public string CodePackageVersion
        {
            get { throw new NotImplementedException(); }
        }

        public string ContextId
        {
            get { throw new NotImplementedException(); }
        }

        public string LogDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public string TempDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public string WorkDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ApplicationPrincipalsDescription GetApplicationPrincipals()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetCodePackageNames()
        {
            throw new NotImplementedException();
        }

        public CodePackage GetCodePackageObject(string packageName)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetConfigurationPackageNames()
        {
            throw new NotImplementedException();
        }

        public ConfigurationPackage GetConfigurationPackageObject(string packageName)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetDataPackageNames()
        {
            throw new NotImplementedException();
        }

        public DataPackage GetDataPackageObject(string packageName)
        {
            throw new NotImplementedException();
        }

        public EndpointResourceDescription GetEndpoint(string endpointName)
        {
            throw new NotImplementedException();
        }

        public KeyedCollection<string, EndpointResourceDescription> GetEndpoints()
        {
            throw new NotImplementedException();
        }

        public KeyedCollection<string, ServiceGroupTypeDescription> GetServiceGroupTypes()
        {
            throw new NotImplementedException();
        }

        public string GetServiceManifestName()
        {
            throw new NotImplementedException();
        }

        public string GetServiceManifestVersion()
        {
            throw new NotImplementedException();
        }

        public KeyedCollection<string, ServiceTypeDescription> GetServiceTypes()
        {
            throw new NotImplementedException();
        }

        public void ReportApplicationHealth(HealthInformation healthInfo)
        {
            throw new NotImplementedException();
        }

        public void ReportDeployedApplicationHealth(HealthInformation healthInfo)
        {
            throw new NotImplementedException();
        }

        public void ReportDeployedServicePackageHealth(HealthInformation healthInfo)
        {
            throw new NotImplementedException();
        }
#pragma warning disable 0067
        public event EventHandler<PackageAddedEventArgs<CodePackage>> CodePackageAddedEvent;

        public event EventHandler<PackageModifiedEventArgs<CodePackage>> CodePackageModifiedEvent;

        public event EventHandler<PackageRemovedEventArgs<CodePackage>> CodePackageRemovedEvent;

        public event EventHandler<PackageAddedEventArgs<ConfigurationPackage>> ConfigurationPackageAddedEvent;

        public event EventHandler<PackageModifiedEventArgs<ConfigurationPackage>> ConfigurationPackageModifiedEvent;

        public event EventHandler<PackageRemovedEventArgs<ConfigurationPackage>> ConfigurationPackageRemovedEvent;

        public event EventHandler<PackageAddedEventArgs<DataPackage>> DataPackageAddedEvent;

        public event EventHandler<PackageModifiedEventArgs<DataPackage>> DataPackageModifiedEvent;

        public event EventHandler<PackageRemovedEventArgs<DataPackage>> DataPackageRemovedEvent;

#pragma warning restore 0067
    }

    public class MockReliableDictionary<TKey, TValue> : IReliableDictionary<TKey, TValue>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new ConcurrentDictionary<TKey, TValue>();

#pragma warning disable 0067
        public event EventHandler<NotifyDictionaryChangedEventArgs<TKey, TValue>> DictionaryChanged;
#pragma warning restore 0067

        public Uri Name { get; set; }

        public Func<IReliableDictionary<TKey, TValue>, NotifyDictionaryRebuildEventArgs<TKey, TValue>, Task>
            RebuildNotificationAsyncCallback
        {
            set { throw new NotImplementedException(); }
        }

        public Task AddAsync(ITransaction tx, TKey key, TValue value)
        {
            if (!_dictionary.TryAdd(key, value))
                throw new InvalidOperationException("key already exists: " + key);


            return Task.FromResult(true);
        }

        public Task AddAsync(ITransaction tx, TKey key, TValue value, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            if (!_dictionary.TryAdd(key, value))
                throw new InvalidOperationException("key already exists: " + key);

            return Task.FromResult(true);
        }

        public Task<TValue> AddOrUpdateAsync(ITransaction tx, TKey key, Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            return Task.FromResult(_dictionary.AddOrUpdate(key, addValueFactory, updateValueFactory));
        }

        public Task<TValue> AddOrUpdateAsync(ITransaction tx, TKey key, TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            return Task.FromResult(_dictionary.AddOrUpdate(key, addValue, updateValueFactory));
        }

        public Task<TValue> AddOrUpdateAsync(
            ITransaction tx, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory,
            TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.AddOrUpdate(key, addValueFactory, updateValueFactory));
        }

        public Task<TValue> AddOrUpdateAsync(
            ITransaction tx, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.AddOrUpdate(key, addValue, updateValueFactory));
        }

        public Task ClearAsync()
        {
            _dictionary.Clear();

            return Task.FromResult(true);
        }

        public Task ClearAsync(TimeSpan timeout, CancellationToken cancellationToken)
        {
            _dictionary.Clear();

            return Task.FromResult(true);
        }

        public Task<bool> ContainsKeyAsync(ITransaction tx, TKey key)
        {
            return Task.FromResult(_dictionary.ContainsKey(key));
        }

        public Task<bool> ContainsKeyAsync(ITransaction tx, TKey key, LockMode lockMode)
        {
            return Task.FromResult(_dictionary.ContainsKey(key));
        }

        public Task<bool> ContainsKeyAsync(ITransaction tx, TKey key, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.ContainsKey(key));
        }

        public Task<bool> ContainsKeyAsync(ITransaction tx, TKey key, LockMode lockMode, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.ContainsKey(key));
        }

        public Task<ConditionalValue<TValue>> TryGetValueAsync(ITransaction tx, TKey key)
        {
            TValue value;
            var result = _dictionary.TryGetValue(key,  out value);

            return Task.FromResult(new ConditionalValue<TValue>(result, value));
        }

        public Task<ConditionalValue<TValue>> TryGetValueAsync(ITransaction tx, TKey key, LockMode lockMode)
        {
            TValue value;
            var result = _dictionary.TryGetValue(key, out value);

            return Task.FromResult(new ConditionalValue<TValue>(result, value));
        }

        public Task<ConditionalValue<TValue>> TryGetValueAsync(ITransaction tx, TKey key, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            TValue value;
            var result = _dictionary.TryGetValue(key, out  value);

            return Task.FromResult(new ConditionalValue<TValue>(result, value));
        }

        public Task<ConditionalValue<TValue>> TryGetValueAsync(
            ITransaction tx, TKey key, LockMode lockMode, TimeSpan timeout, CancellationToken cancellationToken)
        {
            TValue value;
            var result = _dictionary.TryGetValue(key, out  value);

            return Task.FromResult(new ConditionalValue<TValue>(result, value));
        }

        public Task SetAsync(ITransaction tx, TKey key, TValue value)
        {
            _dictionary[key] = value;

            return Task.FromResult(true);
        }

        public Task SetAsync(ITransaction tx, TKey key, TValue value, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            _dictionary[key] = value;

            return Task.FromResult(true);
        }

        public Task<TValue> GetOrAddAsync(ITransaction tx, TKey key, Func<TKey, TValue> valueFactory)
        {
            return Task.FromResult(_dictionary.GetOrAdd(key, valueFactory));
        }

        public Task<TValue> GetOrAddAsync(ITransaction tx, TKey key, TValue value)
        {
            return Task.FromResult(_dictionary.GetOrAdd(key, value));
        }

        public Task<TValue> GetOrAddAsync(ITransaction tx, TKey key, Func<TKey, TValue> valueFactory, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.GetOrAdd(key, valueFactory));
        }

        public Task<TValue> GetOrAddAsync(ITransaction tx, TKey key, TValue value, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.GetOrAdd(key, value));
        }

        public Task<bool> TryAddAsync(ITransaction tx, TKey key, TValue value)
        {
            return Task.FromResult(_dictionary.TryAdd(key, value));
        }

        public Task<bool> TryAddAsync(ITransaction tx, TKey key, TValue value, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.TryAdd(key, value));
        }

        public Task<ConditionalValue<TValue>> TryRemoveAsync(ITransaction tx, TKey key)
        {
            TValue outValue;
            return Task.FromResult(new ConditionalValue<TValue>(_dictionary.TryRemove(key, out outValue), outValue));
        }

        public Task<ConditionalValue<TValue>> TryRemoveAsync(ITransaction tx, TKey key, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return TryRemoveAsync(tx, key);
        }

        public Task<bool> TryUpdateAsync(ITransaction tx, TKey key, TValue newValue, TValue comparisonValue)
        {
            return Task.FromResult(_dictionary.TryUpdate(key, newValue, comparisonValue));
        }

        public Task<bool> TryUpdateAsync(
            ITransaction tx, TKey key, TValue newValue, TValue comparisonValue, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_dictionary.TryUpdate(key, newValue, comparisonValue));
        }

        public Task<IAsyncEnumerable<KeyValuePair<TKey, TValue>>> CreateEnumerableAsync(ITransaction txn)
        {
            return
                Task.FromResult<IAsyncEnumerable<KeyValuePair<TKey, TValue>>>(
                    new MockAsyncEnumerable<KeyValuePair<TKey, TValue>>(_dictionary));
        }

        public Task<IAsyncEnumerable<KeyValuePair<TKey, TValue>>> CreateEnumerableAsync(ITransaction txn,
            EnumerationMode enumerationMode)
        {
            return Task.FromResult<IAsyncEnumerable<KeyValuePair<TKey, TValue>>>(
                new MockAsyncEnumerable<KeyValuePair<TKey, TValue>>(
                    enumerationMode == EnumerationMode.Unordered
                        ? (IEnumerable<KeyValuePair<TKey, TValue>>) _dictionary
                        : _dictionary.OrderBy(x => x.Key)));
        }

        public Task<IAsyncEnumerable<KeyValuePair<TKey, TValue>>> CreateEnumerableAsync(
            ITransaction txn, Func<TKey, bool> filter, EnumerationMode enumerationMode)
        {
            return Task.FromResult<IAsyncEnumerable<KeyValuePair<TKey, TValue>>>(
                new MockAsyncEnumerable<KeyValuePair<TKey, TValue>>(
                    enumerationMode == EnumerationMode.Unordered
                        ? _dictionary.Where(x => filter(x.Key))
                        : _dictionary.Where(x => filter(x.Key)).OrderBy(x => x.Key)));
        }

        public Task<long> GetCountAsync(ITransaction tx)
        {
            return Task.FromResult((long) _dictionary.Count);
        }

        public Task<long> GetCountAsync()
        {
            return Task.FromResult((long) _dictionary.Count);
        }
    }

    public class MockReliableQueue<T> : IReliableQueue<T>
    {
        private readonly ConcurrentQueue<T> queue = new ConcurrentQueue<T>();

        public Task EnqueueAsync(ITransaction tx, T item, TimeSpan timeout, CancellationToken cancellationToken)
        {
            queue.Enqueue(item);

            return Task.FromResult(true);
        }

        public Task EnqueueAsync(ITransaction tx, T item)
        {
            queue.Enqueue(item);

            return Task.FromResult(true);
        }

        public Task<ConditionalValue<T>> TryDequeueAsync(ITransaction tx, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            var result = queue.TryDequeue(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task<ConditionalValue<T>> TryDequeueAsync(ITransaction tx)
        {
            var result = queue.TryDequeue(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task<ConditionalValue<T>> TryPeekAsync(ITransaction tx, LockMode lockMode, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            var result = queue.TryPeek(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task<ConditionalValue<T>> TryPeekAsync(ITransaction tx, LockMode lockMode)
        {
            var result = queue.TryPeek(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task<ConditionalValue<T>> TryPeekAsync(ITransaction tx, TimeSpan timeout,
            CancellationToken cancellationToken)
        {
            var result = queue.TryPeek(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task<ConditionalValue<T>> TryPeekAsync(ITransaction tx)
        {
            var result = queue.TryPeek(out T item);

            return
                Task.FromResult(
                    (ConditionalValue<T>) Activator.CreateInstance(typeof(ConditionalValue<T>), result, item));
        }

        public Task ClearAsync()
        {
            while (!queue.IsEmpty)
            {
                queue.TryDequeue(out T result);
            }

            return Task.FromResult(true);
        }

        public Task<IAsyncEnumerable<T>> CreateEnumerableAsync(ITransaction tx)
        {
            return Task.FromResult<IAsyncEnumerable<T>>(new MockAsyncEnumerable<T>(queue));
        }

        public Task<long> GetCountAsync(ITransaction tx)
        {
            return Task.FromResult<long>(queue.Count);
        }

        public Uri Name { get; set; }

        public Task<long> GetCountAsync()
        {
            return Task.FromResult((long) queue.Count);
        }
    }

    public class MockReliableStateManager : IReliableStateManagerReplica
    {
        private readonly Dictionary<Type, Type> dependencyMap = new Dictionary<Type, Type>
        {
            {typeof(IReliableDictionary<,>), typeof(MockReliableDictionary<,>)},
            {typeof(IReliableQueue<>), typeof(MockReliableQueue<>)}
        };

        private readonly ConcurrentDictionary<Uri, IReliableState> store =
            new ConcurrentDictionary<Uri, IReliableState>();

        public Func<CancellationToken, Task<bool>> OnDataLossAsync { set; get; }

        public ITransaction CreateTransaction()
        {
            return new MockTransaction();
        }

        public Task RemoveAsync(string name)
        {
            store.TryRemove(ToUri(name), out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(ITransaction tx, string name)
        {
            store.TryRemove(ToUri(name), out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(string name, TimeSpan timeout)
        {
            store.TryRemove(ToUri(name), out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(ITransaction tx, string name, TimeSpan timeout)
        {
            store.TryRemove(ToUri(name), out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(Uri name)
        {
            store.TryRemove(name, out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(Uri name, TimeSpan timeout)
        {
            store.TryRemove(name, out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(ITransaction tx, Uri name)
        {
            store.TryRemove(name, out IReliableState result);

            return Task.FromResult(true);
        }

        public Task RemoveAsync(ITransaction tx, Uri name, TimeSpan timeout)
        {
            store.TryRemove(name, out IReliableState result);

            return Task.FromResult(true);
        }

        public Task<ConditionalValue<T>> TryGetAsync<T>(string name) where T : IReliableState
        {
            var result = store.TryGetValue(ToUri(name), out IReliableState item);

            return Task.FromResult(new ConditionalValue<T>(result, (T) item));
        }

        public Task<ConditionalValue<T>> TryGetAsync<T>(Uri name) where T : IReliableState
        {
            var result = store.TryGetValue(name, out IReliableState item);

            return Task.FromResult(new ConditionalValue<T>(result, (T) item));
        }

        public Task<T> GetOrAddAsync<T>(string name) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(ToUri(name), GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(ITransaction tx, string name) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(ToUri(name), GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(string name, TimeSpan timeout) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(ToUri(name), GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(ITransaction tx, string name, TimeSpan timeout) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(ToUri(name), GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(Uri name) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(name, GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(Uri name, TimeSpan timeout) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(name, GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(ITransaction tx, Uri name) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(name, GetDependency(typeof(T))));
        }

        public Task<T> GetOrAddAsync<T>(ITransaction tx, Uri name, TimeSpan timeout) where T : IReliableState
        {
            return Task.FromResult((T) store.GetOrAdd(name, GetDependency(typeof(T))));
        }

        public bool TryAddStateSerializer<T>(IStateSerializer<T> stateSerializer)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<IReliableState> GetAsyncEnumerator()
        {
            return new MockAsyncEnumerator<IReliableState>(store.Values.GetEnumerator());
        }

        public void Initialize(StatefulServiceInitializationParameters initializationParameters)
        {
        }

        public Task<IReplicator> OpenAsync(ReplicaOpenMode openMode, IStatefulServicePartition partition,
            CancellationToken cancellationToken)
        {
            return null;
        }

        public Task ChangeRoleAsync(ReplicaRole newRole, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task CloseAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public void Abort()
        {
        }

        public Task BackupAsync(Func<BackupInfo, CancellationToken, Task<bool>> backupCallback)
        {
            throw new NotImplementedException();
        }

        public Task BackupAsync(
            BackupOption option, TimeSpan timeout, CancellationToken cancellationToken,
            Func<BackupInfo, CancellationToken, Task<bool>> backupCallback)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(string backupFolderPath)
        {
            throw new NotImplementedException();
        }

        public Task RestoreAsync(string backupFolderPath, RestorePolicy restorePolicy,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync(ITransaction tx)
        {
            store.Clear();
            return Task.FromResult(true);
        }

        public Task ClearAsync()
        {
            store.Clear();
            return Task.FromResult(true);
        }

        private IReliableState GetDependency(Type t)
        {
            var mockType = dependencyMap[t.GetGenericTypeDefinition()];

            return (IReliableState) Activator.CreateInstance(mockType.MakeGenericType(t.GetGenericArguments()));
        }

        private Uri ToUri(string name)
        {
            return new Uri("mock://" + name, UriKind.Absolute);
        }

#pragma warning disable 0067
        public event EventHandler<NotifyTransactionChangedEventArgs> TransactionChanged;

        public event EventHandler<NotifyStateManagerChangedEventArgs> StateManagerChanged;
#pragma warning restore 0067
    }

    /// <summary>
    ///     Simple wrapper for a synchronous IEnumerable of T.
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

    /// <summary>
    ///     Simply wrapper for a synchronous IEnumerator of T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class MockAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;

        public MockAsyncEnumerator(IEnumerator<T> enumerator)
        {
            this.enumerator = enumerator;
        }

        public T Current
        {
            get { return enumerator.Current; }
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(enumerator.MoveNext());
        }

        public void Reset()
        {
            enumerator.Reset();
        }
    }

    public class MockTransaction : ITransaction
    {
        public Task CommitAsync()
        {
            return Task.FromResult(true);
        }

        public void Abort()
        {
        }

        public long TransactionId
        {
            get { return 0L; }
        }

        public long CommitSequenceNumber
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
        }

        public Task<long> GetVisibilitySequenceNumberAsync()
        {
            return Task.FromResult(0L);
        }
    }
}