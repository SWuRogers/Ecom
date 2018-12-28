using ECom.Catelog.Model;
using Microsoft.ServiceFabric.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Categlog
{
    public class ServiceFabricCatelogRepository : ICatelogRepository
    {
        private IReliableStateManager _stateManager;
        public ServiceFabricCatelogRepository(IReliableStateManager stateManager)
        {
            this._stateManager = stateManager;
        }
        public async Task AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
