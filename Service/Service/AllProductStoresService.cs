using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Repository.Entities;
using Repository.Interfaces;

namespace Service.Service
{
    public class AllProductStoreService : IService<AllProductStoreDto>
    {
        private readonly IRepository<AllProductsStores> _repo;
        private readonly IMapper _mapper;

        public AllProductStoreService(IRepository<AllProductsStores> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public AllProductStoreDto AddItem(AllProductStoreDto item)
        {
            var entity = _mapper.Map<AllProductsStores>(item);
            var added = _repo.Add(entity);
            return _mapper.Map<AllProductStoreDto>(added);
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
        }

        public List<AllProductStoreDto> GetAll()
        {
            return _mapper.Map<List<AllProductStoreDto>>(_repo.GetAll());
        }

        public AllProductStoreDto GetById(int id)
        {
            return _mapper.Map<AllProductStoreDto>(_repo.GetById(id));
        }

        public AllProductStoreDto Update(int id, AllProductStoreDto item)
        {
            var entity = _repo.GetById(id);
            if (entity == null) return null;

            entity.Barcode = item.Barcode;
            entity.StoreId = item.StoreId;

            var updated = _repo.Put(id, entity);
            return _mapper.Map<AllProductStoreDto>(updated);
        }
    }
}
