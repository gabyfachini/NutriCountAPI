﻿using NutriCount.Domain.Repositories;
using NutriCount.Domain.Repositories.User;
using NutriCount.Domain.Services.Storage;

namespace NutriCount.Application.UseCases.User.Delete.Delete
{
    public class DeleteUserAccountUseCase : IDeleteUserAccountUseCase
    {
        private readonly IUserDeleteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobStorageService _blobStorageService;

        public DeleteUserAccountUseCase(
            IUserDeleteOnlyRepository repository,
            IBlobStorageService blobStorageService,
            IUnitOfWork unitOfWork)
        {
            _blobStorageService = blobStorageService;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(Guid userIdentifier)
        {
            await _blobStorageService.DeleteContainer(userIdentifier);

            await _repository.DeleteAccount(userIdentifier);

            await _unitOfWork.Commit();
        }
    }
}
