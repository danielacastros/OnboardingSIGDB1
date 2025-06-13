using OnboardingSIGDB1.Domain.Notifications.Validators;

namespace OnboardingSIGDB1.Domain.Entity;

public class Cargo : Entidade
{
    public string Descricao { get; set; }

    public Cargo(string descricao)
    {
        Descricao = descricao;

        Validar(this, new CargoValidator());
    }

    public void AlterarDescricao(string descricao)
    {
        Descricao = descricao;
        Validar(this, new CargoValidator());
    }
}