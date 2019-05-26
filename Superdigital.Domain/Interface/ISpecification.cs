namespace Superdigital.Domain.Interface
{
    public interface ISpecification<in TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);

        string MensagemDeRetorno { get; }
    }

}
