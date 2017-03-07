using System;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.UnitOfWork;

namespace WebForLink.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebForLinkContexto _contexto;

        public UnitOfWork(IWebForLinkContexto contexto)
        {
            _contexto = contexto;
        }

        public int Finalizar()
        {
            try
            {
                return _contexto.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.Write("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //Log.ErrorFormat("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.Write("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                        //Log.ErrorFormat("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Erro ao tentar salvar. Tente novamente", ex);
            }
        }

        public void Dispose()
        {
            Finalizar();
            GC.SuppressFinalize(this);
        }
    }
}
