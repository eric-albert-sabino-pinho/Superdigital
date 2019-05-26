using Superdigital.Domain.Entities;
using Superdigital.Domain.Interface;
using Superdigital.Domain.Specifications;
using Superdigital.Domain.VaueObjects;

namespace Superdigital.Domain.Validations
{    public class ValidationRule<TEntity> : IValidationRule<TEntity>
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

    public class TransacaoValidation : Validation<TransacaoEntity>
    {
        public TransacaoValidation()
        {
            var ContaOrigemValidaSpec = new ContaOrigemSpec();
            base.AddRule(new ValidationRule<TransacaoEntity>(ContaOrigemValidaSpec, ContaOrigemValidaSpec.MensagemDeRetorno));

            var ContaDestinoValidaSpec = new ContaDestinoSpec();
            base.AddRule(new ValidationRule<TransacaoEntity>(ContaDestinoValidaSpec, ContaDestinoValidaSpec.MensagemDeRetorno));

            var ValorValidaSpec = new ValorSpec();
            base.AddRule(new ValidationRule<TransacaoEntity>(ValorValidaSpec, ValorValidaSpec.MensagemDeRetorno));
        }
    }
}
