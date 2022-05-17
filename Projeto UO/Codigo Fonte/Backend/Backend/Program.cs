using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Model;
using Backend.Repository;

namespace Backend
{
    class Program
    {
        static void Main(string[] args)
        {
			FormacaoAcademicaRepository f = new FormacaoAcademicaRepository();
			var x = f.GetAllFormacao();
			//DocenteRepository DocenteRepository = new DocenteRepository();

			//DocenteRepository.Add(new Docente
			//{
			//    Cpf = "00011199922",
			//    Nome_Completo = "Rodrigo Duprat",
			//    Rg = "0000000",
			//    Data_Nascimento = Convert.ToDateTime("01/01/01"),
			//    Telefone_Fixo = "2222222222",
			//    Celular = "2222222222",
			//    Celular2 = "2222222222",
			//    Email = "rodrigo@senac.com.br",
			//    Foto = "PIC",
			//    Disp_Viajar = false,
			//    Disp_Horario = "",
			//    Endereço = "rua hortolandia",
			//    Cidade = "Campo Grande",
			//    UF = "MS",
			//    Cep = "79190114"
			//});

			//List<Docente> LDocenteRepository = DocenteRepository.GetAll().ToList<Docente>();
			//foreach (Docente dados in LDocenteRepository)
			//{
			//    Console.WriteLine($"Do_Nome_Completo = {dados.Nome_Completo}");
			//    Console.WriteLine($"Cpf = {dados.Cpf}");
			//    Console.WriteLine($"Rg = {dados.Rg}");
			//    Console.WriteLine($"DataNascimento = {dados.Data_Nascimento}");
			//    Console.WriteLine($"Telefone Fixo = {dados.Telefone_Fixo}");
			//    Console.WriteLine($"Celular = {dados.Celular}");
			//    Console.WriteLine($"Celular2 = {dados.Celular2}");
			//    Console.WriteLine($"Cep = {dados.Cep}");
			//    Console.WriteLine($"UF = {dados.UF}");
			//    Console.WriteLine($"Email = {dados.Email}");
			//    Console.WriteLine($"Foto = {dados.Foto}");
			//    Console.WriteLine($"Disp_Viajar = {dados.Disp_Viajar}");
			//    Console.WriteLine($"Endereco = {dados.Endereço}");
			//    Console.WriteLine($"Disp_Horario = {dados.Disp_Horario}");




			//   }
			

    }
}
}
