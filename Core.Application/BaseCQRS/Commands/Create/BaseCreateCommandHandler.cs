﻿using AutoMapper;
using Core.Persistence.Repository;
using MediatR;

namespace Core.Application.BaseCQRS.Commands.Create
{
    public class BaseCreateCommandHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TEntity : class, IEntity, new()
    {

        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseCreateCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TEntity entity = new TEntity();
            entity = _mapper.Map<TEntity>(request);

            TEntity _result = await _repository.AddAsync(entity);
            TResponse _response = _mapper.Map<TResponse>(_result);

            return _response;
        }
    }
}
