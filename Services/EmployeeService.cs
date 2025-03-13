using System;
using System.IO;
using System.Text.RegularExpressions;
using EmployeeAPI.Models;

namespace EmployeeAPI.Services
{
    public class EmployeeService
    {
        private const string FileName = "EMPLOYEE.DAT";

        public string ValidateEmployee(Employee employee)
        {
            // Validação do ID: deve ter exatamente 4 dígitos numéricos
            if (string.IsNullOrWhiteSpace(employee.EmployeeId) || !Regex.IsMatch(employee.EmployeeId, @"^\d{4}$"))
            {
                return "o ID deve conter 4 dígitos numéricos.";
            }

            // Validação do Nome: no máximo 20 caracteres
            if (employee.Name == null || employee.Name.Length > 20)
            {
                return "o Nome deve conter até 20 caracteres.";
            }

            // Validação da Idade: deve ser numérica com 2 dígitos
            if (string.IsNullOrWhiteSpace(employee.Age) || !Regex.IsMatch(employee.Age, @"^\d{2}$"))
            {
                return "a Idade deve conter 2 dígitos numéricos.";
            }

            // Validação do Endereço: no máximo 30 caracteres
            if (employee.Address == null || employee.Address.Length > 30)
            {
                return "o Endereço deve conter até 30 caracteres.";
            }

            return null; // Retorna null se não houver erros de validação
        }

        public void AddEmployee(Employee employee)
        {
            // Formata o registro do funcionário
            // Usando o caractere '|' para separar os campos
            string record = $"{employee.EmployeeId}|{employee.Name}|{employee.Age}|{employee.Address}";
            // Adiciona o registro ao final do arquivo, preservando os dados existentes
            File.AppendAllText(FileName, record + Environment.NewLine);
        }
    }
}
