using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Cargos;

public class CargoTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveCargoTerUmDescricaoInvalida(string descricaoInvalida)
    {
        var cargo = CargoBuilder.Novo().ComDescricao(descricaoInvalida).Build();
        
        Assert.False(cargo.Valid);
        Assert.Contains(cargo.ValidationResult.Errors,
            f => f.PropertyName == nameof(Domain.Entity.Cargo.Descricao) && f.ErrorMessage == Resource.DescricaoObrigatoria);
    }

    [Theory]
    [InlineData("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.")]
    public void NaoDeveCargoTerUmaDescricaoMaiorQueDuzentosECinquentaCaracteres(string descricaoInvalida)
    {
        var cargo = CargoBuilder.Novo().ComDescricao(descricaoInvalida).Build();
        
        Assert.False(cargo.Valid);
        Assert.Contains(cargo.ValidationResult.Errors,
            f => f.PropertyName == nameof(Domain.Entity.Cargo.Descricao) &&
                 f.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }
}