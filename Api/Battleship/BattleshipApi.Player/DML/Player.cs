using DataBaseHelper.Atributos;

namespace BattleshipApi.Player.DML
{
    [Tabela(pNomeTabela: "Users")]
    public class Player
    {
        public int ID { get; set; }

        [Coluna(pNomeColuna: "Name", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Name { get; set; }

        [Coluna(pNomeColuna: "Login", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Login { get; set; }

        [Coluna(pNomeColuna: "Password", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Password { get; set; }

        [Coluna(pNomeColuna: "Code", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Varchar, pTamanhoCampo: 30)]
        public string Code { get; set; }

        [Coluna(pNomeColuna: "Money", pTipoDadosBanco: DataBaseHelper.Enumerados.TipoDadosBanco.Float)]
        public double Money { get; set; }

    }
}