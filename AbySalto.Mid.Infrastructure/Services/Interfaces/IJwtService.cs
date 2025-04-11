using System;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Infrastructure.Services.Interfaces;

public interface IJwtService
{
    string CreateToken(User user);
}
