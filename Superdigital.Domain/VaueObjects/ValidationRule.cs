using Superdigital.Domain.Interface;

namespace Superdigital.Domain.VaueObjects
{
    public class ValidationRule<TEntity> : IValidationRule<TEntity>
    {
        private readonly ISpecification<TEntity> _specificationRule;
        private object mensagemDeRetorno;

        public ValidationRule(ISpecification<TEntity> specificationRule)
        {
            _specificationRule = specificationRule;
            ErrorMessage = specificationRule.MensagemDeRetorno;
        }

        public ValidationRule(ISpecification<TEntity> specificationRule, string errorMessage)
        {
            _specificationRule = specificationRule;
            ErrorMessage = errorMessage;
        }

        public ValidationRule(object mensagemDeRetorno)
        {
            this.mensagemDeRetorno = mensagemDeRetorno;
        }

        public string ErrorMessage { get; private set; }
        public bool Valid(TEntity entity)
        {
            return _specificationRule.IsSatisfiedBy(entity);
        }
    }

}
