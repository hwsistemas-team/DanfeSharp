using System;

namespace DanfeSharp
{
    public class DanfeConfig
    {
        private float _margem = 5;
        private int _qtdCanhoto = 1;
        private string _formatoQauntidade = "#,0.00##";
        private string _formatoValorUnitario = "#,0.00##";

        public Orientacao Orientacao { get; set; } = Orientacao.Retrato;
        public bool IsRetrato => Orientacao == Orientacao.Retrato;
        public bool IsPaisagem => Orientacao == Orientacao.Paisagem;
        public bool ExibirIcmsInterestadual { get; set; } = true;
        public bool ExibirPisConfins { get; set; } = true;
        public bool ExibirBlocoLocalEntrega { get; set; } = true;
        public bool ExibirBlocoLocalRetirada { get; set; } = true;
        public bool ExibirBlocoFatura { get; set; } = true;
        public bool PreferirEmitenteNomeFantasia { get; set; } = true;

        public bool NFCeItensEm2Linhas { get; set; } = false;
        public bool NFCeExibirItens { get; set; } = true;

        public string FormatoProdutoQuantidade
        {
            get => _formatoQauntidade;
            set
            {
                _formatoQauntidade = value ?? throw new ArgumentOutOfRangeException("O formato do campo quantidade não deve ser nulo.");
            }
        }

        public string FormatoProdutoValorUnitario
        {
            get => _formatoValorUnitario;
            set
            {
                _formatoValorUnitario = value ?? throw new ArgumentOutOfRangeException("O formato do campo valor unitário não deve ser nulo.");
            }
        }

        public int QuantidadeCanhotos
        {
            get => _qtdCanhoto;
            set
            {
                if (value >= 0 && value <= 2)
                    _qtdCanhoto = value;
                else
                    throw new ArgumentOutOfRangeException("A quantidade de canhotos deve de 0 a 2.");
            }
        }

        public float Margem
        {
            get => _margem;
            set
            {
                if (value >= 2 && value <= 5)
                    _margem = value;
                else
                    throw new ArgumentOutOfRangeException("A margem deve ser entre 2 e 5.");
            }
        }
    }
}