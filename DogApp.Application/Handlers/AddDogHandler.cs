﻿using AutoMapper;
using DogApp.Application.Commands.Dog;
using DogApp.Application.Repositories;
using DogApp.Domain.DbEntities;
using LanguageExt.Common;
using MediatR;

namespace DogApp.Application.Handlers
{
    public class AddDogHandler : IRequestHandler<AddDogCommand, Result<Guid>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public AddDogHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            bool isDogExist = await _repositoryWrapper.Dogs.AnyAsync(d => d.Name == request.Name, cancellationToken);

            if (isDogExist)
            {
                var invalidOperationException = new InvalidOperationException("Dog wih the same name already exist");

                return new Result<Guid>(invalidOperationException);
            }

            var dogToAdd = _mapper.Map<DbDog>(request);
            var addedId = await _repositoryWrapper.Dogs.AddAsync(dogToAdd, cancellationToken);
            await _repositoryWrapper.SaveChangesAsync(cancellationToken);

            return addedId;
        }
    }
}