using Superdigital.Domain.Entities;
using Superdigital.Domain.Interface;
using Superdigital.Domain.Menssagens;

namespace Superdigital.Domain.Specifications
{
    public class ContaDestinoSpec : ISpecification<TransacaoEntity>
    {
        public bool IsSatisfiedBy(TransacaoEntity entity)
        {
            if (entity == null)
                return false;

            if (!string.IsNullOrEmpty(entity.ContaDestino))
                return true;
            else
                return false;
        }

        public string MensagemDeRetorno
        {
            get
            {
                return Dicionario.CriticasChaves.Processo_ContaDestino_Obrigatorio;
            }
        }
    }

}
