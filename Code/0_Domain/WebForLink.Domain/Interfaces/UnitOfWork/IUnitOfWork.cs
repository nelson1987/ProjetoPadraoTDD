using System;

namespace WebForLink.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Finalizar();
    }
}