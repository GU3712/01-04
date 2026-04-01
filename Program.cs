using System;
using System.Collections.Generic;
using SistemaBancario.Domain.Models;
using SistemaBancario.Domain.Services;
using SistemaBancario.Domain.Rules;

namespace SistemaBancario.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA BANCÁRIO PRO - ESTRUTURA CLÁSSICA ===");

            
            Cliente cliente = new Cliente(101, "Lucas Silva", "RJ");
            ContaBancaria conta = new ContaBancaria { Numero = "9988-X", Cliente = cliente };

            
            conta.Creditar(2500.50m);

            
            List<IRegraEscore> regrasAtivas = new List<IRegraEscore>
            {
                new RegraSaldoNegativo()
            };
            MotorScore motor = new MotorScore(regrasAtivas);

            
            EmprestimoService service = new EmprestimoService();
            decimal valorSolicitado = 5000m;
            double taxaMensal = 0.03;

            decimal totalSimples = service.CalcularJurosSimples(valorSolicitado, taxaMensal);
            int scoreFinal = motor.ProcessarPontuacaoFinal(cliente, conta);

            
            Console.WriteLine($"\nCliente: {cliente.Nome} | UF: {cliente.EstadoUF}");
            Console.WriteLine($"Saldo em Conta: {conta.Saldo:C}");
            Console.WriteLine($"Pontuação de Crédito: {scoreFinal} pts");
            Console.WriteLine($"Simulação Empréstimo: {totalSimples:C}");

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}