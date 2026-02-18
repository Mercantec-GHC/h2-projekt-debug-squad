using Application.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EfRoomTypeRepositorycs : IRoomTypeRepository
    {
        private readonly AppDbContext _dbContext;

        public EfRoomTypeRepositorycs(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
