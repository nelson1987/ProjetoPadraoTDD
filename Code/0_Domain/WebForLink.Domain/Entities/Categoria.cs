using System.Collections.Generic;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Categoria : ISelfValidation
    {
        public Categoria(int id, string descricao, string codigo, bool ativo)
        {
            Id = id;
            Descricao = descricao;
            Codigo = codigo;
            Ativo = ativo;
            SubCategorias = new List<Categoria>();
        }

        public Categoria(string descricao, string codigo, bool ativo)
            : this(0, descricao, codigo, ativo)
        {
        }

        public Categoria(string descricao, string codigo)
            : this(0, descricao, codigo, true)
        {
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public string Codigo { get; private set; }
        public bool Ativo { get; private set; }
        public List<Categoria> SubCategorias { get; private set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new CategoriaValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public bool Equals(Categoria obj)
        {
            if (this.Ativo == obj.Ativo)
                if (this.Descricao == obj.Descricao)
                    if (this.SubCategorias == obj.SubCategorias)
                        return false;
            return true;
        }

        public ValidationResult ValidationResult { get; private set; }

        public void AdicionarSubCategoria(Categoria subCategoria)
        {
            //TODO: Validar se há limite para subcategoria
            //Exemplo: Categoria tem uma sugcategoria. Essa subcategoria, tem outra subcategoria. Que tem outra, que tem outra... Quantas poderiam?
            SubCategorias.Add(subCategoria);
        }
    }
}