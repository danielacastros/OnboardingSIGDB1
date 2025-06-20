namespace OnboardingSIGDB1.Domain.Base;

public static class Resource
{
    public static string KeyEmpresa = "Empresa";
    public static string NomeObrigatorio = "O Nome deve ser informado.";
    public static string CnpjObrigatorio = "O CNPJ é obrigatório.";
    public static string CnpjInvalido = "CNPJ inválido.";
    public static string CnpjCadastrado = "Esse CNPJ já foi cadastrado.";
    public static string DataInvalida = "Data inválida.";
    public static string EmpresaNaoEncontrada = "Erro ao excluir. Empresa não encontrada.";
    public static string DadosNaoFornecidos = "Os dados não foram fornecidos.";
    public static string QuantidadeDeCaracteresInvalida = "A Quantidade de caracteres ultrapassou o limite permitido.";

    public static string KeyFuncionario = "Funcionário";
    public static string FuncionarioNaoEncontrado = "Funcionário não encontrado";
    public static string FuncionarioNaoPossuiVinculoComNenhumaEmpresa = "O funcionário não possui vínculo com nenhuma empresa.";
    public static string CpfObrigatorio = "O CPF deve ser informado.";
    public static string CpfInvalido = "CPF inválido.";
    public static string CpfJaCadastrado = "CPF já cadastrado.";
    
    public static string KeyCargo = "Cargo";
    public static string DescricaoObrigatoria = "A descrição é obrigatória.";
    public static string CargoNaoEncontrado = "Cargo não encontrado.";
    public static string CargoJaCadastradoParaOFuncionario = "Esse cargo já foi cadastrado para esse funcionário, não é possível cadastrá-lo novamente.";
    public static string VinculoJaCadastrado = "O funcionário já está vinculado a uma empresa.";
    public static string NaoEPossivelExcluirEmpresa = "A empresa tem funcionários vinculados, não é possível excluí-la.";
    

}