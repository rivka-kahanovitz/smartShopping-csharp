using AutoMapper;
using Humanizer;
using Repository.Entities;
using Repository.Interfaces;
using common.DTOs;
using common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Service
{
    public class UserloginService : IService<UserLoginDto>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;
        public UserloginService(IRepository<User> repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public UserLoginDto AddItem(UserLoginDto item)
        {
            item.Password = PasswordHasher.Hash(item.Password);
            return _mapper.Map<UserLoginDto>(_repository.Add(_mapper.Map<User>(item)));
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<UserLoginDto> GetAll()
        {
            return _mapper.Map<List<UserLoginDto>>(_repository.GetAll());
        }

        public UserLoginDto GetById(int id)
        {
            return _mapper.Map<UserLoginDto>(_repository.GetById(id));
        }

        public UserLoginDto Update(int id, UserLoginDto item)
        {
            return _mapper.Map<UserLoginDto>(_repository.Put(id, _mapper.Map<User>(item)));
        }
    }
}
