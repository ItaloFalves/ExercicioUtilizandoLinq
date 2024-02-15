using ExercicioUtilizandoLinq.Entities;
using System.Globalization;

namespace ExercicioUtilizandoLinq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entre com o caminho do arquivo dos funcionários: ");
            string caminho = Console.ReadLine();

            List<Funcionario> funcionarios = new List<Funcionario>();

            try
            {
                using (StreamReader sr = File.OpenText(caminho))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] arquivos = sr.ReadLine().Split(',');
                        string nome = arquivos[0];
                        string email = arquivos[1];
                        double salario = double.Parse(arquivos[2], CultureInfo.InvariantCulture);

                        funcionarios.Add(new Funcionario(nome, email, salario));
                    }
                }

                Console.WriteLine();
                Console.Write("Digite o valor que deseja saber os emails dos funcionários que ganham mais que o valor digitado: ");
                double valor = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.WriteLine();

                var emailDeFuncionarios =
                   from p in funcionarios
                   where p.Salario > valor
                   orderby p.Nome
                   select p.Email;

                Console.WriteLine("Emails de pessoais que o salário é maior que " + valor + ":");

                foreach (var email in emailDeFuncionarios)
                {
                    Console.WriteLine(email);
                }

                var funcionarioM = funcionarios.Where(f => f.Nome[0] == 'M').Sum(f => f.Salario);

                Console.Write("Soma dos salários de pessoas que começando com a letra 'M': " + funcionarioM);
                Console.WriteLine();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}