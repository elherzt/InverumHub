using AutoMapper;
using InverumHub.Core.Common;
using InverumHub.Core.DTOs;
using InverumHub.Core.Entities;
using InverumHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverumHub.Core.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationDTO>> GetAll();
        Task<ApplicationDTO?> GetById(int id);
        Task<ApplicationDTO> Create(CreateAndUpdateApplicationDTO model);
        Task<ApplicationDTO> Update(CreateAndUpdateApplicationDTO model);
        Task Delete(int id);
    }

    public class ApplicationService : IApplicationService
    {
        private readonly IGenericRepository<Application> _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicationService(IGenericRepository<Application> applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<ApplicationDTO> Create(CreateAndUpdateApplicationDTO model)
        {
            var new_app = _mapper.Map<Application>(model);
            new_app.Id = model.Id;
            await _applicationRepository.Add(new_app);
            return _mapper.Map<ApplicationDTO>(new_app);
        }

        public async Task Delete(int id)
        {
            var app = await _applicationRepository.GetById(id);
            if (app == null)
            {
                throw new BusinessException("Application not found");
            }
            await _applicationRepository.Delete(app);

        }

        public async Task<List<ApplicationDTO>> GetAll()
        {
            var apps = await _applicationRepository.GetAll();
            return _mapper.Map<List<ApplicationDTO>>(apps);
        }

        public async Task<ApplicationDTO?> GetById(int id)
        {
            var app = await _applicationRepository.GetById(id);
            return _mapper.Map<ApplicationDTO?>(app);
        }

        public async Task<ApplicationDTO> Update(CreateAndUpdateApplicationDTO model)
        {
            var existingApp = await _applicationRepository.GetById(model.Id);
            if (existingApp == null)
            {
                throw new BusinessException("Application not found");
            }
            _mapper.Map(model, existingApp);
            await _applicationRepository.Update(existingApp);
            return _mapper.Map<ApplicationDTO>(existingApp);
        }
    }
}
