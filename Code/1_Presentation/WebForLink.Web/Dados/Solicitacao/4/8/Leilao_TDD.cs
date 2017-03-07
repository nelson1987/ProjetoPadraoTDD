//Sistema de Leilão
//usuário dão Lance
namespace Leilao
{
	public class Lance
	{
		public Lance(Usuario usuario, double valor)
		{
			this.Usuario = usuario;
			this.Valor = valor;
		}
		
		public Usuario Usuario { get; private set;}
		
		public double Valor { get; private set;}
	}
	
	public class Usuario
	{
		public Usuario(int id, string nome)
		{
			this.Id = id;
			this.Nome = nome;
		}
		
		public Usuario(string nome) : this(0, nome)
		{
			
		}
		
		public int Id { get; private set;}
		public string Nome { get; private set;}
	}
	
	public class Leilao
	{
		public string Descricao {get; set;}		
		public IList<Lance> Lances {get; set;}
		
		public Leilao(string descricao)
		{
			this.Descricao = descricao;
			this.Lances = new List<Lance>();
		}
		
		//ProporLance
		public void Propoe(Lance lance)
		{
			Lance.Add(lance);
		}
	}
	
	public class Avaliador
	{
		private double maiorDeTodos = Double.MinValue;
		private double menorDeTodos = Double.MaxValue;
		
		public double MaiorLance
		{
			get { return maiorDeTodos; }
		}
		
		public double MenorLance
		{
			get { return menorDeTodos; }
		}
		
		public void Avalia(Leilao leilao)
		{
			foreach(var lance in leilao.lances)
			{
				if(lance.Valor > maiorDeTodos)
				{
					maiorDeTodos = lance.Valor;
				}
				else if(lance.Valor < menorDeTodos)
				{
					menorDeTodos = lance.Valor;
				}
			}
		}
	}
	
	public class TesteDoAvaliador
	{
		static void Main(string[] args)
		{
			Usuario joao = new Usuario("João");
			Usuario jose = new Usuario("José");
			Usuario maria = new Usuario("Maria");
			
			Leilao leilao = new Leilao("Playstation 4 Novo");
			
			leilao.Propoe(new Lance(joao, 300.0));
			leilao.Propoe(new Lance(jose, 400.0));
			leilao.Propoe(new Lance(maria, 250.0));
			
			Avaliador leiloeiro = new Avaliador();
			leiloeiro.Avalia(leilao);
			
			double maiorEsperado = 400;
			double menorEsperado = 250;
			Console.WriteLine(maiorEsperado == leiloeiro.MaiorLance);
			Console.WriteLine(menorEsperado == leiloeiro.MenorLance);
			
			Console.ReadKey();
		}
	}
}
//Como descobrir o preço do maior e do menor Leilao