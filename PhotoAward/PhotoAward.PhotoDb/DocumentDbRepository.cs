using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using PhotoAward.PhotoDb.Interfaces;

namespace PhotoAward.PhotoDb
{
    public interface IPhotoDbRepository<T> where T : PhotoDocument
    {
        void Initialize(string databaseId, string collectionId, string endpoint, string authKey);
        Task<Document> CreateItemAsync(T item);
        Task<T> GetItemAsync(string id);
        Task<Document> UpdateItemAsync(string id, T item);
    }
    public class PhotoDbRepository<T> : IPhotoDbRepository<T> where T : PhotoDocument
    {
        private  DocumentClient client;
        private   string DatabaseId;
        private   string CollectionId ;

        

        public  async Task<T> GetItemAsync(string id)
        {
            try
            {
                Database database = client.CreateDatabaseQuery().Where(db => db.Id == this.DatabaseId).AsEnumerable().FirstOrDefault();
                //var doc2 = client.CreateDocumentQuery<Document>(database.CollectionsLink).Where(m => m.Id == id).FirstOrDefault();


                //var doc =  client.CreateDocumentQuery<T>(database.CollectionsLink).FirstOrDefault(m => m.Id == id);
                //return doc;
                var response = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId,
                    id));
                var post = JsonConvert.DeserializeObject<T>(response.Resource.ToString());
                return post;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        

        public  async Task<Document> CreateItemAsync(T item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }

        public  async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
        }

        

        public  async void Initialize(string databaseId, string collectionId, string endpoint, string authKey)
        {
            this.DatabaseId = databaseId;
            this.CollectionId = collectionId;
            this.client = new DocumentClient(new Uri(endpoint),authKey, new ConnectionPolicy { EnableEndpointDiscovery = false });
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseId });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId), new DocumentCollection { Id = CollectionId });
        }
        
    }
}
