using Superdigital.Domain.VaueObjects;

namespace Superdigital.Domain.Service
{
    public class ApplicationService
    {
        public ValidationResult ValidationResult { get; set; }

        public ValidationResult ObterUltimasNotificacoesDeDominio()
        {
            var retorno = new ValidationResult();
            if (ValidationResult != null)
            {
                foreach (var item in ValidationResult.Errors)
                {
                    retorno.Add(item);
                }
                ValidationResult = new ValidationResult();
            }
            return retorno;
        }
    }
}
