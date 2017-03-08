using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Services.Common
{
    public class Service<TEntity> : IService<TEntity>
        where TEntity : class
    {
        #region Constructor

        private readonly IRepository<TEntity> _repository;
        private readonly IReadOnlyRepository<TEntity> _readOnlyRepository;
        private readonly ValidationResult _validationResult;

        public Service(
            IRepository<TEntity> repository,
            IReadOnlyRepository<TEntity> readOnlyRepository)
        {
            _repository = repository;
            _readOnlyRepository = readOnlyRepository;
            _validationResult = new ValidationResult();
        }

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
            _validationResult = new ValidationResult();
        }

        #endregion

        #region Properties

        protected IRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        protected IReadOnlyRepository<TEntity> ReadOnlyRepository
        {
            get { return _readOnlyRepository; }
        }

        protected ValidationResult ValidationResult
        {
            get { return _validationResult; }
        }

        #endregion

        #region Read Methods

        public virtual TEntity Get(int id, bool @readonly = false)
        {
            return @readonly
                ? _readOnlyRepository.Get(id)
                : _repository.Get(id);
        }

        public virtual TEntity GetAllReferences(int id, bool @readonly = false)
        {
            return @readonly
                ? _readOnlyRepository.GetAllReferences(id)
                : _repository.GetAllReferences(id);
        }

        public virtual IEnumerable<TEntity> All(bool @readonly = false)
        {
            return @readonly
                ? _readOnlyRepository.All()
                : _repository.All();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? _readOnlyRepository.Find(predicate)
                : _repository.Find(predicate);
        }

        #endregion

        #region CRUD Methods

        public virtual ValidationResult Add(TEntity entity)
        {
            if (!ValidationResult.EstaValidado)
                return ValidationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.EhValido)
                return selfValidationEntity.ValidationResult;


            _repository.Add(entity);
            return _validationResult;
        }

        public virtual ValidationResult Update(TEntity entity)
        {
            if (!ValidationResult.EstaValidado)
                return ValidationResult;

            var selfValidationEntity = entity as ISelfValidation;
            if (selfValidationEntity != null && !selfValidationEntity.EhValido)
                return selfValidationEntity.ValidationResult;

            _repository.Update(entity);
            return _validationResult;
        }

        public virtual ValidationResult Delete(TEntity entity)
        {
            if (!ValidationResult.EstaValidado)
                return ValidationResult;

            _repository.Delete(entity);
            return _validationResult;
        }

        public List<ValidationResult> Delete(List<TEntity> entity)
        {
            var validation = new List<ValidationResult>();
            foreach (var item in entity)
            {
                validation.Add(Delete(item));
            }
            return validation;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, bool @readonly = false)
        {
            return @readonly
                ? _readOnlyRepository.Find(predicate).FirstOrDefault()
                : _repository.Find(predicate).FirstOrDefault();
        }

        public List<ValidationResult> Add(List<TEntity> entity)
        {
            var validation = new List<ValidationResult>();
            foreach (var item in entity)
            {
                validation.Add(Add(item));
            }
            return validation;
        }

        //public RetornoPesquisa<TEntity> BuscarPesquisa(Expression<Func<TEntity, bool>> filtros, int tamanhoPagina,
        //    int pagina, Func<TEntity, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _repository.Pesquisar(filtros, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        #endregion
    }
}