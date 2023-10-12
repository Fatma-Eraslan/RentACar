using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand:IRequest<CreatedBrandResponse>
    {
        public string Name { get; set; }

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>,ITransactionalRequest
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;

            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);//hata yönetimi



                Brand brand = _mapper.Map<Brand>(request);
               // brand.Name = request.Name;
                brand.Id=Guid.NewGuid();

                await _brandRepository.AddAsync(brand);

                //Brand brand2 = _mapper.Map<Brand>(request);                
                //brand2.Id = Guid.NewGuid();
                //await _brandRepository.AddAsync(brand2); iki tane ekleyince aynı isimde hata veriyor ve ikisinide ekletmiyor

               //var result=await _brandRepository.AddAsync(brand);

                //CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(result);
                CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(brand);

                //createdBrandResponse.Id=result.Id;
                //createdBrandResponse.Name=request.Name;
                
                return createdBrandResponse;
            }
        }
    }
}
