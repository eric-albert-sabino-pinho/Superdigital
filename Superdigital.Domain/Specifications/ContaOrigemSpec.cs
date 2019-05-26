using Superdigital.Domain.Entities;
using Superdigital.Domain.Interface;
using Superdigital.Domain.Menssagens;

namespace Superdigital.Domain.Specifications
{
    public class ContaOrigemSpec : ISpecification<TransacaoEntity>
    {
        public bool IsSatisfiedBy(TransacaoEntity entity)
        {
            if (entity == null)
                return false;

            if (!string.IsNullOrEmpty(entity.ContaOrigem))
                return true;
            else
                return false;
        }

        public string MensagemDeRetorno
        {
            get
            {
                return Dicionario.CriticasChaves.Processo_ContaOrigem_Obrigatorio;
            }
        }

    }

}
