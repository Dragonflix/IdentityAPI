using AutoMapper;
using IdentityBLL.Interfaces;
using IdentityBLL.Models;
using IdentityDAL.Entities;
using IdentityDAL.Interfaces;

namespace IdentityBLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModel>> GetAllAsync()
        {
            var roles = await _unitOfWork.RoleRepository.GetAllAsync();
            return roles.Select(p => _mapper.Map<RoleModel>(p)).ToList();    
        }

        public async Task<RoleModel> GetByIdAsync(Guid id)
        {
            var project = await _unitOfWork.RoleRepository.GetByIdAsync(id);
            return _mapper.Map<RoleModel>(project);
        }

        public async Task AddAsync(RoleModel model)
        {
            var project = _mapper.Map<Role>(model);
            await _unitOfWork.RoleRepository.AddAsync(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(RoleModel model)
        {
            var project = _mapper.Map<Role>(model);
            await _unitOfWork.RoleRepository.UpdateAsync(project);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.RoleRepository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
