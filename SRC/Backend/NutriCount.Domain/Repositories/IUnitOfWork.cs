﻿namespace NutriCount.Domain.Repositories
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
