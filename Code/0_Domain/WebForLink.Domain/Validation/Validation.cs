using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;

namespace WebForLink.Domain.Validation
{
    public class Validation<TEntity> : IValidation<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, IValidationRule<TEntity>> _validationsRules;

        public Validation()
        {
            _validationsRules = new Dictionary<string, IValidationRule<TEntity>>();
        }

        public virtual ValidationResult Validar(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var key in _validationsRules.Keys)
            {
                var rule = _validationsRules[key];
                if (!rule.Valid(entity))
                    result.Add(new ValidationError(rule.ErrorMessage));
            }
            return result;
        }

        protected virtual void AddRule(IValidationRule<TEntity> validationRule)
        {
            var ruleName = validationRule.GetType() + Guid.NewGuid().ToString("D");
            _validationsRules.Add(ruleName, validationRule);
        }

        protected virtual void RemoveRule(string ruleName)
        {
            _validationsRules.Remove(ruleName);
        }
    }
}