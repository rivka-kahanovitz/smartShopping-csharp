using AutoMapper;
using common.DTOs;
using common.Interfaces;
using Repository.Entities;
using Repository.Interfaces;

namespace Service.Service
{
    public class StoreService : IService<StoreDto>
    {
        private readonly IRepository<Stores> _storeRepository;
        private readonly IMapper _mapper;

        public StoreService(IRepository<Stores> storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public StoreDto AddItem(StoreDto item)
        {
            var entity = _mapper.Map<Stores>(item);
            var added = _storeRepository.Add(entity);
            return _mapper.Map<StoreDto>(added);
        }

        public void Delete(int id)
        {
            _storeRepository.Delete(id);
        }

        public List<StoreDto> GetAll()
        {
            return _mapper.Map<List<StoreDto>>(_storeRepository.GetAll());
        }

        public StoreDto GetById(int id)
        {
            return _mapper.Map<StoreDto>(_storeRepository.GetById(id));
        }

        public StoreDto Update(int id, StoreDto item)
        {
            var store = _storeRepository.GetById(id);
            if (store == null) return null;

            store.StorName = item.StorName;
            store.BranchName = item.BranchName;
            store.Address = item.Address;

            var updated = _storeRepository.Put(id, store);
            return _mapper.Map<StoreDto>(updated);
        }
    }
}
