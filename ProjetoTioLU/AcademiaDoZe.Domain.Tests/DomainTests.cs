// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Services;
using AcademiaDoZe.Domain.ValueObjects;
using Xunit;

namespace AcademiaDoZe.Domain.Tests;

public class DomainTests
{
    [Fact(DisplayName = "Normalizacao: LimparDigitos 1")]
    public void Normalizacao_LimparDigitos_1() => Assert.Equal("01900010001", NormalizacaoService.LimparDigitos("(01) 90001-0001"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 2")]
    public void Normalizacao_LimparDigitos_2() => Assert.Equal("02900020002", NormalizacaoService.LimparDigitos("(02) 90002-0002"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 3")]
    public void Normalizacao_LimparDigitos_3() => Assert.Equal("03900030003", NormalizacaoService.LimparDigitos("(03) 90003-0003"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 4")]
    public void Normalizacao_LimparDigitos_4() => Assert.Equal("04900040004", NormalizacaoService.LimparDigitos("(04) 90004-0004"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 5")]
    public void Normalizacao_LimparDigitos_5() => Assert.Equal("05900050005", NormalizacaoService.LimparDigitos("(05) 90005-0005"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 6")]
    public void Normalizacao_LimparDigitos_6() => Assert.Equal("06900060006", NormalizacaoService.LimparDigitos("(06) 90006-0006"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 7")]
    public void Normalizacao_LimparDigitos_7() => Assert.Equal("07900070007", NormalizacaoService.LimparDigitos("(07) 90007-0007"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 8")]
    public void Normalizacao_LimparDigitos_8() => Assert.Equal("08900080008", NormalizacaoService.LimparDigitos("(08) 90008-0008"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 9")]
    public void Normalizacao_LimparDigitos_9() => Assert.Equal("09900090009", NormalizacaoService.LimparDigitos("(09) 90009-0009"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 10")]
    public void Normalizacao_LimparDigitos_10() => Assert.Equal("10900100010", NormalizacaoService.LimparDigitos("(10) 90010-0010"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 11")]
    public void Normalizacao_LimparDigitos_11() => Assert.Equal("11900110011", NormalizacaoService.LimparDigitos("(11) 90011-0011"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 12")]
    public void Normalizacao_LimparDigitos_12() => Assert.Equal("12900120012", NormalizacaoService.LimparDigitos("(12) 90012-0012"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 13")]
    public void Normalizacao_LimparDigitos_13() => Assert.Equal("13900130013", NormalizacaoService.LimparDigitos("(13) 90013-0013"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 14")]
    public void Normalizacao_LimparDigitos_14() => Assert.Equal("14900140014", NormalizacaoService.LimparDigitos("(14) 90014-0014"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 15")]
    public void Normalizacao_LimparDigitos_15() => Assert.Equal("15900150015", NormalizacaoService.LimparDigitos("(15) 90015-0015"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 16")]
    public void Normalizacao_LimparDigitos_16() => Assert.Equal("16900160016", NormalizacaoService.LimparDigitos("(16) 90016-0016"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 17")]
    public void Normalizacao_LimparDigitos_17() => Assert.Equal("17900170017", NormalizacaoService.LimparDigitos("(17) 90017-0017"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 18")]
    public void Normalizacao_LimparDigitos_18() => Assert.Equal("18900180018", NormalizacaoService.LimparDigitos("(18) 90018-0018"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 19")]
    public void Normalizacao_LimparDigitos_19() => Assert.Equal("19900190019", NormalizacaoService.LimparDigitos("(19) 90019-0019"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 20")]
    public void Normalizacao_LimparDigitos_20() => Assert.Equal("20900200020", NormalizacaoService.LimparDigitos("(20) 90020-0020"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 21")]
    public void Normalizacao_LimparDigitos_21() => Assert.Equal("21900210021", NormalizacaoService.LimparDigitos("(21) 90021-0021"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 22")]
    public void Normalizacao_LimparDigitos_22() => Assert.Equal("22900220022", NormalizacaoService.LimparDigitos("(22) 90022-0022"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 23")]
    public void Normalizacao_LimparDigitos_23() => Assert.Equal("23900230023", NormalizacaoService.LimparDigitos("(23) 90023-0023"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 24")]
    public void Normalizacao_LimparDigitos_24() => Assert.Equal("24900240024", NormalizacaoService.LimparDigitos("(24) 90024-0024"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 25")]
    public void Normalizacao_LimparDigitos_25() => Assert.Equal("25900250025", NormalizacaoService.LimparDigitos("(25) 90025-0025"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 26")]
    public void Normalizacao_LimparDigitos_26() => Assert.Equal("26900260026", NormalizacaoService.LimparDigitos("(26) 90026-0026"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 27")]
    public void Normalizacao_LimparDigitos_27() => Assert.Equal("27900270027", NormalizacaoService.LimparDigitos("(27) 90027-0027"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 28")]
    public void Normalizacao_LimparDigitos_28() => Assert.Equal("28900280028", NormalizacaoService.LimparDigitos("(28) 90028-0028"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 29")]
    public void Normalizacao_LimparDigitos_29() => Assert.Equal("29900290029", NormalizacaoService.LimparDigitos("(29) 90029-0029"));

    [Fact(DisplayName = "Normalizacao: LimparDigitos 30")]
    public void Normalizacao_LimparDigitos_30() => Assert.Equal("30900300030", NormalizacaoService.LimparDigitos("(30) 90030-0030"));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 1")]
    public void Normalizacao_LimparEspacos_1() => Assert.Equal("texto 1", NormalizacaoService.LimparEspacos("  texto   1  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 2")]
    public void Normalizacao_LimparEspacos_2() => Assert.Equal("texto 2", NormalizacaoService.LimparEspacos("  texto   2  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 3")]
    public void Normalizacao_LimparEspacos_3() => Assert.Equal("texto 3", NormalizacaoService.LimparEspacos("  texto   3  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 4")]
    public void Normalizacao_LimparEspacos_4() => Assert.Equal("texto 4", NormalizacaoService.LimparEspacos("  texto   4  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 5")]
    public void Normalizacao_LimparEspacos_5() => Assert.Equal("texto 5", NormalizacaoService.LimparEspacos("  texto   5  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 6")]
    public void Normalizacao_LimparEspacos_6() => Assert.Equal("texto 6", NormalizacaoService.LimparEspacos("  texto   6  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 7")]
    public void Normalizacao_LimparEspacos_7() => Assert.Equal("texto 7", NormalizacaoService.LimparEspacos("  texto   7  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 8")]
    public void Normalizacao_LimparEspacos_8() => Assert.Equal("texto 8", NormalizacaoService.LimparEspacos("  texto   8  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 9")]
    public void Normalizacao_LimparEspacos_9() => Assert.Equal("texto 9", NormalizacaoService.LimparEspacos("  texto   9  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 10")]
    public void Normalizacao_LimparEspacos_10() => Assert.Equal("texto 10", NormalizacaoService.LimparEspacos("  texto   10  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 11")]
    public void Normalizacao_LimparEspacos_11() => Assert.Equal("texto 11", NormalizacaoService.LimparEspacos("  texto   11  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 12")]
    public void Normalizacao_LimparEspacos_12() => Assert.Equal("texto 12", NormalizacaoService.LimparEspacos("  texto   12  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 13")]
    public void Normalizacao_LimparEspacos_13() => Assert.Equal("texto 13", NormalizacaoService.LimparEspacos("  texto   13  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 14")]
    public void Normalizacao_LimparEspacos_14() => Assert.Equal("texto 14", NormalizacaoService.LimparEspacos("  texto   14  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 15")]
    public void Normalizacao_LimparEspacos_15() => Assert.Equal("texto 15", NormalizacaoService.LimparEspacos("  texto   15  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 16")]
    public void Normalizacao_LimparEspacos_16() => Assert.Equal("texto 16", NormalizacaoService.LimparEspacos("  texto   16  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 17")]
    public void Normalizacao_LimparEspacos_17() => Assert.Equal("texto 17", NormalizacaoService.LimparEspacos("  texto   17  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 18")]
    public void Normalizacao_LimparEspacos_18() => Assert.Equal("texto 18", NormalizacaoService.LimparEspacos("  texto   18  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 19")]
    public void Normalizacao_LimparEspacos_19() => Assert.Equal("texto 19", NormalizacaoService.LimparEspacos("  texto   19  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 20")]
    public void Normalizacao_LimparEspacos_20() => Assert.Equal("texto 20", NormalizacaoService.LimparEspacos("  texto   20  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 21")]
    public void Normalizacao_LimparEspacos_21() => Assert.Equal("texto 21", NormalizacaoService.LimparEspacos("  texto   21  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 22")]
    public void Normalizacao_LimparEspacos_22() => Assert.Equal("texto 22", NormalizacaoService.LimparEspacos("  texto   22  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 23")]
    public void Normalizacao_LimparEspacos_23() => Assert.Equal("texto 23", NormalizacaoService.LimparEspacos("  texto   23  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 24")]
    public void Normalizacao_LimparEspacos_24() => Assert.Equal("texto 24", NormalizacaoService.LimparEspacos("  texto   24  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 25")]
    public void Normalizacao_LimparEspacos_25() => Assert.Equal("texto 25", NormalizacaoService.LimparEspacos("  texto   25  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 26")]
    public void Normalizacao_LimparEspacos_26() => Assert.Equal("texto 26", NormalizacaoService.LimparEspacos("  texto   26  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 27")]
    public void Normalizacao_LimparEspacos_27() => Assert.Equal("texto 27", NormalizacaoService.LimparEspacos("  texto   27  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 28")]
    public void Normalizacao_LimparEspacos_28() => Assert.Equal("texto 28", NormalizacaoService.LimparEspacos("  texto   28  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 29")]
    public void Normalizacao_LimparEspacos_29() => Assert.Equal("texto 29", NormalizacaoService.LimparEspacos("  texto   29  "));

    [Fact(DisplayName = "Normalizacao: LimparEspacos 30")]
    public void Normalizacao_LimparEspacos_30() => Assert.Equal("texto 30", NormalizacaoService.LimparEspacos("  texto   30  "));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 1")]
    public void Normalizacao_ParaMaiusculo_1() => Assert.Equal("ESTADO 1", NormalizacaoService.ParaMaiusculo("estado 1"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 2")]
    public void Normalizacao_ParaMaiusculo_2() => Assert.Equal("ESTADO 2", NormalizacaoService.ParaMaiusculo("estado 2"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 3")]
    public void Normalizacao_ParaMaiusculo_3() => Assert.Equal("ESTADO 3", NormalizacaoService.ParaMaiusculo("estado 3"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 4")]
    public void Normalizacao_ParaMaiusculo_4() => Assert.Equal("ESTADO 4", NormalizacaoService.ParaMaiusculo("estado 4"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 5")]
    public void Normalizacao_ParaMaiusculo_5() => Assert.Equal("ESTADO 5", NormalizacaoService.ParaMaiusculo("estado 5"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 6")]
    public void Normalizacao_ParaMaiusculo_6() => Assert.Equal("ESTADO 6", NormalizacaoService.ParaMaiusculo("estado 6"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 7")]
    public void Normalizacao_ParaMaiusculo_7() => Assert.Equal("ESTADO 7", NormalizacaoService.ParaMaiusculo("estado 7"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 8")]
    public void Normalizacao_ParaMaiusculo_8() => Assert.Equal("ESTADO 8", NormalizacaoService.ParaMaiusculo("estado 8"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 9")]
    public void Normalizacao_ParaMaiusculo_9() => Assert.Equal("ESTADO 9", NormalizacaoService.ParaMaiusculo("estado 9"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 10")]
    public void Normalizacao_ParaMaiusculo_10() => Assert.Equal("ESTADO 10", NormalizacaoService.ParaMaiusculo("estado 10"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 11")]
    public void Normalizacao_ParaMaiusculo_11() => Assert.Equal("ESTADO 11", NormalizacaoService.ParaMaiusculo("estado 11"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 12")]
    public void Normalizacao_ParaMaiusculo_12() => Assert.Equal("ESTADO 12", NormalizacaoService.ParaMaiusculo("estado 12"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 13")]
    public void Normalizacao_ParaMaiusculo_13() => Assert.Equal("ESTADO 13", NormalizacaoService.ParaMaiusculo("estado 13"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 14")]
    public void Normalizacao_ParaMaiusculo_14() => Assert.Equal("ESTADO 14", NormalizacaoService.ParaMaiusculo("estado 14"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 15")]
    public void Normalizacao_ParaMaiusculo_15() => Assert.Equal("ESTADO 15", NormalizacaoService.ParaMaiusculo("estado 15"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 16")]
    public void Normalizacao_ParaMaiusculo_16() => Assert.Equal("ESTADO 16", NormalizacaoService.ParaMaiusculo("estado 16"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 17")]
    public void Normalizacao_ParaMaiusculo_17() => Assert.Equal("ESTADO 17", NormalizacaoService.ParaMaiusculo("estado 17"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 18")]
    public void Normalizacao_ParaMaiusculo_18() => Assert.Equal("ESTADO 18", NormalizacaoService.ParaMaiusculo("estado 18"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 19")]
    public void Normalizacao_ParaMaiusculo_19() => Assert.Equal("ESTADO 19", NormalizacaoService.ParaMaiusculo("estado 19"));

    [Fact(DisplayName = "Normalizacao: ParaMaiusculo 20")]
    public void Normalizacao_ParaMaiusculo_20() => Assert.Equal("ESTADO 20", NormalizacaoService.ParaMaiusculo("estado 20"));

    [Fact(DisplayName = "Cpf: válido 1")]
    public void Cpf_Valido_1() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 1")]
    public void Cpf_Invalido_1() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 2")]
    public void Cpf_Valido_2() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 2")]
    public void Cpf_Invalido_2() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 3")]
    public void Cpf_Valido_3() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 3")]
    public void Cpf_Invalido_3() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 4")]
    public void Cpf_Valido_4() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 4")]
    public void Cpf_Invalido_4() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 5")]
    public void Cpf_Valido_5() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 5")]
    public void Cpf_Invalido_5() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 6")]
    public void Cpf_Valido_6() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 6")]
    public void Cpf_Invalido_6() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 7")]
    public void Cpf_Valido_7() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 7")]
    public void Cpf_Invalido_7() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 8")]
    public void Cpf_Valido_8() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 8")]
    public void Cpf_Invalido_8() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 9")]
    public void Cpf_Valido_9() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 9")]
    public void Cpf_Invalido_9() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Cpf: válido 10")]
    public void Cpf_Valido_10() => Assert.True(Cpf.Criar("529.982.247-25").IsSuccess);

    [Fact(DisplayName = "Cpf: tamanho inválido 10")]
    public void Cpf_Invalido_10() => Assert.True(Cpf.Criar("123").IsFailure);

    [Fact(DisplayName = "Email: válido 1")]
    public void Email_Valido_1() => Assert.True(Email.Criar(" USUARIO1@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 1")]
    public void Email_Invalido_1() => Assert.True(Email.Criar("usuario1").IsFailure);

    [Fact(DisplayName = "Email: válido 2")]
    public void Email_Valido_2() => Assert.True(Email.Criar(" USUARIO2@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 2")]
    public void Email_Invalido_2() => Assert.True(Email.Criar("usuario2").IsFailure);

    [Fact(DisplayName = "Email: válido 3")]
    public void Email_Valido_3() => Assert.True(Email.Criar(" USUARIO3@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 3")]
    public void Email_Invalido_3() => Assert.True(Email.Criar("usuario3").IsFailure);

    [Fact(DisplayName = "Email: válido 4")]
    public void Email_Valido_4() => Assert.True(Email.Criar(" USUARIO4@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 4")]
    public void Email_Invalido_4() => Assert.True(Email.Criar("usuario4").IsFailure);

    [Fact(DisplayName = "Email: válido 5")]
    public void Email_Valido_5() => Assert.True(Email.Criar(" USUARIO5@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 5")]
    public void Email_Invalido_5() => Assert.True(Email.Criar("usuario5").IsFailure);

    [Fact(DisplayName = "Email: válido 6")]
    public void Email_Valido_6() => Assert.True(Email.Criar(" USUARIO6@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 6")]
    public void Email_Invalido_6() => Assert.True(Email.Criar("usuario6").IsFailure);

    [Fact(DisplayName = "Email: válido 7")]
    public void Email_Valido_7() => Assert.True(Email.Criar(" USUARIO7@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 7")]
    public void Email_Invalido_7() => Assert.True(Email.Criar("usuario7").IsFailure);

    [Fact(DisplayName = "Email: válido 8")]
    public void Email_Valido_8() => Assert.True(Email.Criar(" USUARIO8@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 8")]
    public void Email_Invalido_8() => Assert.True(Email.Criar("usuario8").IsFailure);

    [Fact(DisplayName = "Email: válido 9")]
    public void Email_Valido_9() => Assert.True(Email.Criar(" USUARIO9@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 9")]
    public void Email_Invalido_9() => Assert.True(Email.Criar("usuario9").IsFailure);

    [Fact(DisplayName = "Email: válido 10")]
    public void Email_Valido_10() => Assert.True(Email.Criar(" USUARIO10@EXEMPLO.COM ").IsSuccess);

    [Fact(DisplayName = "Email: inválido 10")]
    public void Email_Invalido_10() => Assert.True(Email.Criar("usuario10").IsFailure);

    [Fact(DisplayName = "Cep: válido 1")]
    public void Cep_Valido_1() => Assert.True(Cep.Criar("88.520001").IsSuccess);

    [Fact(DisplayName = "Cep: válido 2")]
    public void Cep_Valido_2() => Assert.True(Cep.Criar("88.520002").IsSuccess);

    [Fact(DisplayName = "Cep: válido 3")]
    public void Cep_Valido_3() => Assert.True(Cep.Criar("88.520003").IsSuccess);

    [Fact(DisplayName = "Cep: válido 4")]
    public void Cep_Valido_4() => Assert.True(Cep.Criar("88.520004").IsSuccess);

    [Fact(DisplayName = "Cep: válido 5")]
    public void Cep_Valido_5() => Assert.True(Cep.Criar("88.520005").IsSuccess);

    [Fact(DisplayName = "Cep: válido 6")]
    public void Cep_Valido_6() => Assert.True(Cep.Criar("88.520006").IsSuccess);

    [Fact(DisplayName = "Cep: válido 7")]
    public void Cep_Valido_7() => Assert.True(Cep.Criar("88.520007").IsSuccess);

    [Fact(DisplayName = "Cep: válido 8")]
    public void Cep_Valido_8() => Assert.True(Cep.Criar("88.520008").IsSuccess);

    [Fact(DisplayName = "Cep: válido 9")]
    public void Cep_Valido_9() => Assert.True(Cep.Criar("88.520009").IsSuccess);

    [Fact(DisplayName = "Cep: válido 10")]
    public void Cep_Valido_10() => Assert.True(Cep.Criar("88.520010").IsSuccess);

}
