using ECom.Catelog.Model;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ECom.Categlog
{
    public class ServiceFabricCatelogRepository : ICatelogRepository
    {
        private readonly IReliableStateManager _stateManager;
        public ServiceFabricCatelogRepository(IReliableStateManager stateManager)
        {
            this._stateManager = stateManager;
        }
        public async Task AddProduct(Product product)
        {
            var products = await this._stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");
            using(var tx = this._stateManager.CreateTransaction())
            {
                await products.AddOrUpdateAsync(tx, product.Id, product, (id, value) => product);

                await tx.CommitAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await this._stateManager.GetOrAddAsync<IReliableDictionary<Guid, Product>>("products");
            var result = new List<Product>();
            using(var tx = this._stateManager.CreateTransaction())
            {
                var allProducts = await products.CreateEnumerableAsync(tx, EnumerationMode.Unordered);
                using (var enumerator = allProducts.GetAsyncEnumerator())
                {
                    while(await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        var current = enumerator.Current;
                        result.Add(current.Value);
                    }
                }
            }
            return result;

        }
    }
}
